﻿using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.Class;
using Lib_Models.Models_Insert.v1.Class_School;
using Lib_Models.Models_Select.Class;
using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Models_Select.Student;
using Lib_Models.Models_Select.Teacher;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.Abstract;
using Lib_Repository.V1.Class_Repository;
using Lib_Services.Token_Service;
using Lib_Services.V1.Class_Member_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Class_Service
{
    public class Class_Service_v1 : IClass_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly IClass_Repository_v1 _class_Repository_V1;
        private readonly IToken_Service_v1 _token_Service_V1;
        private readonly IToken_Service_v2 _token_Service_V2;
        private readonly IClass_Member_Service_v1 _class_Member_Service_V1;
        private readonly IRepository<tbMonHocClass_Student> _repositoryMonHocClass_Student;
        public Class_Service_v1(Trendyt_DbContext db, IClass_Repository_v1 class_Repository_V1,
            IToken_Service_v1 token_Service_V1, IClass_Member_Service_v1 class_Member_Service_V1,
            IToken_Service_v2 token_Service_V2, IRepository<tbMonHocClass_Student> repositoryMonHocClass_Student)
        {
            _db = db;
            _class_Repository_V1 = class_Repository_V1;
            _token_Service_V1 = token_Service_V1;
            _class_Member_Service_V1 = class_Member_Service_V1;
            _token_Service_V2 = token_Service_V2;
            _repositoryMonHocClass_Student = repositoryMonHocClass_Student;
        }

        #region SelectAll
        public async Task<List<Class_Select_v1>> SelectAll()
        {
            // Id_Menber Manager
            var menberManager = await _token_Service_V2.Get_Menber_Token();

            return await _class_Repository_V1.SelectAll(menberManager!);
        }
        #endregion

        #region SelectById
        public async Task<Class_Select_v1> SelectById(int id)
        {
            // Id_Menber Manager
            var menberManager = await _token_Service_V2.Get_Menber_Token();

            return await _class_Repository_V1.SelectById(menberManager!, id);
        }
        #endregion

        #region InsertAsync
        public async Task<Status_Application> InsertAsync(string name_ClassSchool)
        {
            name_ClassSchool = name_ClassSchool.Trim();
            // Id_Menber Manager
            var menberManager = await _token_Service_V2.Get_Menber_Token();
            var khoa = await _db.tbKhoaSchool.FindAsync(menberManager!.id_KhoaSchool);
            if (khoa == null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Tài khoản chưa gia nhập khoa" };
            }

            var isNameClass = await _db.tbClassSchool.AnyAsync(x => x.id_KhoaSchool == khoa.id_KhoaSchool && x.name_ClassSchool!.ToLower() == name_ClassSchool.ToLower());
            if (isNameClass)
            {
                return new Status_Application { StatusBool = false, StatusType = "Tên lớp đã tồn tại" };
            }

            var tag = await _db.tbTag.FirstOrDefaultAsync(t => t.name == "active");
            tbClassSchool classSchool = new tbClassSchool
            {
                name_ClassSchool = name_ClassSchool,
                id_KhoaSchool = khoa.id_KhoaSchool,
                id_Tag = tag!.id_Tag,
            };
            return await _class_Repository_V1.InsertAsync(classSchool);
        }
        #endregion

        #region UpdateAsync
        public async Task<Status_Application> UpdateAsync(Class_Update_v1 request)
        {
            if (string.IsNullOrEmpty(request.tag))
            {
                request.tag = "active";
            }

            var memberManager = await _token_Service_V2.Get_Menber_Token();
            // Kiểm tra class có tồn tại và name class không tồn tại

            var isIdClass = await _db.tbClassSchool.FindAsync(request.id_ClassSchool);
            if (isIdClass == null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Id lớp không tồn tại" };
            }
            var checkNameClass = await _db.tbClassSchool.FirstOrDefaultAsync(x => x.id_KhoaSchool == memberManager!.id_KhoaSchool
                                && x.id_ClassSchool != isIdClass.id_ClassSchool && x.name_ClassSchool!.ToLower() == request.name_ClassSchool!.ToLower());
            if (checkNameClass != null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Tên lớp đã tồn tại" };
            }
            //KIểm tra teacher
            if (request.id_Teacher != 0)
            {
                // Kiểm tra trong lớp đã tồn tại teacher
                var teacher_Class = await (from mc in _db.tbClassSchool_Menber
                                           where mc.id_ClassSchool == request.id_ClassSchool
                                           join m in _db.tbMenberSchool
                                           on mc.id_MenberSchool equals m.id_MenberSchool
                                           join r in _db.tbRoleSchool
                                           on m.id_RoleSchool equals r.id_RoleSchool
                                           where r.name_Role == "teacher"
                                           select new { memberClass = mc, Member = m, Role = r }).FirstOrDefaultAsync();

                if (teacher_Class == null)
                {
                    Class_Member_Insert_v1 _Insert_V1 = new Class_Member_Insert_v1
                    {
                        id_ClassSchool = request.id_ClassSchool,
                        id_Student = request.id_Teacher
                    };
                    Status_Application status = await _class_Member_Service_V1.Insert(_Insert_V1);
                    if (!status.StatusBool)
                    {
                        return status;
                    }
                }
                else
                {
                    teacher_Class.memberClass.id_MenberSchool = request.id_Teacher;
                }
            }
            else
            {
                return new Status_Application { StatusBool = false, StatusType = "Id teacher 0 không tồn tại" };
            }
            var tag = await _db.tbTag.FirstOrDefaultAsync(t => t.name == request.tag);
            isIdClass.name_ClassSchool = request.name_ClassSchool!.Trim();
            isIdClass.id_Tag = tag!.id_Tag;
            await _db.SaveChangesAsync();
            return new Status_Application { StatusBool = true, StatusType = "success" };
        }
        #endregion


        public async Task<MonHoc_Member_Select> GetMember(int idLopHoc)
        {
            var member_InMonHoc = await _db.tbClassSchool_Menber.Where(x => x.id_ClassSchool == idLopHoc)
                                    .Include(x => x.tbMenberSchool).ThenInclude(m => m!.tbRoleSchool)
                                    .Include(x => x.tbMenberSchool).ThenInclude(m => m!.tbAccount)
                                    .ToListAsync();

            if (member_InMonHoc == null || !member_InMonHoc.Any())
            {
                return null!;
            }

            Select_One_Teacher_v1 teacher = null!;
            List<Student_Select_v1> students = new List<Student_Select_v1>();

            foreach (var item in member_InMonHoc)
            {
                var member = item.tbMenberSchool;
                if (member!.tbRoleSchool!.name_Role == "teacher")
                {
                    teacher = new Select_One_Teacher_v1
                    {
                        id_Teacher = member.id_MenberSchool,
                        user_Name = member.tbAccount?.user_Name,
                        fullName = member.tbAccount?.fullName,
                        phone_User = member.tbAccount?.phone_User,
                        sex_User = member.tbAccount?.sex_User,
                        email_User = member.tbAccount?.email_User,
                        image_User = member.tbAccount?.image_User
                    };
                }
                else
                {
                    Student_Select_v1 student = new Student_Select_v1
                    {
                        id_Student = member.id_MenberSchool,
                        email_User = member.tbAccount?.email_User,
                        fullName = member.tbAccount?.fullName,
                        image_User = member.tbAccount?.image_User,
                        sex_User = member.tbAccount?.sex_User,
                        phone_User = member.tbAccount?.phone_User,
                        user_Name = member.tbAccount?.user_Name
                    };
                    students.Add(student);
                }
            }

            return new MonHoc_Member_Select
            {
                giangvien = teacher,
                student = students
            };
        }
    }
}

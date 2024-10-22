﻿using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using AutoMapper.Execution;
using Lib_Models.Models_Insert.v2.MonHoc;
using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Models_Select.Student;
using Lib_Models.Models_Select.Teacher;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.Abstract;
using Lib_Repository.Repository_Class;
using Lib_Services.Token_Service;
using Lib_Services.V2.DiemDanh;
using Lib_Services.V2.LịchHoc;
using Lib_Services.V2.MonHocClass_Student;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.MonHoc_Service
{
    public class MonHoc_Service_v2 : IMonHoc_Service_v2
    {
        private readonly Trendyt_DbContext _db;
        private readonly IRepository<tbMonHoc> _repositoryMonHoc;
        private readonly IRepository<tbMenberSchool> _repositoryMenberSchool;
        private readonly IRepository<tbMonHocClass_Student> _repositoryMonHocClass_Student;
        private readonly IMonHocClass_Student_v2 _monHocClass_Student_V2;
        private readonly IRepository<tbTag> _repositoryTag;
        private readonly IToken_Service_v2 _token_Service_V2;
        private readonly ILichHoc_Service_v2 _lichHoc_Service_V2;
        private readonly IDiemDanh_Service_v2 _diemDanh_Service_V2;
        public MonHoc_Service_v2(IMonHocClass_Student_v2 monHocClass_Student_V2,
                                 IRepository<tbMonHoc> repositoryMonHoc, Trendyt_DbContext db,
                                 IToken_Service_v2 token_Service_V2, IRepository<tbTag> repositoryTag,
                                 ILichHoc_Service_v2 lichHoc_Service_V2, IDiemDanh_Service_v2 diemDanh_Service_V2,
                                 IRepository<tbMenberSchool> repositoryMenberSchool, IRepository<tbMonHocClass_Student> repositoryMonHocClass_Student)
        {
            _db = db;
            _monHocClass_Student_V2 = monHocClass_Student_V2;
            _repositoryMonHoc = repositoryMonHoc;
            _token_Service_V2 = token_Service_V2;
            _repositoryTag = repositoryTag;
            _lichHoc_Service_V2 = lichHoc_Service_V2;
            _diemDanh_Service_V2 = diemDanh_Service_V2;
            _repositoryMenberSchool = repositoryMenberSchool;
            _repositoryMonHocClass_Student = repositoryMonHocClass_Student;
        }

        public async Task<MonHoc_Member_Select> GetMember(int idMonHoc)
        {
            var member_InMonHoc = await _db.tbMonHocClass_Student.Where(x => x.id_MonHoc == idMonHoc)
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

        public async Task<Status_Application> InsertAsync(MonHoc_Insert_Request_v2 request)
        {
            var executionStrategy = _db.Database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = new TransactionManager(_db))
                {
                    tbMenberSchool memberManager = await _token_Service_V2.Get_Menber_Token();
                    try
                    {
                        if (string.IsNullOrEmpty(request.monHoc!.name_MonHoc))
                        {
                            return new Status_Application { StatusBool = true, StatusType = "Chưa nhập tên môn học"};
                        }
                        var checkNamnMonHoc = await _repositoryMonHoc.GetAll(x => x.name_MonHoc!.ToLower() == request.monHoc.name_MonHoc.ToLower());
                        if (checkNamnMonHoc.Any())
                        {
                            return new Status_Application { StatusBool = true, StatusType = "Tên môn học đã tồn tại" };
                        }
                        // Add môn học
                        var tagDb = await _repositoryTag.GetAll(t => t.name == "active");

                        tbMonHoc monHoc = new tbMonHoc
                        {
                            id_School = memberManager.id_School,
                            name_MonHoc = request.monHoc.name_MonHoc,
                            _danhGiaTrungBinh = 5,
                            id_Tag = tagDb.First().id_Tag,
                            ngayBatDau = request.monHoc!.ngayBatDau,
                            ngayKetThuc = request.monHoc!.ngayKetThuc,
                            _SoBuoiHoc = request.monHoc!._SoBuoiHoc,
                            _SoBuoiNghi = request.monHoc!._SoBuoiNghi,
                        };

                        await _repositoryMonHoc.Insert(monHoc);
                        await _repositoryMonHoc.Commit();
                        int _idMonHoc = monHoc.id_MonHoc;

                        // Add sinh viên vào môn học
                        Status_Application status_addStudent = await _monHocClass_Student_V2.InsertAsync(_idMonHoc, request.students!, request.monHoc.id_Teacher);
                        if (!status_addStudent.StatusBool)
                        {
                            dbContextTransaction.Rollback();
                            return status_addStudent;
                        }

                        // Add lịch học
                        Status_Application status_addLichHoc = await _lichHoc_Service_V2.InsertAsync(request.lichHocs, monHoc._SoBuoiHoc, _idMonHoc);
                        if (!status_addLichHoc.StatusBool)
                        {
                            dbContextTransaction.Rollback();
                            return status_addLichHoc;
                        }

                        // Add điểm danh
                        Status_Application status_addDiemDanh = await _diemDanh_Service_V2.InsertAsync(status_addLichHoc.List_Id_Int!, status_addStudent.List_Id_Int!);
                        if (!status_addDiemDanh.StatusBool)
                        {
                            dbContextTransaction.Rollback();
                            return status_addDiemDanh;
                        }

                        var teacherResult = await _repositoryMenberSchool.GetAllIncluding(m => m.id_MenberSchool == request.monHoc.id_Teacher, ac => ac.tbAccount!);
                        MonHoc_SelectAll_v1 monHoc_SelectAll_V1 = new MonHoc_SelectAll_v1
                        {
                            id_MonHoc = _idMonHoc,
                            name_MonHoc = monHoc.name_MonHoc,
                            danhGiaTrungBinh = monHoc._danhGiaTrungBinh,
                            tag = tagDb.First().name,
                            soBuoiHoc = monHoc._SoBuoiHoc,
                            soBuoiNghi = monHoc._SoBuoiNghi,
                            ngayBatDau = monHoc.ngayBatDau,
                            ngayKetThuc = monHoc.ngayKetThuc,
                            coutnStudent = request.students!.Count(),
                            giangvien = new Select_One_Teacher_v1
                            {
                                id_Teacher = teacherResult.First().id_MenberSchool,
                                user_Name = teacherResult.First().tbAccount!.user_Name,
                                fullName = teacherResult.First().tbAccount!.fullName,
                                image_User = teacherResult.First().tbAccount!.image_User,
                            },
                            student = status_addStudent.myListObj!.Cast<Student_Select_v1>().ToList(),
                            //lichhoc = status_addLichHoc.myListObj!.Cast<LichHoc_MonHoc_Select_v1>().ToList(),
                        };
                        dbContextTransaction.Commit();
                        return new Status_Application { StatusBool = true , StatusType = "success",myObj = monHoc_SelectAll_V1, Id_Int = _idMonHoc, List_Id_Int = status_addLichHoc.List_Id_Int };
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync(ex.ToString());
                        dbContextTransaction.Rollback();
                        return new Status_Application { StatusBool = false, StatusType = ex.Message };
                    }
                }
            });
        }
    }
}

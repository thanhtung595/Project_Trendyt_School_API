using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Class;
using Lib_Models.Models_Select.Khoa;
using Lib_Models.Models_Select.Student;
using Lib_Models.Models_Select.Teacher;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Class_Repository
{
    public class Class_Repository_v1 : IClass_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public Class_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }

        #region SelectAll
        public async Task<List<Class_Select_v1>> SelectAll(tbMenberSchool menberSchoolManager)
        {
            IQueryable<tbClassSchool> query = _db.tbClassSchool;
            if (menberSchoolManager.id_KhoaSchool == 0 && menberSchoolManager.tbRoleSchool!.name_Role != "school management")
            {
                return null!;
            }
            if (menberSchoolManager.id_KhoaSchool != 0)
            {
                var membemInClass = await _db.tbClassSchool_Menber.FirstOrDefaultAsync(x => x.id_MenberSchool == menberSchoolManager.id_MenberSchool);
                query = query.Where(x => x.id_KhoaSchool == menberSchoolManager.id_KhoaSchool && x.id_ClassSchool == membemInClass!.id_ClassSchool);
            }
            var list = await (from cl in query
                              join k in _db.tbKhoaSchool
                              on cl.id_KhoaSchool equals k.id_KhoaSchool
                              join s in _db.tbSchool
                              on k.id_School equals s.id_School
                              where s.id_School == menberSchoolManager.id_School
                              select new Class_Select_v1
                              {
                                  id_ClassSchool = cl.id_ClassSchool,
                                  name_ClassSchool = cl.name_ClassSchool,
                                  khoa = new KhoaSchool_Select_v1
                                  {
                                      id_KhoaSchool = k.id_KhoaSchool,
                                      ma_Khoa = k.ma_Khoa,
                                      name_Khoa = k.name_Khoa
                                  },
                                  tags = (from tag in _db.tbTag
                                          where tag.id_Tag == cl.id_Tag
                                          select tag.name).FirstOrDefault(),
                                  chu_nhiem = (from member_class in _db.tbClassSchool_Menber
                                               where member_class.id_ClassSchool == cl.id_ClassSchool
                                               join member in _db.tbMenberSchool
                                               on member_class.id_MenberSchool equals member.id_MenberSchool
                                               join r in _db.tbRoleSchool
                                               on member.id_RoleSchool equals r.id_RoleSchool
                                               where r.name_Role == "teacher"
                                               join ac in _db.tbAccount
                                               on member.id_Account equals ac.id_Account
                                               select new Select_All_Teacher_v1
                                               {
                                                   id_Teacher = member.id_MenberSchool,
                                                   fullName = ac.fullName,
                                                   user_Name = ac.user_Name,
                                                   image_User = ac.image_User
                                               }).FirstOrDefault(),
                                  count_student = (from member_class in _db.tbClassSchool_Menber
                                                   where member_class.id_ClassSchool == cl.id_ClassSchool
                                                   select member_class).Count() - 1,
                                  
                              }).ToListAsync();
            return list;
        }
        #endregion

        #region SelectById
        public async Task<Class_Select_v1> SelectById(tbMenberSchool menberSchoolManager, int idClass)
        {
            IQueryable<tbClassSchool> query = _db.tbClassSchool;
            if (menberSchoolManager.id_KhoaSchool == 0 && menberSchoolManager.tbRoleSchool!.name_Role != "school management")
            {
                return null!;
            }
            if (menberSchoolManager.id_KhoaSchool != 0)
            {
                query = query.Where(x => x.id_KhoaSchool == menberSchoolManager.id_KhoaSchool);
            }
            var list = await (from cl in query
                              where cl.id_ClassSchool == idClass
                              join k in _db.tbKhoaSchool
                              on cl.id_KhoaSchool equals k.id_KhoaSchool
                              where k.id_School == menberSchoolManager.id_School
                              join s in _db.tbSchool
                              on k.id_School equals s.id_School
                              where s.id_School == menberSchoolManager.id_School
                              select new Class_Select_v1
                              {
                                  id_ClassSchool = cl.id_ClassSchool,
                                  name_ClassSchool = cl.name_ClassSchool,
                                  khoa = new KhoaSchool_Select_v1
                                  {
                                      id_KhoaSchool = k.id_KhoaSchool,
                                      ma_Khoa = k.ma_Khoa,
                                      name_Khoa = k.name_Khoa
                                  },
                                  tags = (from tag in _db.tbTag
                                          where tag.id_Tag == cl.id_Tag
                                          select tag.name).FirstOrDefault(),
                                  chu_nhiem = (from member_class in _db.tbClassSchool_Menber
                                               where member_class.id_ClassSchool == cl.id_ClassSchool
                                               join member in _db.tbMenberSchool
                                               on member_class.id_MenberSchool equals member.id_MenberSchool
                                               join r in _db.tbRoleSchool
                                               on member.id_RoleSchool equals r.id_RoleSchool
                                               where r.name_Role == "teacher"
                                               join ac in _db.tbAccount
                                               on member.id_Account equals ac.id_Account
                                               select new Select_All_Teacher_v1
                                               {
                                                   id_Teacher = member.id_MenberSchool,
                                                   fullName = ac.fullName,
                                                   user_Name = ac.user_Name,
                                                   image_User = ac.image_User
                                               }).FirstOrDefault(),
                                  count_student = (from member_class in _db.tbClassSchool_Menber
                                                   select member_class).Count() -1 
                              }).FirstOrDefaultAsync();
            return list!;
        }
        #endregion

        #region InsertAsync
        public async Task<Status_Application> InsertAsync(tbClassSchool classSchool)
        {
            try
            {
                await _db.tbClassSchool.AddAsync(classSchool);
                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true, StatusType = "success", Id_Int = classSchool.id_ClassSchool };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "error: " + ex.Message };
            }
        }
        #endregion

        #region UpdateAsync
        public Task<Status_Application> UpdateAsync(tbClassSchool classSchool)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Select.Teacher;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Teacher_Repository
{
    public class Teacher_Repository_v1 : ITeacher_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public Teacher_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }
        #region Select_All_Teacher
        public async Task<List<Select_All_Teacher_v1>> Select_All_Teacher(int id_School)
        {
            var list = await (from m in _db.tbMenberSchool
                              where m.id_School == id_School
                              join r in _db.tbRoleSchool
                              on m.id_RoleSchool equals r.id_RoleSchool
                              where r.name_Role == "teacher"
                              join a in _db.tbAccount
                              on m.id_Account equals a.id_Account
                              join k in _db.tbKhoaSchool
                              on m.id_KhoaSchool equals k.id_KhoaSchool
                              select new Select_All_Teacher_v1
                              {
                                  id_Teacher = m.id_MenberSchool,
                                  user_Name = a.user_Name,
                                  fullName = a.fullName,    
                                  ma_Khoa = k.ma_Khoa,
                                  name_Khoa = k.name_Khoa,
                                  image_User = a.image_User,
                              }).ToListAsync();
            return list;
        }
        #endregion
    }
}

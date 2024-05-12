using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
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
        public async Task<List<Select_All_Teacher_v1>> Select_All_Teacher(tbMenberSchool menberManager)
        {
            IQueryable<tbMenberSchool> query = _db.tbMenberSchool;

            if (menberManager.id_KhoaSchool != 0)
            {
                query = query.Where(m => m.id_KhoaSchool == menberManager.id_KhoaSchool);
            }

            var list = await (from m in query
                              where m.id_School == menberManager.id_School
                              join r in _db.tbRoleSchool on m.id_RoleSchool equals r.id_RoleSchool
                              where r.name_Role == "teacher"
                              join a in _db.tbAccount on m.id_Account equals a.id_Account
                              join k in _db.tbKhoaSchool on m.id_KhoaSchool equals k.id_KhoaSchool
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

        #region Select_One_Teacher
        public async Task<Select_One_Teacher_v1> Select_One_Teacher(tbMenberSchool menberManager, int id_Teacher)
        {
            var teacher = await (from m in _db.tbMenberSchool
                                 where m.id_MenberSchool == id_Teacher && m.id_School == menberManager.id_School
                                 join k in _db.tbKhoaSchool
                                 on m.id_KhoaSchool equals k.id_KhoaSchool
                                 join ac in _db.tbAccount
                                 on m.id_Account equals ac.id_Account
                                 select new Select_One_Teacher_v1
                                 {
                                     id_Teacher = m.id_MenberSchool,
                                     user_Name = ac.user_Name,
                                     fullName = ac.fullName,
                                     image_User = ac.image_User,
                                     ma_Khoa = k.ma_Khoa,
                                     name_Khoa = k.name_Khoa,
                                     tag = (from tag in _db.tbTag
                                            where tag.id_Tag == m.id_Tag
                                            select tag.name).FirstOrDefault(),
                                 }).FirstOrDefaultAsync();
            return teacher!;
        }
        #endregion
    }
}

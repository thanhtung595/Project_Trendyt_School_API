using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Student;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Student_Repository
{
    public class Student_Repository_v1 : IStudent_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public Student_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }
        public async Task<List<Student_Select_v1>> SelectAllAsync(tbMenberSchool menberKhoaManager)
        {
            IQueryable<tbMenberSchool> query = _db.tbMenberSchool;

            if (menberKhoaManager.id_KhoaSchool != 0)
            {
                query = query.Where(x => x.id_KhoaSchool == menberKhoaManager.id_KhoaSchool);
            }


            var list = await (from m in query
                              where m.id_KhoaSchool == menberKhoaManager.id_KhoaSchool
                              join k in _db.tbKhoaSchool
                              on m.id_KhoaSchool equals k.id_KhoaSchool
                              join r in _db.tbRoleSchool
                              on m.id_RoleSchool equals r.id_RoleSchool
                              where r.name_Role == "student"
                              join ac in _db.tbAccount
                              on m.id_Account equals ac.id_Account
                              select new Student_Select_v1
                              {
                                  id_Student = m.id_MenberSchool,
                                  ma_Khoa = k.ma_Khoa,
                                  name_Khoa = k.name_Khoa,
                                  user_Name = ac.user_Name,
                                  fullName = ac.fullName,
                                  sex_User = ac.sex_User,
                                  email_User = ac.email_User,
                                  phone_User = ac.phone_User,
                                  image_User = ac.image_User,
                              }).ToListAsync();
            return list;
        }
    }
}

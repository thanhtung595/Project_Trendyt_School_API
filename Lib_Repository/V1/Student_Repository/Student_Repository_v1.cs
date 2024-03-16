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
                query = query.Where(m => m.id_KhoaSchool == menberKhoaManager.id_KhoaSchool);
            }

            var list = await (from m in query
                              where m.id_School == menberKhoaManager.id_School
                              join r in _db.tbRoleSchool on m.id_RoleSchool equals r.id_RoleSchool
                              where r.name_Role == "student"
                              join a in _db.tbAccount on m.id_Account equals a.id_Account
                              join k in _db.tbKhoaSchool on m.id_KhoaSchool equals k.id_KhoaSchool
                              join cm in _db.tbClassSchool_Menber on m.id_MenberSchool equals cm.id_MenberSchool into cmGroup
                              from cm in cmGroup.DefaultIfEmpty()
                              join cl in _db.tbClassSchool on cm.id_ClassSchool equals cl.id_ClassSchool  into clGroup
                              from cl in clGroup.DefaultIfEmpty()
                              select new Student_Select_v1
                              {
                                  id_Student = m.id_MenberSchool,
                                  ma_Khoa = k.ma_Khoa,
                                  name_Khoa = k.name_Khoa,
                                  user_Name = a.user_Name,
                                  fullName = a.fullName,
                                  sex_User = a.sex_User,
                                  email_User = a.email_User,
                                  phone_User = a.phone_User,
                                  image_User = a.image_User,
                                  id_Class = cl != null ? cl.id_ClassSchool : 0,
                                  name_Class = cl != null ? cl.name_ClassSchool :"Chưa vào lớp",
                              }).ToListAsync();
            return list;
        }
    }
}

using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Menber;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Menber_Repository
{
    public class Menber_Repository_v1 : IMenber_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public Menber_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }

        #region Select All Menber Async
        public async Task<List<Menber_SclectAll_v1>> SelectAllAsync(int id_School)
        {
            var list = await (from m in _db.tbMenberSchool
                              where m.id_School == id_School
                              join a in _db.tbAccount
                              on m.id_Account equals a.id_Account
                              join r in _db.tbRoleSchool
                              on m.id_RoleSchool equals r.id_RoleSchool
                              join k in _db.tbKhoaSchool
                              on m.id_KhoaSchool equals k.id_KhoaSchool into kj
                              from k in kj.DefaultIfEmpty() // Sử dụng DefaultIfEmpty để xử lý trường hợp null
                              select new Menber_SclectAll_v1
                              {
                                  id_MenberSchool = m.id_MenberSchool,
                                  user_Name = a.user_Name,
                                  fullName = a.fullName,
                                  birthday_User = a.birthday_User,
                                  sex_User = a.sex_User,
                                  email_User = a.email_User,
                                  phone_User = a.phone_User,
                                  danhGiaTb = m.danhGiaTb,
                                  tags = m.tags,
                                  name_RoleSchool = r.name_Role,
                                  name_KhoaSchool = k != null ? k.name_Khoa : "" // Sử dụng điều kiện để kiểm tra null
                              }).ToListAsync();
            return list;
        }
        #endregion

        #region InsertAsync
        public async Task<Status_Application> InsertAsync(tbMenberSchool menberSchool)
        {
			try
			{
                await _db.tbMenberSchool.AddAsync(menberSchool);
                await _db.SaveChangesAsync();
                return new Status_Application
                {
                    StatusBool = true,
                    StatusType = "success"
                };
			}
			catch (Exception ex)
			{
                return new Status_Application { StatusBool = false , StatusType = $"error menber repository {ex}" };
			}
        }
        #endregion
    }
}

using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.School;
using Lib_Models.Models_Select.Menber;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<List<Menber_SclectAll_v1>> SelectAllAsync(tbMenberSchool menberManager)
        {
            IQueryable<tbMenberSchool> query = _db.tbMenberSchool;
            if (menberManager.id_KhoaSchool != 0)
            {
                query = query.Where(x => x.id_KhoaSchool == menberManager.id_KhoaSchool);
            }

            var list = await (from m in query
                              where m.id_School == menberManager.id_School
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
                                  imageUser = a.image_User,
                                  email_User = a.email_User,
                                  phone_User = a.phone_User,
                                  danhGiaTb = m.danhGiaTb,
                                  tags = (from tag in _db.tbTag
                                          where tag.id_Tag == m.id_Tag
                                          select tag.name).FirstOrDefault(),
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
                return new Status_Application { StatusBool = false, StatusType = $"error menber repository {ex}" };
            }
        }
        #endregion

        #region School Menber Update Async
        public async Task<Status_Application> SchoolMenberUpdateAsync(School_Menber_Update_v1 request)
        {
            try
            {
                // Check tag
                if (string.IsNullOrEmpty(request.tags))
                {
                    request.tags = "active";
                }

                // Check is menber
                var menber = await _db.tbMenberSchool.FindAsync(request.id_MenberSchool);
                if (menber == null)
                {
                    return new Status_Application { StatusBool = false, StatusType = "Menber không tồn tại" };
                }

                // Check is khoa
                if (request.id_KhoaSchool != 0)
                {
                    var khoa = await _db.tbKhoaSchool.FindAsync(request.id_KhoaSchool);
                    if (khoa == null)
                    {
                        return new Status_Application { StatusBool = false, StatusType = "Khoa không tồn tại" };
                    }
                    menber.id_KhoaSchool = khoa.id_KhoaSchool;
                }
                else
                {
                    menber.id_KhoaSchool = 0;
                }

                // check role menber
                var role = await _db.tbRoleSchool.FirstOrDefaultAsync(x => x.name_Role!.ToLower() == request.chuc_vu!.ToLower());
                if (role == null)
                {
                    return new Status_Application { StatusBool = false, StatusType = "Role không tồn tại" };
                }
                else
                {
                    menber.id_RoleSchool = role.id_RoleSchool;
                }

                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true, StatusType = "success" };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "error " + ex.Message };
            }
        }

        #endregion

        #region Profile
        public async Task<Member_Profile_v1> Profile(int id_Member)
        {
            var member = await (from m in _db.tbMenberSchool
                                where m.id_Account == id_Member
                                join a in _db.tbAccount
                                on m.id_Account equals a.id_Account
                                join r in _db.tbRoleSchool
                                on m.id_RoleSchool equals r.id_RoleSchool
                                join s in _db.tbSchool
                                on m.id_School equals s.id_School
                                join k in _db.tbKhoaSchool
                                on m.id_KhoaSchool equals k.id_KhoaSchool into kj
                                from k in kj.DefaultIfEmpty() // Sử dụng DefaultIfEmpty để xử lý trường hợp null
                                select new Member_Profile_v1
                                {
                                    id_MenberSchool = m.id_MenberSchool,
                                    user_Name = a.user_Name,
                                    fullName = a.fullName,
                                    birthday_User = a.birthday_User,
                                    sex_User = a.sex_User,
                                    imageUser = a.image_User,
                                    email_User = a.email_User,
                                    phone_User = a.phone_User,
                                    danhGiaTb = m.danhGiaTb,
                                    tags = (from tag in _db.tbTag
                                            where tag.id_Tag == m.id_Tag
                                            select tag.name).FirstOrDefault(),
                                    chuc_vu = r.name_Role,
                                    school = s.name_School,
                                    name_KhoaSchool = k != null ? k.name_Khoa : "" // Sử dụng điều kiện để kiểm tra null
                                }).FirstOrDefaultAsync();

            return member!;
        }
        #endregion

        #region Delete
        public async Task<Status_Application> Delete(tbMenberSchool menberSchool)
        {
            try
            {
                menberSchool.id_Tag = (from tag in _db.tbTag
                                       where tag.name == "delete"
                                       select tag.id_Tag).FirstOrDefault();
                await _db.SaveChangesAsync();
                return new Status_Application
                {
                    StatusBool = true,
                    StatusType = "success"
                };
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "error " + ex.Message };
            }
        }
        #endregion
    }
}

using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Models_Select.Teacher;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.V1.MonHoc_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.MonHoc
{
    public class MonHoc_Repository_v1 : IMonHoc_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public MonHoc_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }

        public async Task<List<MonHoc_SelectAll_v1>> GetAll(int idSchool)
        {
            var list = await (from mh in _db.tbMonHoc
                              where mh.id_School == idSchool
                              select new MonHoc_SelectAll_v1
                              {
                                  id_MonHoc = mh.id_MonHoc,
                                  name_MonHoc = mh.name_MonHoc,
                                  danhGiaTrungBinh = mh._danhGiaTrungBinh,
                                  tag = mh.tags,
                                  soBuoiNghi = mh._SoBuoiNghi,
                                  ngayBatDau = mh.ngayBatDau,
                                  ngayKetThuc = mh.ngayKetThuc,
                                  giangvien = (from st in _db.tbMonHocClass_Student
                                               where st.id_MonHoc == mh.id_MonHoc
                                               join m in _db.tbMenberSchool
                                               on st.id_MenberSchool equals m.id_MenberSchool
                                               join r in _db.tbRoleSchool
                                               on m.id_RoleSchool equals r.id_RoleSchool
                                               where r.name_Role == "teacher"
                                               join ac in _db.tbAccount
                                               on m.id_Account equals ac.id_Account
                                               join k in _db.tbKhoaSchool
                                               on m.id_KhoaSchool equals k.id_KhoaSchool
                                               select new Select_One_Teacher_v1
                                               {
                                                   id_Teacher = m.id_MenberSchool,
                                                   user_Name = ac.user_Name,
                                                   fullName = ac.fullName,
                                                   name_Khoa = k.name_Khoa,
                                                   ma_Khoa = k.ma_Khoa,
                                                   image_User = ac.image_User,
                                                   tag = m.tags
                                               }).FirstOrDefault(),

                                  coutnStudent = (from st in _db.tbMonHocClass_Student
                                                  where st.id_MonHoc == mh.id_MonHoc
                                                  join m in _db.tbMenberSchool
                                                  on st.id_MenberSchool equals m.id_MenberSchool
                                                  join r in _db.tbRoleSchool
                                                  on m.id_RoleSchool equals r.id_RoleSchool
                                                  where r.name_Role == "student"
                                                  select st).Count()
                              }).ToListAsync();
            return list;
        }

        public async Task<Status_Application> Insert(tbMonHoc monHoc)
        {
            try
            {
                await _db.tbMonHoc.AddAsync(monHoc);
                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true ,StatusType = "success",Id_Int = monHoc.id_MonHoc};
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false , StatusType = "error"+ex.Message };
            }
        }
    }
}

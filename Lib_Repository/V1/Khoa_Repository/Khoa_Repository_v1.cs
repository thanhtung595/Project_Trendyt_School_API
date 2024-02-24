using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.Khoa;
using Lib_Models.Models_Select.Khoa;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Khoa_Repository
{
    public class Khoa_Repository_v1 : IKhoa_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public Khoa_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }

        #region Select All 
        public async Task<List<KhoaSchool_Select_v1>> SelectAll(int id_School)
        {
            var list = await (from k in _db.tbKhoaSchool
                              where k.id_School == id_School
                              select new KhoaSchool_Select_v1
                              {
                                  id_KhoaSchool = k.id_KhoaSchool,
                                  ma_Khoa = k.ma_Khoa,
                                  name_Khoa = k.name_Khoa
                              }).ToListAsync();

            return list;
        }
        #endregion

        #region Insert
        public async Task<Status_Application> InsertAysnc(tbKhoaSchool khoaSchool)
        {
            try
            {
                await _db.tbKhoaSchool.AddAsync(khoaSchool);
                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true, StatusType = "success"};
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false , StatusType = "error"+ex.Message };
            }
        }
        #endregion

        #region Update
        public async Task<Status_Application> UpdateAysnc(KhoaSchool_Update_v1 khoaSchool)
        {
            try
            {
                tbKhoaSchool? tbKhoa = await _db.tbKhoaSchool.FindAsync(khoaSchool.id_KhoaSchool);
                tbKhoa!.name_Khoa = khoaSchool.name_Khoa;
                tbKhoa.ma_Khoa = khoaSchool.ma_Khoa;

                await _db.SaveChangesAsync();
                return new Status_Application { StatusBool = true, StatusType = "success"};
            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "error" + ex.Message };
            }
        }
        #endregion
    }
}

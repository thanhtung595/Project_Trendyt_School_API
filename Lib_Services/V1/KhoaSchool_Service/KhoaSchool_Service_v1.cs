using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.Khoa;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.Khoa;
using Lib_Models.Status_Model;
using Lib_Repository.V1.Khoa_Repository;
using Lib_Services.Token_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.KhoaSchool_Service
{
    public class KhoaSchool_Service_v1 : IKhoaSchool_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly IToken_Service_v1 _tokenService_V1;
        private readonly IKhoa_Repository_v1 _khoa_Repository_V1;
        public KhoaSchool_Service_v1(Trendyt_DbContext db, IToken_Service_v1 token_Service_V1,
            IKhoa_Repository_v1 khoa_Repository_V1)
        {
            _db = db;
            _tokenService_V1 = token_Service_V1;
            _khoa_Repository_V1 = khoa_Repository_V1;
        }

        #region Select All 
        public async Task<List<KhoaSchool_Select_v1>> SelectAll()
        {
            // Lấy id_school
            int id_Manager_Menber = await _tokenService_V1.GetAccessTokenIdAccount();
            var menberManager = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == id_Manager_Menber);
            return await _khoa_Repository_V1.SelectAll(menberManager!.id_School);
        }
        #endregion

        #region Insert
        public async Task<Status_Application> InsertAysnc(KhoaSchool_Insert_v1 reques)
        {
            reques.ma_Khoa = reques.ma_Khoa!.Trim();
            reques.name_Khoa = reques.name_Khoa!.Trim();
            // Check null value
            if (string.IsNullOrEmpty(reques.name_Khoa))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập name khoa" };
            }
            if (string.IsNullOrEmpty(reques.ma_Khoa))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập mã khoa" };
            }

            // Lấy id_school
            int id_Manager_Menber = await _tokenService_V1.GetAccessTokenIdAccount();
            var menberManager = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == id_Manager_Menber);

            // Check name khoa da ton tai
            var isNameKhoa = await _db.tbKhoaSchool.FirstOrDefaultAsync(x => x.id_School == menberManager!.id_School 
                && x.name_Khoa!.ToLower() == reques.name_Khoa.ToLower());
            if (isNameKhoa != null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Name khoa đã tồn tại" };
            }

            // Check ma khoa da ton tai
            var isMaKhoa = await _db.tbKhoaSchool.FirstOrDefaultAsync(x => x.id_School == menberManager!.id_School
                && x.ma_Khoa!.ToLower() == reques.ma_Khoa.ToLower());
            if (isMaKhoa != null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Mã khoa đã tồn tại" };
            }

            // Add
            tbKhoaSchool khoaSchool = new tbKhoaSchool
            {
                ma_Khoa = reques.ma_Khoa,
                name_Khoa = reques.name_Khoa,
                id_School = menberManager!.id_School
            };
            return await _khoa_Repository_V1.InsertAysnc(khoaSchool);
        }
        #endregion

        #region Update
        public async Task<Status_Application> UpdateAysnc(KhoaSchool_Update_v1 reques)
        {
            reques.ma_Khoa = reques.ma_Khoa!.Trim();
            reques.name_Khoa = reques.name_Khoa!.Trim();
            // Check null value
            if (string.IsNullOrEmpty(reques.name_Khoa))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập name khoa" };
            }
            if (string.IsNullOrEmpty(reques.ma_Khoa))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập mã khoa" };
            }

            // Check khoa có tồn tại
            var iskhoa = await _db.tbKhoaSchool.FindAsync(reques.id_KhoaSchool);
            if (iskhoa == null)
            {
                return new Status_Application { StatusBool = false, StatusType = "Khoa không tồn tại" };
            }

            // Lấy id_school
            int id_Manager_Menber = await _tokenService_V1.GetAccessTokenIdAccount();
            var menberManager = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == id_Manager_Menber);

            // Kiểm tra name khoa có trùng
            var isNameKhoa = await _db.tbKhoaSchool.AnyAsync(x => x.id_School == menberManager!.id_School 
                && x.id_KhoaSchool != reques.id_KhoaSchool && x.name_Khoa!.ToLower() == reques.name_Khoa.ToLower());

            if (isNameKhoa)
            {
                return new Status_Application { StatusBool = false, StatusType = "Name khoa đã tồn tại" };
            }

            // Kiểm tra mã khoa có trùng
            var isMaKhoa = await _db.tbKhoaSchool.AnyAsync(x => x.id_School == menberManager!.id_School
                && x.id_KhoaSchool != reques.id_KhoaSchool && x.ma_Khoa!.ToLower() == reques.ma_Khoa.ToLower());

            if (isMaKhoa)
            {
                return new Status_Application { StatusBool = false, StatusType = "Name khoa đã tồn tại" };
            }

            return await _khoa_Repository_V1.UpdateAysnc(reques);
        }
        #endregion
    }
}

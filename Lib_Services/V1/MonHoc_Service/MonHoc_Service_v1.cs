using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.V1.MonHoc_Repository;
using Lib_Services.Token_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.MonHoc
{
    public class MonHoc_Service_v1 : IMonHoc_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly IToken_Service_v1 _tokenService_V1;
        private readonly IMonHoc_Repository_v1 _monHoc_Repository_V1;
        public MonHoc_Service_v1(Trendyt_DbContext db, IToken_Service_v1 tokenService_V1,
            IMonHoc_Repository_v1 monHoc_Repository_V1)
        {
            _db = db;
            _tokenService_V1 = tokenService_V1;
            _monHoc_Repository_V1 = monHoc_Repository_V1;
        }

        public async Task<List<MonHoc_SelectAll_v1>> GetAll()
        {
            tbMenberSchool menberManager = await _tokenService_V1.Get_Menber_Token();
            return await _monHoc_Repository_V1.GetAll(menberManager.id_School);
        }

        public async Task<Status_Application> Insert(tbMonHoc monHoc)
        {
            tbMenberSchool menberManager = await _tokenService_V1.Get_Menber_Token();
            // Check null data
            if (string.IsNullOrEmpty(monHoc.name_MonHoc))
            {
                return new Status_Application { StatusBool = false, StatusType = "Chưa nhập tên môn học" };
            }

            // Kiểm tra tên môn học đã tồn tại
            bool isNameMonHoc = await _db.tbMonHoc.AnyAsync(x => x.id_School == menberManager.id_School 
                                && x.name_MonHoc!.ToLower() == monHoc.name_MonHoc.ToLower());

            if (isNameMonHoc)
            {
                return new Status_Application { StatusBool = false, StatusType = "Tên môn học đã tồn tại" };
            }

            tbMonHoc monHocAdd = new tbMonHoc
            {
                id_School = menberManager.id_School,
                name_MonHoc = monHoc.name_MonHoc.Trim(),
                _danhGiaTrungBinh = 5,
                tags = "active",
                _SoBuoiNghi = monHoc._SoBuoiNghi,
                ngayBatDau = monHoc.ngayBatDau,
                ngayKetThuc = monHoc.ngayKetThuc
            };

            return await _monHoc_Repository_V1.Insert(monHocAdd);
        }
    }
}

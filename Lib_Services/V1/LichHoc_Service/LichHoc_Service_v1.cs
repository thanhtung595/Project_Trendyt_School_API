using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Models_Select.LichHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Repository.V1.LichHoc_Repository;
using Lib_Services.Token_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.LichHoc_Service
{
    public class LichHoc_Service_v1 : ILichHoc_Service_v1
    {
        //private readonly Trendyt_DbContext _db;
        //private readonly ILichHoc_Repository_v1 _lichHoc_Repository_V1;
        //private readonly IToken_Service_v2 _tokenService_v2;
        //public LichHoc_Service_v1(Trendyt_DbContext db, ILichHoc_Repository_v1 lichHoc_Repository_V1, IToken_Service_v2 token_Service_V2)
        //{
        //    _db = db;
        //    _lichHoc_Repository_V1 = lichHoc_Repository_V1;
        //    _tokenService_v2 = token_Service_V2;
        //}
        //public async Task<Status_Application> Insert(LichHoc_Insert_v1 lichHoc)
        //{
        //    var islichhoc = await _db.tbMonHoc.FindAsync(lichHoc.id_MonHoc);
        //    if (islichhoc == null)
        //    {
        //        return new Status_Application { StatusBool = false, StatusType = "Môn học không tồn tại" };
        //    }

        //    if (string.IsNullOrEmpty(lichHoc.phonghoc))
        //    {
        //        return new Status_Application { StatusBool = false, StatusType = "Chưa nhập phòng học" };
        //    }

        //    var checkTrungBuoiHoc = await _db.tbLichHoc.FirstOrDefaultAsync(x =>x.id_MonHoc == islichhoc.id_MonHoc &&
        //        x.thoiGianKetThuc == lichHoc.thoiGianKetThuc && x.thoiGianBatDau == lichHoc.thoiGianBatDau);
        //    if (checkTrungBuoiHoc != null)
        //    {
        //        return new Status_Application { StatusBool = false, StatusType = $"Buổi học thời thời gian băt đầu:{lichHoc.thoiGianBatDau} và kết thúc: {lichHoc.thoiGianKetThuc} bị trùng" }; 
        //    }
        //    return await _lichHoc_Repository_V1.Insert(lichHoc);
        //}

        //public async Task<List<LichHoc_Select_All_v1>> SelectAll()
        //{
        //    var member = await _tokenService_v2.Get_Menber_Token();
        //    return await _lichHoc_Repository_V1.SelectAll(member);
        //}
        public Task<Status_Application> Insert(LichHoc_Insert_v1 lichHoc)
        {
            throw new NotImplementedException();
        }

        public Task<List<LichHoc_Select_All_v1>> SelectAll()
        {
            throw new NotImplementedException();
        }
    }
}

using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Models_Select.LichHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Services.V1.LichHoc_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Objects;
using System.Linq;
using TrendyT_Data.Identity;

namespace API_Application.Controllers_School_Api.v1.LichHoc
{
    [Route(RouterName.RouterControllerName.LichHoc)]
    [ApiController]
    public class LichHocController : ControllerBase
    {
        private readonly Trendyt_DbContext _db;
        private readonly ILichHoc_Service_v1 _lichHoc_Service_V1;
        public LichHocController(Trendyt_DbContext db, ILichHoc_Service_v1 lichHoc_Service_V1)
        {
            _db = db;
            _lichHoc_Service_V1 = lichHoc_Service_V1;
        }

        [Authorize(Policy = IdentityData.ScuritySchool)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           
            return Ok(await _lichHoc_Service_V1.SelectAll());
        }



        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpPost]
        public async Task<IActionResult> Insert(List<LichHoc_Insert_v1> lichHocs)
        {
            int count_lichHocs = lichHocs.Count();
            int count_SoBuoiHoc;
            var monHocDb = await _db.tbMonHoc.FindAsync(lichHocs[0].id_MonHoc);
            if (monHocDb == null)
            {
                return StatusCode(400, "Môn học không tồn tại");
            }
            count_SoBuoiHoc = monHocDb!._SoBuoiHoc;
            var lichHocDb = await _db.tbLichHoc.FirstOrDefaultAsync(x => x.id_MonHoc == monHocDb.id_MonHoc);
            if (lichHocDb != null)
            {
                return StatusCode(400, "Môn học này đã đủ lịch học");
            }
            if (count_lichHocs != count_SoBuoiHoc)
            {
                return StatusCode(400, $"Số buổi học của môn này là: {count_SoBuoiHoc} hiện tại đang thêm có: {count_lichHocs}");
            }

            var executionStrategy = _db.Database.CreateExecutionStrategy();

            IActionResult result = null!;

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in lichHocs)
                        {
                            Status_Application status_add = await _lichHoc_Service_V1.Insert(item);
                            if (!status_add.StatusBool)
                            {
                                // Nếu có lỗi, rollback giao dịch và trả về lỗi
                                dbContextTransaction.Rollback();
                                result = StatusCode(400, status_add.StatusType);
                                return;
                            }
                        }
                        // Nếu mọi thứ thành công, commit giao dịch
                        dbContextTransaction.Commit();
                        result = StatusCode(201);
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, rollback giao dịch và trả về lỗi
                        dbContextTransaction.Rollback();
                        result = StatusCode(500, $"Error: {ex.Message}");
                    }
                }
            });

            return result!;
        }
    }
}

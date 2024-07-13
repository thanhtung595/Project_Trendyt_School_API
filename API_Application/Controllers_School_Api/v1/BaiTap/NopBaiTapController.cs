using API_Application.Service.FileService;
using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Model_Update.BaiTap;
using Lib_Models.Models_Insert.v2.BaiTap;
using Lib_Models.Status_Model;
using Lib_Services.V2.NopBaiTap_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.BaiTap
{
    [Route(RouterName.RouterControllerName.BaiTap)]
    [ApiController]
    public class NopBaiTapController : ControllerBase
    {
        private readonly INopBaiTap_Service_v2 _nopBaiTap_Service_V2;
        private readonly Trendyt_DbContext _trendytDbContext;
        public NopBaiTapController(INopBaiTap_Service_v2 nopBaiTap_Service_V2, Trendyt_DbContext trendytDbContext)
        {
            _nopBaiTap_Service_V2 = nopBaiTap_Service_V2;
            _trendytDbContext = trendytDbContext;
        }

        [HttpGet]
        [Route("da-nop")]
        [Authorize(Policy = IdentityData.ScuritySchool)]
        public async Task<IActionResult> GetBaiTapNop([FromQuery] int idBaiTap)
        {
            return Ok(await _nopBaiTap_Service_V2.GetAll(idBaiTap));
        }

        [Authorize(Policy = IdentityData.StudentPolicyName)]
        [HttpPost]
        [Route("nopbai")]
        public async Task<IActionResult> Add([FromForm] NopBaiTap_Insert_v2 baiTap)
        {
            if (baiTap.files!.Count() < 1)
            {
                return StatusCode(400, "Chưa thêm file bài tập");
            }
            Status_Application status = await _nopBaiTap_Service_V2.Add(baiTap);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }

            if (baiTap.files!.Count() > 0)
            {
                FileInFolder.AddFileBaiTapInFolder_FileSrc(baiTap.files!, status.List_String_Int!);
            }
            return StatusCode(201);
        }

        [Authorize(Policy = IdentityData.TeacherPolicyName)]
        [HttpPut]
        [Route("cham-diem")]
        public async Task<IActionResult> ChamDiem(ChamDiemModel chamDiem)
        {
            await _nopBaiTap_Service_V2.ChamDiem(chamDiem);
            return StatusCode(204);
        }
    }
}

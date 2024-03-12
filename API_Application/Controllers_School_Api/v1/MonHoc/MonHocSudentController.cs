using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Services.V1.MonHoc_Student_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.MonHoc
{
    [Route("api/v1/mon-hoc/student")]
    [ApiController]
    public class MonHocSudentController : ControllerBase
    {
        private readonly Trendyt_DbContext _db;
        private readonly IMonHoc_Student_Service_v1 _monHoc_Student_Service_V1;
        public MonHocSudentController(Trendyt_DbContext db, IMonHoc_Student_Service_v1 monHoc_Student_Service_V1)
        {
            _db = db;
            _monHoc_Student_Service_V1 = monHoc_Student_Service_V1;
        }

        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpPost]
        public async Task<IActionResult> AddStudent()
        {
            return Ok();
        }
    }
}

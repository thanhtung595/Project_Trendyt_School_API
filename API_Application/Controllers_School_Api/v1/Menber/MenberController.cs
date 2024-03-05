using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Model_Update.School;
using Lib_Models.Status_Model;
using Lib_Services.V1.Menber_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.Menber
{
    [Route("api/v1/menber")]
    [ApiController]
    public class MenberController : ControllerBase
    {
        private readonly IMenber_Service_v1 _menber_Service_V1;
        private readonly Trendyt_DbContext _db;
        public MenberController(IMenber_Service_v1 menber_Service_V1, Trendyt_DbContext db)
        {
            _menber_Service_V1 = menber_Service_V1;
            _db = db;
        }

        // Menber school start
        #region Get all Menber School
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        public async Task<IActionResult> GetAllMenberSchool()
        {
            return Ok(await _menber_Service_V1.SelectAllAsync());
        }
        #endregion

        #region Edit Menber School
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpPut]
        public async Task<IActionResult> EditMenberSchool(School_Menber_Update_v1 request)
        {
            Status_Application status = await _menber_Service_V1.SchoolMenberUpdateAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion
    }
}

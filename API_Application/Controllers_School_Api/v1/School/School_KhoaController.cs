using Lib_Models.Model_Update.Khoa;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using Lib_Services.V1.KhoaSchool_Service;
using Lib_Services.V1.School_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.School
{
    [Route("api/v1/school/khoa")]
    [ApiController]
    public class School_KhoaController : ControllerBase
    {
        private readonly IKhoaSchool_Service_v1 _khoaSchool_Service_V1;
        public School_KhoaController(IKhoaSchool_Service_v1 khoaSchool_Service_V1)
        {
            _khoaSchool_Service_V1 = khoaSchool_Service_V1;
        }

        // Khoa school start
        #region Get All Khoa School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpGet]
        public async Task<IActionResult> GellAllKhoaSchool()
        {
            return Ok(await _khoaSchool_Service_V1.SelectAll());
        }
        #endregion

        #region Add Khoa School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPost]
        public async Task<IActionResult> AddKhoaSchool(KhoaSchool_Insert_v1 reques)
        {
            Status_Application status = await _khoaSchool_Service_V1.InsertAysnc(reques);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(201);
        }
        #endregion

        #region Edit Khoa School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPut]
        public async Task<IActionResult> EditKhoaSchool(KhoaSchool_Update_v1 khoaSchool)
        {
            Status_Application status = await _khoaSchool_Service_V1.UpdateAysnc(khoaSchool);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion
        // Khoa school end
    }
}

using Lib_Models.Model_Update.School;
using Lib_Models.Status_Model;
using Lib_Services.V1.School_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1
{
    [Route("api/v1/school")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchool_Service_v1 _school_Service_v1;
        public SchoolController(ISchool_Service_v1 school_Service_V1)
        {
            _school_Service_v1 = school_Service_V1;
        }

        #region Get All School
        [HttpGet]
        public async Task<IActionResult> GetAllSchool()
        {
            return Ok(await _school_Service_v1.SelectAllAsync());
        }
        #endregion

        #region Edit School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPut]        
        public async Task<IActionResult> EditSchool(School_Update_v1 request)
        {
            Status_Application status = await _school_Service_v1.UpdateAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion
    }
}

using Lib_Services.V1.Teacher_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.School
{
    [Route("api/v1/school/teacher")]
    [ApiController]
    public class School_TeacherController : ControllerBase
    {
        private readonly ITeacher_Service_v1 _teacher_Service_V1;
        public School_TeacherController(ITeacher_Service_v1 teacher_Service_V1)
        {
            _teacher_Service_V1 = teacher_Service_V1;
        }

        #region Get All Teacher
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpGet]
        public async Task<IActionResult> GetAllTeacher()
        {
            return Ok(await _teacher_Service_V1.Select_All_Teacher());
        }
        #endregion

        #region Show One Teacher
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpGet]
        [Route("show")]
        public async Task<IActionResult> ShowOneTeacher([FromQuery(Name = "id_teacher")] int id_teacher) 
        {
            return Ok();
        }
        #endregion

        #region Edit Teacher
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPut]
        public async Task<IActionResult> EditTeacher()
        {
            return Ok();
        }
        #endregion
    }
}

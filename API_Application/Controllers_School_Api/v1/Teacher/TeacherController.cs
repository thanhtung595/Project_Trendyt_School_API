using Lib_Services.V1.Teacher_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.Teacher
{
    [Route(RouterName.RouterControllerName.Teacher)]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacher_Service_v1 _teacher_Service_V1;
        public TeacherController(ITeacher_Service_v1 teacher_Service_V1)
        {
            _teacher_Service_V1 = teacher_Service_V1;
        }

        #region Get All Teacher
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        public async Task<IActionResult> GetAllTeacher()
        {
            return Ok(await _teacher_Service_V1.Select_All_Teacher());
        }
        #endregion

        #region Show One Teacher
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        [Route("show")]
        public async Task<IActionResult> ShowOneTeacher([FromQuery(Name = "id")] int id_teacher)
        {
            return Ok(await _teacher_Service_V1.Show_One_Teacher(id_teacher));
        }
        #endregion

        #region Edit Teacher
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpPut]
        public async Task<IActionResult> EditTeacher()
        {
            return Ok();
        }
        #endregion
    }
}

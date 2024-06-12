using Lib_Services.V1.Student_Service;
using Lib_Services.V2.Student_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.Student
{
    [Route(RouterName.RouterControllerName.Student)]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent_Service_v1 _student_Service_V1;
        private readonly IStudent_Service_v2 _student_Service_V2;
        public StudentController(IStudent_Service_v1 student_Service_V1, IStudent_Service_v2 student_Service_V2)
        {
            _student_Service_V1 = student_Service_V1;
            _student_Service_V2 = student_Service_V2;
        }

        #region SelectAll
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        public async Task<IActionResult> SelectAll()
        {
            return Ok(await _student_Service_V1.SelectAllAsync());
        }
        #endregion

        #region SelectAllNotJoinClass
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        [Route("not-join-class")]
        public async Task<IActionResult> SelectAllNotJoinClass()
        {
            return Ok(await _student_Service_V2.SelectAllNotJoinClassAsync());
        }
        #endregion

        #region Edit
        #endregion

        #region Show one
        #endregion
    }
}

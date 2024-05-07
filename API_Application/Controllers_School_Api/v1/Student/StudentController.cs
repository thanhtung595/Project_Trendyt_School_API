using Lib_Services.V1.Student_Service;
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
        public StudentController(IStudent_Service_v1 student_Service_V1)
        {
            _student_Service_V1 = student_Service_V1;
        }

        #region SelectAll
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        public async Task<IActionResult> SelectAll()
        {
            return Ok(await _student_Service_V1.SelectAllAsync());
        }
        #endregion

        #region Edit
        #endregion

        #region Show one
        #endregion
    }
}

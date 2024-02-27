using Lib_Models.Status_Model;
using Lib_Services.V1.Class_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.Class_School
{
    [Route("api/v1/class")]
    [ApiController]
    public class Class_SchoolController : ControllerBase
    {
        private readonly IClass_Service_v1 _class_Service_V1;
        public Class_SchoolController(IClass_Service_v1 class_Service_V1)
        {
            _class_Service_V1 = class_Service_V1;
        }

        #region Show All
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        public async Task<IActionResult> ShowAll()
        {
            return Ok(await _class_Service_V1.SelectAll());
        }
        #endregion

        #region Show one
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        [Route("show")]
        public async Task<IActionResult> ShowOne([FromQuery(Name = "id_ClassSchool")] int id_ClassSchool)
        {
            return Ok();
        }
        #endregion

        #region Add class
        [Authorize(Policy = IdentityData.QuanLyKhoaManager)]
        [HttpPost]
        public async Task<IActionResult> AddClas([FromBody] string name_ClassSchool)
        {
            Status_Application status = await _class_Service_V1.InsertAsync(name_ClassSchool);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(201);
        }
        #endregion

        #region Editclass
        [Authorize(Policy = IdentityData.QuanLyKhoaManager)]
        [HttpPut]
        public async Task<IActionResult> EditClas([FromBody] string name_ClassSchool)
        {
            Status_Application status = await _class_Service_V1.InsertAsync(name_ClassSchool);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion
    }
}

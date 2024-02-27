using Lib_Models.Status_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.Class_School
{
    [Route("api/v1/class/menber")]
    [ApiController]
    public class ClassSchool_MenberController : ControllerBase
    {
        public ClassSchool_MenberController()
        {
            
        }

        #region Add class
        [Authorize(Policy = IdentityData.QuanLyKhoaManager)]
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddClas([FromBody] string name_ClassSchool)
        {
            //Status_Application status = await _class_Service_V1.InsertAsync(name_ClassSchool);
            //if (!status.StatusBool)
            //{
            //    return StatusCode(400, status.StatusType);
            //}
            return StatusCode(201);
        }
        #endregion
    }
}

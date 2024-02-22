using Lib_Models.Model_Insert.v1;
using Lib_Models.Model_Update.Role;
using Lib_Models.Model_Update.RoleSchool;
using Lib_Models.Status_Model;
using Lib_Services.V1.Role_Service;
using Lib_Services.V1.RoleSchool_Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Areas.Admin.Controllers_Api.v1
{
    [Route("api/admin/v1/role-school")]
    [ApiController]
    public class RoleSchoolController : ControllerBase
    {
        private readonly IRoleSchool_Service_v1 _roleSchool_Service_V1;
        public RoleSchoolController(IRoleSchool_Service_v1 roleSchool_Service_V1)
        {
            _roleSchool_Service_V1 = roleSchool_Service_V1;
        }

        #region Select All Role School
        [HttpGet]
        public async Task<IActionResult> SelectAll()
        {
            return Ok(await _roleSchool_Service_V1.SelectAllAsync());
        }
        #endregion

        #region Insert Role School
        [HttpPost]
        public async Task<IActionResult> Insert(Role_Insert_v1 request)
        {
            Status_Application status = await _roleSchool_Service_V1.InsertAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(201);
        }
        #endregion

        #region Update Role School
        [HttpPut("update-roleschool-menber")]
        public async Task<IActionResult> Update_Role_Account(UpdateRoleSchool request)
        {
            Status_Application status = await _roleSchool_Service_V1.Update_Role_Account(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion

    }
}

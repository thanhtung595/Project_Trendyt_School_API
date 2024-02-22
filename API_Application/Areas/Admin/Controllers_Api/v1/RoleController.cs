using Lib_Models.Model_Insert.v1;
using Lib_Models.Model_Update.Role;
using Lib_Models.Status_Model;
using Lib_Services.V1.Role_Service;
using Microsoft.AspNetCore.Mvc;

namespace API_Application.Areas.Admin.Controllers_Api.v1
{
    [Route("api/admin/v1/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole_Service_v1 _roleService_v1;
        public RoleController(IRole_Service_v1 roleService_v1)
        {
            _roleService_v1 = roleService_v1;
        }

        #region SelectAll Role
        [HttpGet]
        public async Task<IActionResult> SelectAll()
        {
            return Ok(await _roleService_v1.SelectAllAsync());
        }
        #endregion

        #region Insert Role
        [HttpPost]
        public async Task<IActionResult> Insert(Role_Insert_v1 request)
        {
            Status_Application status = await _roleService_v1.InsertAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(201);
        }
        #endregion

        #region Update Role Account (Admin)
        [HttpPut("update-role-account")]
        public async Task<IActionResult> Update_Role_Account(UpdateRoleAccount request)
        {
            Status_Application status = await _roleService_v1.Update_Role_Account(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion

    }
}

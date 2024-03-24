using Lib_Models.Model_Insert.v1;
using Lib_Models.Model_Update.Role;
using Lib_Models.Status_Model;
using Lib_Services.V1.Role_Service;
using Microsoft.AspNetCore.Mvc;
using Stored_Procedures.PROC.Role;

namespace API_Application.Areas.Admin.Controllers_Api.v1
{
    [Route("api/admin/v1/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole_Service_v1 _roleService_v1;
        private readonly IPROC_tbRole _proc_tbRole;
        public RoleController(IRole_Service_v1 roleService_v1, IPROC_tbRole proc_tbRole)
        {
            _roleService_v1 = roleService_v1;
            _proc_tbRole = proc_tbRole;
        }

        #region SelectAll Role
        [HttpGet]
        public async Task<IActionResult> SelectAll()
        {
            //return Ok(await _roleService_v1.SelectAllAsync());
            return Ok(_proc_tbRole.Proc_GetAllRole());
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

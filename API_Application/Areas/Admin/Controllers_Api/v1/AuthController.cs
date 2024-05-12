using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using Lib_Services.PublicServices.Auth_Service;
using Lib_Services.V1.Register_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Areas.Admin.Controllers_Api.v1
{
    [Route(RouterName.RouterAdminControllerName.Authentication)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAtuh_Service _atuh_Service;
        public AuthController(IAtuh_Service atuh_Service)
        {
            _atuh_Service = atuh_Service;
        }

        #region Admin tạo tài khoản người dùng (không bị hạn chế)
        [Authorize(Policy = IdentityData.AdminServerPolicyName)]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserName(Register_Insert_v1 register)
        {
            Status_Application status = await _atuh_Service.RegisterUserName(register);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            else if (status.StatusBool)
            {
                return StatusCode(201);
            }
            else
            {
                return StatusCode(404, status.StatusType);
            }
        }
        #endregion
    }
}

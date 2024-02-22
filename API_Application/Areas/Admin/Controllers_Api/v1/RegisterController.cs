using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using Lib_Services.V1.Register_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Areas.Admin.Controllers_Api.v1
{
    [Route("api/admin/v1/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegister_Service_v1 _register_Service_v1;
        public RegisterController(IRegister_Service_v1 register_Service_v1)
        {
            _register_Service_v1 = register_Service_v1;
        }

        #region Admin tạo tài khoản người dùng (không bị hạn chế)
        /*
            - Truy cập access_Token - All Role
            - Gửi {user_Name , user_Password}
         */
        [Authorize(Policy = IdentityData.AdminServerPolicyName)]
        [HttpPost]
        public async Task<IActionResult> RegisterUserName(Register_Insert_v1 register)
        {
            Status_Application status = await _register_Service_v1.RegisterUserName(register);
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

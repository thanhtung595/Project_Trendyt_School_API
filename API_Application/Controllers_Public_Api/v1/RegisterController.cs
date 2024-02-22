using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using Lib_Services.V1.Register_Service;
using Microsoft.AspNetCore.Mvc;

namespace API_Application.Controllers_Public_Api.v1
{
    [Route("api/v1/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegister_Service_v1 _register_Service_v1;
        public RegisterController(IRegister_Service_v1 register_Service_v1)
        {
            _register_Service_v1 = register_Service_v1;
        }

        #region Register Account User Name
        /*
            - Truy cập access_Token - All Role
            - Gửi {user_Name , fullName , user_Password , email_User}
         */
        [HttpPost]
        public async Task<IActionResult> RegisterUserName(Register_Insert_v1 register)
        {
            //Check tài khoản đuôi edu.vn
            string edu = "edu.vn";
            if (register.user_Name!.ToLower().Contains(edu.ToLower()))
            {
                return StatusCode(400, "Không thể đăng ký tài khoản với edu.vn.");
            }
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

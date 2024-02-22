using Lib_Models.Models_Select.Account;
using Lib_Models.Models_Select.Login;
using Lib_Services.V1.Login_Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_Api.v1
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin_Service_v1 _loginService_v1;
        public LoginController(ILogin_Service_v1 login_Service_V1)
        {
            _loginService_v1 = login_Service_V1;
        }

        #region Login Account User Name
        /*
            - Truy cập access_Token - All Role
            - Gửi {user_Name , user_Password}
         */
        [HttpPost]
        public async Task<IActionResult> Login(Login_Select_v1 request)
        {
            Account_Login_Select_v1 status_Login = await _loginService_v1.LoginAsync(request);
            if (!status_Login.StatusBool)
            {
                return StatusCode(400, status_Login.StatusType);
            }
            else if (status_Login.StatusBool)
            {
                return StatusCode(200, status_Login);
            }
            else
            {
                return StatusCode(500, status_Login.StatusType);
            }
        }
        #endregion
    }
}

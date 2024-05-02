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
                // Set Cokie
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true, // chỉ có thể truy cập qua HTTP, không thể truy cập qua JavaScript
                    Secure = false, // chỉ gửi cookie qua HTTPS nếu kích hoạt
                    SameSite = SameSiteMode.Strict, // bảo mật ngăn chặn CSRF,
                    Domain = "localhost:3000",
                    Path= "/"
                };
                Response.Cookies.Append("accessToken", status_Login.access_Token!, cookieOptions);

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

using Lib_Models.Models_Select.Account;
using Lib_Models.Models_Select.Login;
using Lib_Services.PublicServices.CookieService;
using Lib_Services.V2.Login_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_Api.v1
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin_Service_v2 _login_Service_V2;
        private readonly ICustomCookieService _customCookieService;
        public LoginController(ILogin_Service_v2 login_Service_V2, ICustomCookieService customCookieService)
        {
            _login_Service_V2 = login_Service_V2;
            _customCookieService = customCookieService;
        }

        #region Login Account User Name
        [HttpPost]
        public async Task<IActionResult> Login(Login_Select_v1 request)
        {
            Account_Login_Select_v1 status_Login = await _login_Service_V2.LoginAsync(request);
            if (!status_Login.StatusBool)
            {
                return StatusCode(400, status_Login.StatusType);
            }
            else if (status_Login.StatusBool)
            {
                // Set Cokie
                //_customCookieService.SetCookie(StringUrl.DomainCookieServer, "access_token", status_Login.access_Token!, BaseSettingProject.EXPIRES_ACCESSTOKEN);
                _customCookieService.SetCookie(StringUrl.DomainCookieClient2, BaseSettingProject.ACCESSTOKEN, status_Login.access_Token!, 1); // BaseSettingProject.EXPIRES_ACCESSTOKEN
                _customCookieService.SetCookieAllTime(StringUrl.DomainCookieClient2, BaseSettingProject.KEYSCRFT, status_Login.key_refresh_Token!);

                return StatusCode(200, status_Login.info);
            }
            else
            {
                return StatusCode(500, status_Login.StatusType);
            }
        }
        #endregion
    }
}

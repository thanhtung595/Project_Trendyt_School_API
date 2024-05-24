using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.Account;
using Lib_Models.Models_Select.Login;
using Lib_Models.Status_Model;
using Lib_Services.PublicServices.Auth_Service;
using Lib_Services.PublicServices.CookieService;
using Lib_Services.PublicServices.TokentJwt_Service;
using Lib_Services.V1.Register_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Lib_Settings.RouterName;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_Api.v1
{
    [Route(RouterControllerName.Authentication)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAtuh_Service _authService;
        private readonly ICustomCookieService _customCookieService;
        private readonly ITokentJwt_Service _tokentJwt_Service;
        private readonly IRegister_Service_v1 _register_Service_v1;
        public AuthController(IAtuh_Service authService, ICustomCookieService customCookieService,
            ITokentJwt_Service tokentJwt_Service, IRegister_Service_v1 register_Service_v1)
        {
            _authService = authService;
            _customCookieService = customCookieService;
            _tokentJwt_Service = tokentJwt_Service;
            _register_Service_v1 = register_Service_v1;
        }

        #region Login Account User Name
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login_Select_v1 request)
        {
            Account_Login_Select_v1 status_Login = await _authService.LoginAsync(request);
            if (!status_Login.StatusBool)
            {
                return StatusCode(400, status_Login.StatusType);
            }
            else if (status_Login.StatusBool)
            {
                // Set Cokie
                //_customCookieService.SetCookie(StringUrl.DomainCookieServer, "access_token", status_Login.access_Token!, BaseSettingProject.EXPIRES_ACCESSTOKEN);
                _customCookieService.SetCookie(StringUrl.DomainCookieClient2, BaseSettingProject.ACCESSTOKEN, status_Login.access_Token!, BaseSettingProject.EXPIRES_ACCESSTOKEN);
                _customCookieService.SetCookie(StringUrl.DomainCookieClient2, BaseSettingProject.KEYSCRFT, status_Login.key_refresh_Token!, BaseSettingProject.EXPIRES_REFESHTOKEN);
                return StatusCode(200, status_Login.info);
            }
            else
            {
                return StatusCode(500, status_Login.StatusType);
            }
        }
        #endregion

        #region Register Account User Name
        /*
            - Truy cập access_Token - All Role
            - Gửi {user_Name , fullName , user_Password , email_User}
         */
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserName(Register_Insert_v1 register)
        {
            //Check tài khoản đuôi edu.vn
            string edu = "edu.vn";
            if (register.user_Name!.ToLower().Contains(edu.ToLower()))
            {
                return StatusCode(400, "Không thể đăng ký tài khoản với edu.vn.");
            }
            Status_Application status = await _authService.RegisterUserName(register);
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

        #region Logout Token
        /*
            - Truy cập access_Token - All Role
            - Xóa cặp access_Token và refesh_Token
         */
        [HttpDelete("logout"), Authorize]
        public async Task<IActionResult> LogoutToken()
        {
            await _tokentJwt_Service.LogoutToken();
            _customCookieService.DeleteCokie(BaseSettingProject.ACCESSTOKEN);
            _customCookieService.DeleteCokie(BaseSettingProject.KEYSCRFT);
            return StatusCode(204);
        }
        #endregion
    }
}

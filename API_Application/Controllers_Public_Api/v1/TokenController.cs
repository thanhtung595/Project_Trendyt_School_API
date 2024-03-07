using Lib_Models.Models_Table_Class.Token;
using Lib_Services.Token_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Application.Controllers_Public_Api.v1
{
    [Route("api/v1/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IToken_Service_v1 _tokenService_V1;
        public TokenController(IToken_Service_v1 token_Service_V1)
        {
            _tokenService_V1 = token_Service_V1;
        }

        #region RefeshToken 
        /*
            - Truy cập bằng refesh
            - Gửi access_Token
            - Nhận {access_Token , refesh_Token}
         */
        [HttpPut("refesh-token"),Authorize]
        public async Task<IActionResult> RefshToken()
        {
            Token_Refesh_Model token_Refesh = await _tokenService_V1.RefeshToken();
            if (token_Refesh == null)
            {
                return StatusCode(400, new { error = "access token, refresh token hoặc is active không hợp lệ. Vui lòng đăng nhập lại." });
            }
            return StatusCode(200, token_Refesh);
        }
        #endregion

        #region Logout Token
        /*
            - Truy cập access_Token - All Role
            - Xóa cặp access_Token và refesh_Token
         */
        [HttpPut("logout"), Authorize]
        public async Task<IActionResult> LogoutToken()
        {
            await _tokenService_V1.DeleteToken();
            return StatusCode(204);
        }
        #endregion
    }
}

using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Table_Class.Token;
using Lib_Services.PublicServices.TokentJwt_Service;
using Lib_Services.Token_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using UAParser;

namespace API_Application.Controllers_Public_Api.v1
{
    [Route("api/v1/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IToken_Service_v1 _tokenService_V1;
        private readonly IToken_Service_v2 _tokenService_V2;
        private readonly Trendyt_DbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokentJwt_Service _tokentJwt_Service;
        public TokenController(IToken_Service_v1 token_Service_V1, IToken_Service_v2 token_Service_V2, Trendyt_DbContext db
            , IHttpContextAccessor httpContextAccessor, ITokentJwt_Service tokentJwt_Service)
        {
            _tokenService_V1 = token_Service_V1;
            _tokenService_V2 = token_Service_V2;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _tokentJwt_Service = tokentJwt_Service;
        }

        #region RefeshToken 
        /*
            - Truy cập bằng refesh
            - Nhận {refesh_Token}
         */
        [HttpPut("refesh-token"),Authorize]
        public async Task<IActionResult> RefshToken()
        {
            
            return Ok("Ok");
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
            await _tokenService_V2.LogoutToken();
            return StatusCode(204);
        }
        #endregion

    }
}

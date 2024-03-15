using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Table_Class.Token;
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
        public TokenController(IToken_Service_v1 token_Service_V1, IToken_Service_v2 token_Service_V2, Trendyt_DbContext db
            , IHttpContextAccessor httpContextAccessor)
        {
            _tokenService_V1 = token_Service_V1;
            _tokenService_V2 = token_Service_V2;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        #region RefeshToken 
        /*
            - Truy cập bằng refesh
            - Nhận {refesh_Token}
         */
        [HttpPut("refesh-token"),Authorize]
        public async Task<IActionResult> RefshToken([FromBody] string hostName)
        {
            Token_Refesh_Model token_Refesh = await _tokenService_V2.RefeshToken(hostName);
            if (token_Refesh == null)
            {
                return StatusCode(400, new { error = "Hết phiên làm việc, vui lòng đăng nhập lại" });
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
            await _tokenService_V2.LogoutToken();
            return StatusCode(204);
        }
        #endregion


        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GiaMaToken()
        {
            //// Lấy JWT từ request header
            //var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            //// Giải mã JWT
            //var handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadJwtToken(jwtToken);

            //// Đọc các claims từ JWT
            //string userId = token.Claims.First(claim => claim.Type == "id").Value;

            //Guid idTokenGuidPasre = Guid.Parse(userId);

            //var account = await _db.tbToken.Include(x => x.tbAccount).FirstOrDefaultAsync(x => x.id_Token == idTokenGuidPasre);


            var httpContext = _httpContextAccessor.HttpContext;

            // Lấy địa chỉ IP của người dùng
            var ipAddress = httpContext!.Connection.RemoteIpAddress;

            // Lấy tên máy của người dùng (nếu có)
            var hostName = Dns.GetHostEntry(ipAddress!)?.HostName;

            // Nếu địa chỉ IP là IPv4
            if (ipAddress!.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                // Xử lý IPv4
                var ipv4Address = ipAddress.ToString();
            }
            // Nếu địa chỉ IP là IPv6
            else if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                // Xử lý IPv6
                var ipv6Address = ipAddress.ToString();
            }

            // Lấy tên trình duyệt
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();

            var parser = Parser.GetDefault();
            var clientInfo = parser.Parse(userAgent);

            var browserName = clientInfo.UA.Family; // Tên trình duyệt
            var browserVersion = clientInfo.UA.Major; // Phiên bản trình duyệt

            DateTime now = DateTime.Now;
            DateTime newDateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            return Ok(newDateTime);
        }
    }
}

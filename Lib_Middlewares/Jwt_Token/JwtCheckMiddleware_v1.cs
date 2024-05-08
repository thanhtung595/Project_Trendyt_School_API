using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib_Middlewares.Jwt_Token
{
    public class JwtCheckMiddleware_v1
    {
        //private readonly RequestDelegate _next;
        //private readonly IConfiguration _configuration;
        //private readonly IServiceProvider _serviceProvider;

        //public JwtCheckMiddleware_v1(RequestDelegate next, IConfiguration configuration, IServiceProvider serviceProvider)
        //{
        //    _next = next;
        //    _configuration = configuration;
        //    _serviceProvider = serviceProvider;
        //}

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        // Kiểm tra xem endpoint có yêu cầu xác thực hay không
        //        var endpoint = context.GetEndpoint();
        //        var authorizeAttribute = endpoint?.Metadata.GetMetadata<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>();

        //        if (authorizeAttribute == null)
        //        {
        //            // Nếu không yêu cầu xác thực, bỏ qua và cho request đi qua
        //            await _next(context);
        //            return;
        //        }

        //        // Kiểm tra Headers có chứa Authorization
        //        if (!context.Request.Headers.ContainsKey("Authorization"))
        //        {
        //            // Trả về lỗi 401 (Unauthorized) nếu không có Header Authorization
        //            SetUnauthorizedResponse(context, "Thiếu Header Authorization !");
        //            return;
        //        }

        //        // Lấy giá trị của Header Authorization
        //        var authorizationHeader = context.Request.Headers["Authorization"].ToString();

        //        // Kiểm tra xem Header Authorization có đúng định dạng "Bearer <token>" không
        //        if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        //        {
        //            // Tách lấy token từ Header Authorization
        //            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        //            // Kiểm tra xem token có rỗng hay không
        //            if (string.IsNullOrEmpty(token))
        //            {
        //                SetUnauthorizedResponse(context, "Mã xác thực rỗng !");
        //                return;
        //            }

        //            // Kiểm tra token có trong database
        //            using (var scope = _serviceProvider.CreateScope())
        //            {
        //                var dbContext = scope.ServiceProvider.GetRequiredService<Trendyt_DbContext>();
        //                var jwtToken = await dbContext.tbToken.FirstOrDefaultAsync(t => t.access_Token == token || t.refresh_Token == token && t.is_Active == true);
        //                if (jwtToken == null)
        //                {
        //                    SetUnauthorizedResponse(context, "Token không tồn tại trong server !");
        //                    return;
        //                }
        //            }    

        //            // Thiết lập các tham số kiểm tra hợp lệ của token
        //            var tokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidateAudience = true,
        //                ValidateLifetime = true,
        //                ValidateIssuerSigningKey = true,
        //                ValidIssuer = _configuration["Jwt:Issuer"],
        //                ValidAudience = _configuration["Jwt:Audience"],
        //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!)),
        //            };

        //            var tokenHandler = new JwtSecurityTokenHandler();
        //            SecurityToken securityToken;

        //            // Kiểm tra và lấy thông tin principal từ token
        //            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

        //            // Lấy thời điểm hết hạn của token từ principal
        //            var expirationTime = principal.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

        //            // Kiểm tra thời gian hết hạn của token
        //            if (!string.IsNullOrEmpty(expirationTime) && long.TryParse(expirationTime, out var expirationTimestamp))
        //            {
        //                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationTimestamp).UtcDateTime;

        //                if (DateTime.UtcNow > expirationDateTime)
        //                {
        //                    // Token đã hết hạn
        //                    SetUnauthorizedResponse(context, "Token đã hết hạn !");
        //                    return;
        //                }
        //            }

        //            // Nếu không có lỗi, chuyển tiếp request
        //            await _next(context);
        //        }
        //        else
        //        {
        //            // Trả về lỗi 401 (Unauthorized) nếu Header Authorization không đúng định dạng
        //            SetUnauthorizedResponse(context, "Định dạng Header Authorization không đúng !");
        //        }
        //    }
        //    catch (SecurityTokenExpiredException)
        //    {
        //        // Xử lý lỗi khi token đã hết hạn
        //        SetUnauthorizedResponse(context, "Token đã hết hạn !");
        //    }
        //    catch (Exception)
        //    {
        //        // Xử lý lỗi khi token không hợp lệ (ngoại trừ trường hợp đã xử lý ở trên)
        //        SetUnauthorizedResponse(context, "Lỗi token, vui lòng đăng nhập lại.");
        //    }
        //}

        //// Hàm để thiết lập phản hồi cho trạng thái Unauthorized
        //private void SetUnauthorizedResponse(HttpContext context, string errorMessage)
        //{
        //    context.Response.StatusCode = 401; // Unauthorized

        //    var errorResponse = new { error = errorMessage };
        //    var json = JsonSerializer.Serialize(errorResponse);

        //    context.Response.ContentType = "application/json";
        //    context.Response.WriteAsync(json, Encoding.UTF8);
        //}
    }
}

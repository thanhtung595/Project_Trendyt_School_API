using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib_Middlewares.Jwt_Token
{
    public class JwtCheckMiddleware_v2
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public JwtCheckMiddleware_v2(RequestDelegate next, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _next = next;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Kiểm tra xem endpoint có yêu cầu xác thực hay không
                var endpoint = context.GetEndpoint();
                var authorizeAttribute = endpoint?.Metadata.GetMetadata<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>();

                if (authorizeAttribute == null || context.Request.Path == "/api/v1/token/refesh-token")
                {
                    // Nếu không yêu cầu xác thực, bỏ qua và cho request đi qua
                    await _next(context);
                    return;
                }

                // Kiểm tra Headers có chứa Authorization
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    // Trả về lỗi 401 (Unauthorized) nếu không có Header Authorization
                    SetUnauthorizedResponse(context, "error: Thiếu Header Authorization !");
                    return;
                }

                // Lấy giá trị của Header Authorization
                var authorizationHeader = context.Request.Headers["Authorization"].ToString();

                // Kiểm tra xem Header Authorization có đúng định dạng "Bearer <token>" không
                if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    // Tách lấy token từ Header Authorization
                    var token = authorizationHeader.Substring("Bearer ".Length).Trim();

                    // Kiểm tra xem token có rỗng hay không
                    if (string.IsNullOrEmpty(token))
                    {
                        SetUnauthorizedResponse(context, "error: Mã xác thực rỗng !");
                        return;
                    }

                    // Kiểm tra token có trong database
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        try
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<Trendyt_DbContext>();
                            // Giải mã JWT
                            var handler = new JwtSecurityTokenHandler();
                            var readtoken = handler.ReadJwtToken(token);
                            // Đọc các claims từ JWT
                            string userId = readtoken.Claims.First(claim => claim.Type == "id").Value;
                            Guid idTokenGuidPasre = Guid.Parse(userId);

                            var tokenDb = await dbContext.tbToken.FindAsync(idTokenGuidPasre);
                            // Kiểm tra token có tồn tại
                            if (tokenDb == null)
                            {
                                SetUnauthorizedResponse(context, "error: token không tồn tại");
                                return;
                            }
                            if (tokenDb.is_Active == false)
                            {
                                SetUnauthorizedResponse(context, "Hết phiên làm việc, vui lòng đăng nhập lại");
                                return;
                            }
                            
                        }
                        catch (Exception)
                        {
                            SetUnauthorizedResponse(context, "error: token đã bị sửa đổi bởi phần mềm thứ ba");
                            return;
                        }
                    }
                    // Nếu không có lỗi, chuyển tiếp request
                    await _next(context);
                }
                else
                {
                    // Trả về lỗi 401 (Unauthorized) nếu Header Authorization không đúng định dạng
                    SetUnauthorizedResponse(context, "error: Định dạng Header Authorization không đúng !");
                    return;
                }
            }
            catch (SecurityTokenExpiredException)
            {
                // Xử lý lỗi khi token đã hết hạn
                SetUnauthorizedResponse(context, "error: access_Token đã hết hạn");
                return;
            }
            catch (Exception)
            {
                // Xử lý lỗi khi token không hợp lệ (ngoại trừ trường hợp đã xử lý ở trên)
                SetUnauthorizedResponse(context, "error: lỗi do server vui lòng đang nhập lại");
                return;
            }
        }

        // Hàm để thiết lập phản hồi cho trạng thái Unauthorized
        private void SetUnauthorizedResponse(HttpContext context, string errorMessage)
        {
            context.Response.StatusCode = 401; // Unauthorized

            var errorResponse = new { error = errorMessage };
            var json = JsonSerializer.Serialize(errorResponse);

            context.Response.ContentType = "application/json";
            context.Response.WriteAsync(json, Encoding.UTF8);
        }
    }
}

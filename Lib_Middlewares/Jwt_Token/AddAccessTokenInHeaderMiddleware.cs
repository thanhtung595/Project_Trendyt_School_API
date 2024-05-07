using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Table_Class.Token;
using Lib_Repository.Abstract;
using Lib_Services.PublicServices.CookieService;
using Lib_Services.PublicServices.TokentJwt_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib_Middlewares.Jwt_Token
{
    public class AddAccessTokenInHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AddAccessTokenInHeaderMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor,
                                                 IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var endpoint = context.GetEndpoint();
                if (endpoint?.Metadata.GetMetadata<AuthorizeAttribute>() != null)
                {
                    var accessToken = _httpContextAccessor.HttpContext!.Request.Cookies["access_token"];
                    var KEYSCRFT = _httpContextAccessor.HttpContext!.Request.Cookies["KEYSCRFT"];

                    if (accessToken == null && KEYSCRFT == null)
                    {
                        SetUnauthorizedResponse(context, 401, "Null Access Token");
                        return;
                    }

                    // Refesh Token
                    else if (accessToken == null)
                    {
                        try
                        {
                            using (var scope = _serviceScopeFactory.CreateScope())
                            {
                                var tokenService = scope.ServiceProvider.GetRequiredService<ITokentJwt_Service>();
                                TokenModel tokenModel = await tokenService.RefeshToken();
                                if (tokenModel == null)
                                {
                                    SetUnauthorizedResponse(context, 401, "Null Access Token");
                                    return;
                                }
                                // Thêm AccessToken vào header Authorization
                                var customCookieService = scope.ServiceProvider.GetRequiredService<ICustomCookieService>();
                                customCookieService.SetCookie(StringUrl.DomainCookieClient2, BaseSettingProject.ACCESSTOKEN, tokenModel.access_Token!, BaseSettingProject.EXPIRES_ACCESSTOKEN);
                                customCookieService.SetCookieAllTime(StringUrl.DomainCookieClient2, BaseSettingProject.KEYSCRFT, tokenModel.key_refresh_Token!);
                                context.Request.Headers["Authorization"] = "Bearer " + tokenModel.refresh_Token;
                            }
                        }
                        catch (Exception ex)
                        {
                            await Console.Out.WriteLineAsync(ex.Message);
                            SetUnauthorizedResponse(context, 500, ex.Message);
                            return;
                        }
                    }
                    else
                    {
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var tokenRepository = scope.ServiceProvider.GetRequiredService<IRepository<tbToken>>();
                            var handler = new JwtSecurityTokenHandler();
                            var token = handler.ReadJwtToken(accessToken);
                            var idTokenRefesh = token.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
                            Guid idTokenParse = new Guid();
                            if (!string.IsNullOrEmpty(idTokenRefesh) && Guid.TryParse(idTokenRefesh, out Guid idToken))
                            {
                                idTokenParse = idToken;
                            }
                            var tokenDb = await tokenRepository.GetById(idTokenParse);
                            if (tokenDb == null)
                            {
                                SetUnauthorizedResponse(context, 401, "Null Access Token");
                                return;
                            }
                            // Thêm AccessToken vào header Authorization
                            context.Request.Headers["Authorization"] = "Bearer " + tokenDb.refresh_Token;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                SetUnauthorizedResponse(context, 500, ex.Message);
                return;
            }
            await _next(context);

        }


        // Hàm để thiết lập phản hồi cho trạng thái Unauthorized
        private void SetUnauthorizedResponse(HttpContext context, int statusCode , string errorMessage)
        {
            context.Response.StatusCode = statusCode; // Unauthorized

            var errorResponse = new { error = errorMessage };
            var json = JsonSerializer.Serialize(errorResponse);

            context.Response.ContentType = "application/json";
            context.Response.WriteAsync(json, Encoding.UTF8);
        }
    }
}

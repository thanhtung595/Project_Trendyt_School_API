using Azure;
using Lib_Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Text.Json;

namespace Lib_Services.PublicServices.CookieService
{
    public class CustomCookieService : ICustomCookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomCookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCookie(string domain, string key, object T, int expiresMinutes)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            string json = JsonSerializer.Serialize(T);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false, // chỉ có thể truy cập qua HTTP, không thể truy cập qua JavaScript
                Secure = false, // chỉ gửi cookie qua HTTPS nếu kích hoạt
                SameSite = SameSiteMode.None, // bảo mật ngăn chặn CSRF,
                Domain = domain, // Tên đường dẫn của client
                Path = "/", // Đường dẫn con của domain
                //Expires = DateTime.UtcNow.AddMinutes(expiresMinutes) // Thời gian hết hạn
            };

            httpContext!.Response.Cookies.Append(key, json, cookieOptions);
        }

        public void SetCookie(string domain,string key, string value , int expiresMinutes)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // chỉ có thể truy cập qua HTTP, không thể truy cập qua JavaScript
                Secure = false, // chỉ gửi cookie qua HTTPS nếu kích hoạt
                SameSite = SameSiteMode.Lax, // bảo mật ngăn chặn CSRF,
                Domain = domain, // Tên đường dẫn của client
                Path = "/", // Đường dẫn con của domain
                Expires = DateTime.UtcNow.AddMinutes(expiresMinutes) // Thời gian hết hạn
            };

            httpContext!.Response.Cookies.Append(key, value, cookieOptions);
        }

        public void SetCookieAllTime(string domain, string key, string value)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // chỉ có thể truy cập qua HTTP, không thể truy cập qua JavaScript
                Secure = false, // chỉ gửi cookie qua HTTPS nếu kích hoạt
                SameSite = SameSiteMode.Lax, // bảo mật ngăn chặn CSRF,
                Domain = domain, // Tên đường dẫn của client
                Path = "/", // Đường dẫn con của domain
            };

            httpContext!.Response.Cookies.Append(key, value, cookieOptions);
        }

        public void DeleteCokie(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext!.Response.Cookies.Delete(key);
        }
    }
}

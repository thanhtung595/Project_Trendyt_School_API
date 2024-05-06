using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using AutoMapper;
using Azure.Core;
using Lib_Models.Models_Table_Class.Token;
using Lib_Repository.Abstract;
using Lib_Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UAParser;

namespace Lib_Services.PublicServices.TokentJwt_Service
{
    public class TokentJwt_Service : ITokentJwt_Service
    {
        private readonly IRepository<tbAccount> _accountRepository;
        private readonly IRepository<tbRole> _roleRepository;
        private readonly IRepository<tbMenberSchool> _menberSchooRepository;
        private readonly IRepository<tbToken> _tokenRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int EXPIRES_ACCESSTOKEN = 1;
        //private readonly int EXPIRES_ACCESSTOKEN = BaseSettingProject.EXPIRES_ACCESSTOKEN;
        private readonly int EXPIRES_REFESHTOKEN = BaseSettingProject.EXPIRES_REFESHTOKEN;
        public TokentJwt_Service(IRepository<tbToken> tokenRepository, IConfiguration configuration,
            IMapper mapper, IHttpContextAccessor httpContextAccessor, IRepository<tbAccount> accountRepository,
            IRepository<tbRole> roleRepository, IRepository<tbMenberSchool> menberSchooRepository)
        {
            _tokenRepository = tokenRepository;
            _configuration = configuration;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _menberSchooRepository = menberSchooRepository;
        }

        #region CreateToken
        public async Task<TokenModel> CreateToken(int id_Account, string name_Role)
        {
            /*
             * Kiểm tra nếu token đã tồn tại thì cấp access mới
             * Nếu chưa có thì tạo mới
             */

            try
            {
                // Thời gian login
                DateTime now = DateTime.Now;
                DateTime newDateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                CusstomBrowser browserInfo = GetInfoBrowser();
                //
                // Kiểm tra Cấp 2 với hostName - browserName
                //
                // Kiểm tra token đã tồn tại 
                var tokenAccount = await _tokenRepository.GetAll(t => t.id_Account == id_Account && t.access_Expire_Token < DateTime.UtcNow
                && t.hostName == browserInfo.hostName && t.browserName == browserInfo.browserName);

                if (!tokenAccount.Any())
                {
                    Guid id_Token = Guid.NewGuid();

                    tbToken createToken = new tbToken
                    {
                        id_Token = id_Token,
                        id_Account = id_Account,
                        access_Token = await CreateAcesssTokenString(id_Token, EXPIRES_ACCESSTOKEN), // 5 P
                        refresh_Token = await CreateRefeshTokenString(id_Token, id_Account, name_Role, EXPIRES_REFESHTOKEN), // 7 Day
                        access_Expire_Token = DateTime.UtcNow.AddMinutes(EXPIRES_ACCESSTOKEN),
                        refresh_Expire_Token = DateTime.UtcNow.AddMinutes(EXPIRES_REFESHTOKEN),
                        is_Active = true,
                        ipv4 = browserInfo.ipv4Addres,
                        ipv6 = browserInfo.ipv6Address,
                        hostName = browserInfo.hostName,
                        browserName = browserInfo.browserName,
                        time_login = newDateTime,
                        key_refresh_Token = HashExample.ComputeHash(id_Account + id_Token.ToString())
                    };
                    await _tokenRepository.Insert(createToken);
                    await _tokenRepository.Commit();
                    return _mapper.Map<TokenModel>(createToken);
                }
                else
                {
                    // cấp lại token
                    var tokenRefesh = tokenAccount.First();
                    tokenRefesh.access_Token = await CreateAcesssTokenString(tokenRefesh.id_Token, EXPIRES_ACCESSTOKEN); // 5 P
                    tokenRefesh.refresh_Token = await CreateRefeshTokenString(tokenRefesh.id_Token, id_Account, name_Role, EXPIRES_REFESHTOKEN); // 7 Day
                    tokenRefesh.access_Expire_Token = DateTime.UtcNow.AddMinutes(EXPIRES_ACCESSTOKEN);
                    tokenRefesh.refresh_Expire_Token = DateTime.UtcNow.AddMinutes(EXPIRES_REFESHTOKEN);
                    tokenRefesh.is_Active = true;
                    tokenRefesh.ipv4 = browserInfo.ipv4Addres;
                    tokenRefesh.ipv6 = browserInfo.ipv6Address;
                    tokenRefesh.hostName = browserInfo.hostName;
                    tokenRefesh.browserName = browserInfo.browserName;
                    tokenRefesh.time_login = newDateTime;
                    tokenRefesh.key_refresh_Token = HashExample.ComputeHash(id_Account + tokenRefesh.id_Token.ToString());
                    _tokenRepository.Update(tokenRefesh);
                    await _tokenRepository.Commit();
                    return _mapper.Map<TokenModel>(tokenRefesh);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null!;
            }
        }
        #endregion

        #region RefeshToken
        public async Task<TokenModel> RefeshToken()
        {
            try
            {
                var accessToken = _httpContextAccessor.HttpContext!.Request.Cookies[BaseSettingProject.KEYSCRFT];
                var tokenDb = await _tokenRepository.GetAll(t => t.key_refresh_Token == accessToken);
                if (tokenDb != null)
                {
                    var tokenRefesh = tokenDb.First();
                    string roleSchool = "";
                    string roleJwt = "";

                    var account = await _accountRepository.GetAllIncluding(a => a.id_Account == tokenRefesh.id_Account, r => r.tbRole!);
                    // Lấy role của account với idRole từ account
                    roleJwt = account.First().tbRole!.name_Role!;
                    // Lấy Member và roleSchool của account với idAccount từ account
                    var member = await _menberSchooRepository.GetAllIncluding(m => m.id_Account == account.First().id_Account, roleSchool => roleSchool.tbRoleSchool!);

                    if (member.Any())
                    {
                        var memberOne = member.First();
                        roleSchool = memberOne.tbRoleSchool!.name_Role!;
                        roleJwt = roleSchool;
                    }

                    CusstomBrowser browserInfo = GetInfoBrowser();
                    // cấp lại token
                    tokenRefesh.access_Token = await CreateAcesssTokenString(tokenRefesh.id_Token, EXPIRES_ACCESSTOKEN); // 5 P
                    tokenRefesh.refresh_Token = await CreateRefeshTokenString(tokenRefesh.id_Token, tokenRefesh.id_Account, roleJwt, EXPIRES_REFESHTOKEN); // 7 Day
                    tokenRefesh.access_Expire_Token = DateTime.UtcNow.AddMinutes(EXPIRES_ACCESSTOKEN);
                    tokenRefesh.refresh_Expire_Token = DateTime.UtcNow.AddMinutes(EXPIRES_REFESHTOKEN);
                    tokenRefesh.is_Active = true;
                    tokenRefesh.ipv4 = browserInfo.ipv4Addres;
                    tokenRefesh.ipv6 = browserInfo.ipv6Address;
                    tokenRefesh.hostName = browserInfo.hostName;
                    tokenRefesh.browserName = browserInfo.browserName;
                    tokenRefesh.key_refresh_Token = HashExample.ComputeHash(account.First().id_Account + tokenRefesh.id_Token.ToString());
                    _tokenRepository.Update(tokenRefesh);
                    await _tokenRepository.Commit();
                    return _mapper.Map<TokenModel>(tokenRefesh);
                }
                return null!;

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null!;
            }
        }
        #endregion

        private CusstomBrowser GetInfoBrowser()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            // Lấy địa chỉ IP của người dùng
            var ipAddress = httpContext!.Connection.RemoteIpAddress;
            string ipv4Addres = "";
            string ipv6Address = "";

            // Nếu địa chỉ IP là IPv4
            if (ipAddress!.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                // Xử lý IPv4
                ipv4Addres = ipAddress.ToString();
            }
            // Nếu địa chỉ IP là IPv6
            else if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                // Xử lý IPv6
                ipv6Address = ipAddress.ToString();
            }

            // Lấy tên trình duyệt
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();

            var parser = Parser.GetDefault();
            var clientInfo = parser.Parse(userAgent);

            string browserName = clientInfo.UA.Family; // Tên trình duyệt
            string clientHostName = Dns.GetHostName(); // Tên máy 
            return new CusstomBrowser
            {
                ipv4Addres = ipv4Addres,
                ipv6Address = ipv6Address,
                browserName = browserName,
                hostName = clientHostName
            };
        }

        #region Private CreateAcesssTokenString
        private async Task<string> CreateAcesssTokenString(Guid id_Token, int day_Expires)
        {

            string id_TokenString = id_Token.ToString();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("id", id_TokenString),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(day_Expires), // Thời gian sống của JWT
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return await Task.FromResult(tokenString);
        }
        #endregion

        #region Private CreateRefeshTokenString
        private async Task<string> CreateRefeshTokenString(Guid id_Token, int idAccount, string name_Role, int day_Expires)
        {

            string id_TokenString = id_Token.ToString();
            string id_AccountString = idAccount.ToString();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("idToken", id_TokenString),
                        new Claim("idAccount", id_AccountString),
                        new Claim("typeRole", name_Role!),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(day_Expires), // Thời gian sống của JWT
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return await Task.FromResult(tokenString);
        }
        #endregion

        private class CusstomBrowser
        {
            public string? ipv4Addres { get; set; }
            public string? ipv6Address { get; set; }
            public string? hostName { get; set; }
            public string? browserName { get; set; }
        }

        private static class HashExample
        {
            public static string ComputeHash(string input)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    DateTime time = DateTime.UtcNow;

                    string keyHash = input + time.ToString();

                    byte[] inputBytes = Encoding.UTF8.GetBytes(keyHash);
                    byte[] hashBytes = sha256.ComputeHash(inputBytes);
                    return Convert.ToBase64String(hashBytes);
                }
            }
        }
    } 
}

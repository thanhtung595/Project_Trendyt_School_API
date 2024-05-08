using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Z.EntityFramework.Extensions;
using AutoMapper;
using Azure.Core;
using Lib_Models.Models_Table_Class.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UAParser;

namespace Lib_Services.Token_Service
{
    public class Token_Service_v2 : IToken_Service_v2
    {
        //private readonly Trendyt_DbContext _db;
        //private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //public Token_Service_v2(Trendyt_DbContext db, IConfiguration configuration,
        //    IMapper mapper, IHttpContextAccessor httpContextAccessor)
        //{
        //    _db = db;
        //    _configuration = configuration;
        //    _mapper = mapper;
        //    _httpContextAccessor = httpContextAccessor;
        //}

        //#region CreateToken
        //public async Task<TokenModel> CreateToken(int id_Account, string name_Role, string hostName)
        //{
        //    /*
        //     * Kiểm tra nếu token đã tồn tại thì cấp access mới
        //     * Nếu chưa có thì tạo mới
        //     */

        //    try
        //    {
        //        var httpContext = _httpContextAccessor.HttpContext;

        //        // Lấy địa chỉ IP của người dùng
        //        var ipAddress = httpContext!.Connection.RemoteIpAddress;
        //        string ipv4Addres = "";
        //        string ipv6Address = "";

        //        // Nếu địa chỉ IP là IPv4
        //        if (ipAddress!.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //        {
        //            // Xử lý IPv4
        //            ipv4Addres = ipAddress.ToString();
        //        }
        //        // Nếu địa chỉ IP là IPv6
        //        else if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
        //        {
        //            // Xử lý IPv6
        //            ipv6Address = ipAddress.ToString();
        //        }

        //        // Lấy tên trình duyệt
        //        var userAgent = httpContext.Request.Headers["User-Agent"].ToString();

        //        var parser = Parser.GetDefault();
        //        var clientInfo = parser.Parse(userAgent);

        //        var browserName = clientInfo.UA.Family; // Tên trình duyệt

        //        var tokenCheck = await _db.tbToken.FirstOrDefaultAsync(x => x.id_Account == id_Account
        //                        && x.ipv4 == ipv4Addres && x.ipv6 == ipv6Address 
        //                        && x.browserName == browserName);

        //        if (tokenCheck == null)
        //        {
        //            Guid id_Token = Guid.NewGuid();
        //            DateTime now = DateTime.Now;
        //            DateTime newDateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

        //            tbToken createToken = new tbToken
        //            {
        //                id_Token = id_Token,
        //                id_Account = id_Account,
        //                access_Token = await CreateTokenString(id_Token, id_Account, name_Role, 5), // 5 P
        //                refresh_Token = await CreateTokenString(id_Token, id_Account, name_Role, 10080), // 7 Day
        //                access_Expire_Token = DateTime.UtcNow.AddMinutes(5),
        //                refresh_Expire_Token = DateTime.UtcNow.AddDays(7),
        //                is_Active = true,
        //                ipv4 = ipv4Addres,
        //                ipv6 = ipv6Address,
        //                hostName = hostName,
        //                browserName = browserName,
        //                time_login = newDateTime
        //            };
        //            await _db.tbToken.AddAsync(createToken);
        //            await _db.SaveChangesAsync();
        //            return new TokenModel
        //            {
        //                access_Token = createToken.access_Token,
        //                refresh_Token = createToken.refresh_Token,
        //                access_Expire_Token = createToken.access_Expire_Token,
        //                refresh_Expire_Token = createToken.refresh_Expire_Token
        //            };
        //        }
        //        else
        //        {
        //            tokenCheck.access_Token = await CreateTokenString(tokenCheck.id_Token, id_Account, name_Role, 5); // 5 P
        //            tokenCheck.refresh_Token = await CreateTokenString(tokenCheck.id_Token, id_Account, name_Role, 10080); // 7 Day
        //            tokenCheck.access_Expire_Token = DateTime.UtcNow.AddMinutes(5);
        //            tokenCheck.refresh_Expire_Token = DateTime.UtcNow.AddDays(7);
        //            tokenCheck.is_Active = true;
        //            await _db.SaveChangesAsync();
        //            return new TokenModel
        //            {
        //                access_Token = tokenCheck.access_Token,
        //                refresh_Token = tokenCheck.refresh_Token,
        //                access_Expire_Token = tokenCheck.access_Expire_Token,
        //                refresh_Expire_Token = tokenCheck.refresh_Expire_Token
        //            };
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null!;
        //    }
        //}
        //#endregion

        //#region RefeshToken
        //public async Task<Token_Refesh_Model> RefeshToken()
        //{
        //    try
        //    {
        //        if (!_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        //        {
        //            return null!;
        //        }
        //        var tokenHeaders = authorizationHeader!.ToString().Split(' ').Last();
        //        var handler = new JwtSecurityTokenHandler();
        //        var token = handler.ReadJwtToken(tokenHeaders);
        //        var userIdClaim = token.Claims.FirstOrDefault(claim => claim.Type == "id");
        //        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var idTokenGuidParse))
        //        {
        //            return null!;
        //        }

        //        var account = await _db.tbToken
        //                .Include(x => x.tbAccount)
        //                .FirstOrDefaultAsync(x => x.id_Token == idTokenGuidParse);

        //        if (account == null || account!.is_Active == false)
        //        {
        //            return null!;
        //        }
        //        // Xóa các token hết hạn
        //        var deleteToken = await _db.tbToken.Where(x => x.id_Account == account.id_Account
        //                          && DateTime.UtcNow > x.refresh_Expire_Token).ToListAsync();
        //        if (deleteToken != null || deleteToken!.Count() > 0)
        //        {
        //            _db.tbToken.RemoveRange(deleteToken!);
        //        }

        //        var role = await _db.tbRole.FindAsync(account!.tbAccount!.id_Role);

        //        account!.access_Token = await CreateTokenString(account.id_Token, account.id_Account, role!.name_Role!, 5); // 5 P
        //        account.refresh_Token = await CreateTokenString(account.id_Token, account.id_Account, role.name_Role!, 10080); // 7 Day
        //        account.access_Expire_Token = DateTime.UtcNow.AddMinutes(5);
        //        account.refresh_Expire_Token = DateTime.UtcNow.AddDays(7);
        //        account.is_Active = true;
        //        await _db.SaveChangesAsync();

        //        return new Token_Refesh_Model
        //        {
        //            access_Token = account.access_Token,
        //            refresh_Token = account.refresh_Token,
        //            access_Expire_Token = account.access_Expire_Token,
        //            refresh_Expire_Token = account.refresh_Expire_Token
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return null!;
        //    }
        //}
        //#endregion

        //#region LogoutToken
        //public async Task LogoutToken()
        //{
        //    try
        //    {
        //        if (!_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        //        {
        //            return;
        //        }
        //        var tokenHeaders = authorizationHeader!.ToString().Split(' ').Last();
        //        var handler = new JwtSecurityTokenHandler();
        //        var token = handler.ReadJwtToken(tokenHeaders);
        //        var userIdClaim = token.Claims.FirstOrDefault(claim => claim.Type == "id");
        //        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var idTokenGuidParse))
        //        {
        //            return;
        //        }

        //        var tokenDb = await _db.tbToken.FindAsync(idTokenGuidParse);
        //        tokenDb!.is_Active = false;
        //        await _db.SaveChangesAsync();
        //        return;
        //    }
        //    catch (Exception)
        //    {
        //        return;
        //    }
        //    throw new NotImplementedException();
        //}
        //#endregion

        //#region Private CreateTokenString
        //private async Task<string> CreateTokenString(Guid id_Token, int id_Account, string name_Role, int day_Expires)
        //{
        //    // Kiểm tra tài khoản có nằm trong school để lấy role school
        //    var isMenberSchool = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == id_Account);
        //    if (isMenberSchool != null)
        //    {
        //        var roleSchool = await _db.tbRoleSchool.FindAsync(isMenberSchool.id_RoleSchool);
        //        name_Role = roleSchool!.name_Role!;
        //    }

        //    string id_TokenString = id_Token.ToString();
        //    string id_AccountString = id_Account.ToString();

        //    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        //    var issuer = _configuration["Jwt:Issuer"];
        //    var audience = _configuration["Jwt:Audience"];
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim("id", id_TokenString),
        //                new Claim("typeRole", name_Role!),
        //            }),
        //        Expires = DateTime.UtcNow.AddMinutes(day_Expires), // Thời gian sống của JWT
        //        Issuer = issuer,
        //        Audience = audience,
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(token);
        //    return await Task.FromResult(tokenString);
        //}
        //#endregion

        //#region Get_Id_Account_Token
        //public async Task<int> Get_Id_Account_Token()
        //{
        //    if (!_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        //    {
        //        return 0;
        //    }
        //    var tokenHeaders = authorizationHeader!.ToString().Split(' ').Last();
        //    var handler = new JwtSecurityTokenHandler();
        //    var token = handler.ReadJwtToken(tokenHeaders);
        //    var userIdClaim = token.Claims.FirstOrDefault(claim => claim.Type == "idAccount");
        //    if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var idTokenGuidParse))
        //    {
        //        return 0;
        //    }

        //    return await Task.FromResult(idTokenGuidParse);
        //}
        //#endregion

        //#region Get_Menber_Token
        //public async Task<tbMenberSchool> Get_Menber_Token()
        //{
        //    if (!_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        //    {
        //        return null!; ;
        //    }
        //    var tokenHeaders = authorizationHeader!.ToString().Split(' ').Last();
        //    var handler = new JwtSecurityTokenHandler();
        //    var token = handler.ReadJwtToken(tokenHeaders);
        //    var userIdClaim = token.Claims.FirstOrDefault(claim => claim.Type == "idAccount");
        //    if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var idTokenGuidParse))
        //    {
        //        return null!;
        //    }
        //    var member = await _db.tbMenberSchool.Include(x => x.tbRoleSchool).FirstOrDefaultAsync(x => x.id_Account == idTokenGuidParse);
        //    return member ?? null!;
        //}
        //#endregion
        public Task<TokenModel> CreateToken(int id_Account, string name_Role, string hostName)
        {
            throw new NotImplementedException();
        }

        public Task<int> Get_Id_Account_Token()
        {
            throw new NotImplementedException();
        }

        public Task<tbMenberSchool> Get_Menber_Token()
        {
            throw new NotImplementedException();
        }

        public Task LogoutToken()
        {
            throw new NotImplementedException();
        }

        public Task<Token_Refesh_Model> RefeshToken()
        {
            throw new NotImplementedException();
        }
    }
}

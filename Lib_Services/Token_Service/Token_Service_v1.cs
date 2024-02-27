using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using AutoMapper;
using Lib_Models.Models_Table_Class.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.Token_Service
{
    public class Token_Service_v1 : IToken_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Token_Service_v1(Trendyt_DbContext db, IConfiguration configuration,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _configuration = configuration;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetAccessTokenAccount()
        {
            if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                var tokenHeaders = authorizationHeader.ToString().Split(' ').Last();
                var accessToken = await _db.tbToken.FirstOrDefaultAsync(x => x.refresh_Token == tokenHeaders);

                return await Task.FromResult(accessToken!.access_Token!);
            }
            return null!;
        }

        public async Task<string> GetRefeshTokenAccount()
        {
            if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                var tokenHeaders = authorizationHeader.ToString().Split(' ').Last();
                var accessToken = await _db.tbToken.FirstOrDefaultAsync(x => x.refresh_Token == tokenHeaders);
                if (accessToken == null)
                {
                    return null!;
                }
                return await Task.FromResult(accessToken.refresh_Token!);
            }
            return null!;
        }

        public async Task<int> GetAccessTokenIdAccount()
        {
            if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                var tokenHeaders = authorizationHeader.ToString().Split(' ').Last();
                var accessToken = await _db.tbToken.FirstOrDefaultAsync(x => x.access_Token == tokenHeaders);
                if (accessToken == null)
                {
                    return 0;
                }
                return await Task.FromResult(accessToken.id_Account);
            }
            return 0;
        }
        public async Task<int> GetRefeshTokenIdAccount()
        {
            if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                var tokenHeaders = authorizationHeader.ToString().Split(' ').Last();
                var refresh_Token = await _db.tbToken.FirstOrDefaultAsync(x => x.refresh_Token == tokenHeaders);
                if (refresh_Token == null)
                {
                    return 0;
                }
                return await Task.FromResult(refresh_Token.id_Account);
            }
            return 0;
        }
        
        public async Task CreateToken(int id_Account)
        {
            try
            {
                tbToken add_Token = new tbToken();
                add_Token.id_Token = Guid.NewGuid();
                add_Token.access_Token = "";
                add_Token.refresh_Token = "";
                add_Token.access_Expire_Token = DateTime.UtcNow;
                add_Token.refresh_Expire_Token = DateTime.UtcNow;
                add_Token.id_Account = id_Account;
                add_Token.is_Active = false;
                await _db.tbToken.AddAsync(add_Token);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TokenModel> RefeshToken(int id_Account)
        {
            var account = await _db.tbAccount.FindAsync(id_Account);
            var role = await _db.tbRole.FindAsync(account!.id_Role);
            var tokenDb = await _db.tbToken.FirstOrDefaultAsync(x => x.id_Account == id_Account);
            tokenDb!.access_Token = await CreateTokenString(tokenDb.id_Token, id_Account, role!.name_Role!,7);
            tokenDb.refresh_Token = await CreateTokenString(tokenDb.id_Token, id_Account, role.name_Role!, 30);
            tokenDb.access_Expire_Token = DateTime.UtcNow.AddDays(7);
            tokenDb.refresh_Expire_Token = DateTime.UtcNow.AddDays(30);
            tokenDb.is_Active = true;
            await _db.SaveChangesAsync();

            return _mapper.Map<TokenModel>(tokenDb);
        }
        public async Task DeleteToken()
        {
            int id_AccountToken = await GetAccessTokenIdAccount();
            var token = await _db.tbToken.FirstOrDefaultAsync(x => x.id_Account == id_AccountToken);
            token!.access_Token = "";
            token.refresh_Token = "";
            token.is_Active = false;
            await _db.SaveChangesAsync();
        }
        public async Task<Token_Refesh_Model> RefeshToken(string access_Token)
        {
            int id_AccountToken = await GetRefeshTokenIdAccount();
            string rfToken = await GetRefeshTokenAccount();
            string acToken = access_Token;

            var token = await _db.tbToken.FirstOrDefaultAsync(x =>
                x.access_Token == access_Token && x.refresh_Token == rfToken 
                && x.id_Account == id_AccountToken && x.is_Active == true);

            if (token == null)
            {
                return null!;
            }

            TokenModel tokenModel = await RefeshToken(id_AccountToken);

            return new Token_Refesh_Model
            {
                access_Token = tokenModel.access_Token,
                refresh_Token = tokenModel.refresh_Token,
                access_Expire_Token = tokenModel.access_Expire_Token,
                refresh_Expire_Token = tokenModel.refresh_Expire_Token,
            };
        }
        private async Task<string> CreateTokenString(Guid id_Token, int id_Account, string name_Role,int day_Expires)
        {
            var isMenberSchool = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == id_Account);
            if (isMenberSchool != null)
            {
                var roleSchool = await _db.tbRoleSchool.FindAsync(isMenberSchool.id_RoleSchool);
                name_Role = roleSchool!.name_Role!;
            }
            string id_TokenString = id_Token.ToString();
            string id_AccountString = id_Account.ToString();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("id", id_TokenString),
                        new Claim("typeRole", name_Role!),
                    }),
                Expires = DateTime.UtcNow.AddDays(day_Expires), // Thời gian sống của JWT
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return await Task.FromResult(tokenString);
        }

        public async Task<tbMenberSchool> Get_Menber_Token()
        {
            try
            {
                if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                {
                    var tokenHeaders = authorizationHeader.ToString().Split(' ').Last();
                    var accessToken = await _db.tbToken.FirstOrDefaultAsync(x => x.access_Token == tokenHeaders);
                    if (accessToken == null)
                    {
                        return null!;
                    }
                    var menber = await _db.tbMenberSchool.FirstOrDefaultAsync(x => x.id_Account == accessToken.id_Account);
                    if (menber == null)
                    {
                        return null!;
                    }
                    return await Task.FromResult(menber);
                }
                return null!;
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}

using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyT_Data.Identity;

namespace Lib_Config.Configuration
{
    public static class ConfigurationService
    {
        /// <summary>
        /// RegisterDbContext : kết nối MSSQL
        /// RegisterJwt : Cấu hình Json jwt
        /// RegisterRoleAuthorition : Kiểm tra role khi có request tới
        /// RegisterAddCors : Sét quyền các domain có thể truy cập api
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Trendyt_DbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DataBase_Trendyt_School"),
            options => options.MigrationsAssembly(typeof(Trendyt_DbContext).Assembly.FullName)));
        }

        public static void RegisterJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    LifetimeValidator = (notBefore, expires, token, param) =>
                    {
                        return expires > DateTime.UtcNow; // Kiểm tra thời gian hết hạn
                    },
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public static void RegisterRoleAuthorition(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                #region AdminServer
                options.AddPolicy(IdentityData.AdminServerPolicyName, p =>
                p.RequireClaim(IdentityData.TypeRole, IdentityData.AdminServerClaimName));
                #endregion

                #region AdminSchool
                options.AddPolicy(IdentityData.AdminSchoolPolicyName, p =>
                p.RequireClaim(IdentityData.TypeRole, IdentityData.AdminSchoolClaimName));
                #endregion

                #region QuanLySchoolManager
                options.AddPolicy(IdentityData.QuanLySchoolManager, policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(c =>
                            c.Type == IdentityData.TypeRole &&
                            (c.Value == IdentityData.AdminSchoolClaimName ||
                                c.Value == IdentityData.IndustryClaimName ||
                                c.Value == IdentityData.SecretaryClaimName));
                    });
                });
                #endregion

                #region QuanLyKhoaManager
                options.AddPolicy(IdentityData.QuanLyKhoaManager, policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(c =>
                            c.Type == IdentityData.TypeRole &&
                            (c.Value == IdentityData.IndustryClaimName ||
                             c.Value == IdentityData.SecretaryClaimName));
                    });
                });
                #endregion

                #region Industry
                options.AddPolicy(IdentityData.IndustryPolicyName, p =>
                p.RequireClaim(IdentityData.TypeRole, IdentityData.IndustryClaimName));
                #endregion

                #region TeacherAndStudent
                options.AddPolicy(IdentityData.TeacherAndStudent, policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(c =>
                            c.Type == IdentityData.TypeRole &&
                            (c.Value == IdentityData.TeacherClaimName ||
                             c.Value == IdentityData.StudentClaimName));
                    });
                });
                #endregion

                #region Teacher
                options.AddPolicy(IdentityData.TeacherPolicyName, p =>
                p.RequireClaim(IdentityData.TypeRole, IdentityData.TeacherClaimName));
                #endregion

                #region Student
                options.AddPolicy(IdentityData.StudentPolicyName, p =>
                p.RequireClaim(IdentityData.TypeRole, IdentityData.StudentClaimName));
                #endregion
            });
        }

        public static void RegisterAddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.WithOrigins(
                            "http://localhost:3000", 
                            "https://trendyt.netlify.app")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .WithExposedHeaders("*");
                    });
            });
        }
    }
}

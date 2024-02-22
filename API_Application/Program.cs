using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Lib_Helpers.Scoped;
using System.Text;
using Lib_Helpers.AutoMapper;
using Lib_Middlewares.Jwt_Token;
using TrendyT_Data.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//*******************Start User Config Builder*********************//

//Connect database sqlsever
builder.Services.AddDbContext<Trendyt_DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase_Trendyt_School"), sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure();
    });
});

// Cấu hình JWT
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
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
//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .WithExposedHeaders("*");
        });
});

//Add scoped middlewares
builder.Services.MyServiceScopedV1();
builder.Services.MyRepositoryScopedV1();

//Add AutoMapper middlewares
builder.Services.AddAutoMapper(typeof(AutoMapperV1));

// AddHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Check role Authorition
builder.Services.AddAuthorization(options =>
{
    #region AdminServer
        options.AddPolicy(IdentityData.AdminServerPolicyName, p =>
        p.RequireClaim(IdentityData.TypeRole, IdentityData.AdminServerClaimName));
    #endregion

    #region AdminSchool
        options.AddPolicy(IdentityData.AdminSchoolPolicyName , p =>
        p.RequireClaim(IdentityData.TypeRole, IdentityData.AdminSchoolClaimName));
    #endregion

    #region Industry
    options.AddPolicy(IdentityData.IndustryPolicyName, p =>
    p.RequireClaim(IdentityData.TypeRole, IdentityData.IndustryClaimName));
    #endregion

    #region Teacher
    options.AddPolicy(IdentityData.TeacherPolicyName, p =>
    p.RequireClaim(IdentityData.TypeRole, IdentityData.TeacherClaimName));
    #endregion

    #region Student
    options.AddPolicy(IdentityData.StudentPolicyName, p =>
    p.RequireClaim(IdentityData.TypeRole, IdentityData.StudentClaimName));
    #endregion 
    //// Manager School
    //options.AddPolicy(IdentityData.ManagerSchoolPolicy, policy =>
    //{
    //    policy.RequireAssertion(context =>
    //    {
    //        return context.User.HasClaim(c =>
    //            c.Type == IdentityData.TypeRole &&
    //            (c.Value == IdentityData.AdminSchoolUserClaimName ||
    //             c.Value == IdentityData.IndustrySchoolUserClaimName));
    //    });
    //});
});
//*******************End User Config*********************//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//*******************Start User Config Use*********************//

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<JwtCheckMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

//*******************End User Config Use*********************//
app.MapControllers();

app.Run();

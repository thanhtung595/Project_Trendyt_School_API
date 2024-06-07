using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Lib_Helpers.Scoped;
using System.Text;
using Lib_Helpers.AutoMapper;
using Lib_Middlewares.Jwt_Token;
using TrendyT_Data.Identity;
using Lib_Config.Configuration;
using Lib_Middlewares;
using Xceed.Document.NET;
using Lib_Services.PublicServices.NotificationService;
using Lib_Services.PublicServices.SignalRService;
using Microsoft.AspNetCore.SignalR;
using Lib_Services.PublicServices.SignalRService.NotificationHub;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//*******************Start User Config Builder*********************//

//Connect database sqlsever
builder.Services.RegisterDbContext(builder.Configuration);

// Cấu hình JWT
builder.Services.RegisterJwt(builder.Configuration);
builder.Services.AddSignalR()
    .AddNewtonsoftJsonProtocol(options =>
    {
        options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
//Add Cors
builder.Services.RegisterAddCors();

// Check role Authorition
builder.Services.RegisterRoleAuthorition();

//Add scoped middlewares
builder.Services.RegisterRepositoryScoped();
builder.Services.RegisterServiceScoped();
builder.Services.MyServiceScopedV1();
builder.Services.MyRepositoryScopedV1();
builder.Services.MyStoredProceduresScoped();

//Add AutoMapper middlewares
builder.Services.AddAutoMapper(typeof(AutoMapperV1));

// Add AddHostedService NotificationBackgroundService
builder.Services.AddHostedService<NotificationBackgroundService>();

//
builder.Services.AddSignalR();

// AddHttpContextAccessor
builder.Services.AddHttpContextAccessor();

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
app.UseMiddleware<AddAccessTokenInHeaderMiddleware>();
//app.UseMiddleware<UseFileScurity>();
app.UseStaticFiles();
//app.UseMiddleware<JwtCheckMiddleware_v2>();
app.UseAuthentication();
app.UseAuthorization();

// SignalR
app.MapHub<NotificationHub>("/notificationHub-monhoc");

//*******************End User Config Use*********************//
app.MapControllers();

app.Run();

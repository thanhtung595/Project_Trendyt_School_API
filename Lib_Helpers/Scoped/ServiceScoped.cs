using Lib_Services.Token_Service;
using Lib_Services.V1.Account_Service;
using Lib_Services.V1.Class_Member_Service;
using Lib_Services.V1.Class_Service;
using Lib_Services.V1.KhoaSchool_Service;
using Lib_Services.V1.Login_Service;
using Lib_Services.V1.Menber_Service;
using Lib_Services.V1.MonHoc;
using Lib_Services.V1.MonHoc_Student_Service;
using Lib_Services.V1.Register_Service;
using Lib_Services.V1.Role_Service;
using Lib_Services.V1.RoleSchool_Service;
using Lib_Services.V1.School_Service;
using Lib_Services.V1.Student_Service;
using Lib_Services.V1.Teacher_Service;
using Lib_Services.V1.TypeAccount_Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Helpers.Scoped
{
    public static class ServiceScoped
    {
        public static void MyServiceScopedV1(this IServiceCollection services)
        {
            services.AddScoped<IRole_Service_v1, Role_Service_v1>();
            services.AddScoped<ITypeAccount_Service_v1, TypeAccount_Service_v1>();
            services.AddScoped<IRegister_Service_v1, Register_Service_v1>();
            services.AddScoped<IAccount_Service_v1, Account_Service_v1>();
            services.AddScoped<IToken_Service_v1, Token_Service_v1>();
            services.AddScoped<ILogin_Service_v1, Login_Service_v1>();
            services.AddScoped<IRoleSchool_Service_v1, RoleSchool_Service_v1>();
            services.AddScoped<ISchool_Service_v1, School_Service_v1>();
            services.AddScoped<IMenber_Service_v1, Menber_Service_v1>();
            services.AddScoped<IKhoaSchool_Service_v1, KhoaSchool_Service_v1>();
            services.AddScoped<ITeacher_Service_v1, Teacher_Service_v1>();
            services.AddScoped<IClass_Service_v1, Class_Service_v1>();
            services.AddScoped<IStudent_Service_v1, Student_Service_v1>();
            services.AddScoped<IClass_Member_Service_v1, Class_Member_Service_v1>();
            services.AddScoped<IMonHoc_Service_v1, MonHoc_Service_v1>();
            services.AddScoped<IMonHoc_Student_Service_v1, MonHoc_Student_Service_v1>();
        }
    }
}

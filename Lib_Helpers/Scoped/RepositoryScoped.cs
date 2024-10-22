﻿using Lib_Repository.V1.Account_Repository;
using Lib_Repository.V1.Class_Member_Repository;
using Lib_Repository.V1.Class_Repository;
using Lib_Repository.V1.Khoa_Repository;
using Lib_Repository.V1.LichHoc_Repository;
using Lib_Repository.V1.Menber_Repository;
using Lib_Repository.V1.MonHoc;
using Lib_Repository.V1.MonHoc_Repository;
using Lib_Repository.V1.MonHoc_Student_Repository;
using Lib_Repository.V1.Role_Repository;
using Lib_Repository.V1.RoleSchool_Repository;
using Lib_Repository.V1.School_Repository;
using Lib_Repository.V1.Student_Repository;
using Lib_Repository.V1.Teacher_Repository;
using Lib_Repository.V1.TypeAccount_Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Helpers.Scoped
{
    public static class RepositoryScoped
    {
        public static void MyRepositoryScopedV1(this IServiceCollection services)
        {
            services.AddScoped<IRole_Repository_v1, Role_Repository_v1>();
            services.AddScoped<ITypeAccount_Repository_v1, TypeAccount_Repository_v1>();
            services.AddScoped<IAccount_Repository_v1, Account_Repository_v1>();
            services.AddScoped<IRoleSchool_Repository_v1, RoleSchool_Repository_v1>();
            services.AddScoped<ISchool_Repository_v1, School_Repository_v1>();
            services.AddScoped<IMenber_Repository_v1, Menber_Repository_v1>();
            services.AddScoped<IKhoa_Repository_v1, Khoa_Repository_v1>();
            services.AddScoped<ITeacher_Repository_v1, Teacher_Repository_v1>();
            services.AddScoped<IClass_Repository_v1, Class_Repository_v1>();
            services.AddScoped<IStudent_Repository_v1, Student_Repository_v1>();
            services.AddScoped<IClass_Member_Repository_v1, Class_Member_Repository_v1>();
            services.AddScoped<IMonHoc_Repository_v1, MonHoc_Repository_v1>();
            services.AddScoped<IMonHoc_Student_Repository_v1, MonHoc_Student_Repository_v1>();
            services.AddScoped<ILichHoc_Repository_v1, LichHoc_Repository_v1>();

        }
    }
}

using Lib_Repository.V1.Role_Repository;
using Microsoft.Extensions.DependencyInjection;
using Stored_Procedures.PROC.MonHoc;
using Stored_Procedures.PROC.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Helpers.Scoped
{
    public static class StoredProceduresScoped
    {
        public static void MyStoredProceduresScoped(this IServiceCollection services)
        {
            services.AddScoped<IPROC_tbRole, PROC_tbRole>();
            services.AddScoped<IPROC_MonHoc, PROC_MonHoc>();
        }
    }
}

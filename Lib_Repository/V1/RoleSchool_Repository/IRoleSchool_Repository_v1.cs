using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.RoleSchool;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.RoleSchool_Repository
{
    public interface IRoleSchool_Repository_v1
    {
        Task<List<tbRoleSchool>> SelectAllAsync();
        Task<Status_Application> InsertAsync(tbRoleSchool role);

        Task<Status_Application> Update_Role_Menber(UpdateRoleSchool request);
    }
}

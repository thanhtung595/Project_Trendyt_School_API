using Lib_Models.Model_Insert.v1;
using Lib_Models.Model_Update.Role;
using Lib_Models.Models_Select.Role;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Role_Service
{
    public interface IRole_Service_v1
    {
        Task<List<Role_Select_v1>> SelectAllAsync();
        Task<Status_Application> InsertAsync(Role_Insert_v1 request);
        Task<Status_Application> Update_Role_Account(UpdateRoleAccount request);
    }
}

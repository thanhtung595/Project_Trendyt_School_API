using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Insert.v1;
using Lib_Models.Models_Select.Role;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Role_Repository
{
    public interface IRole_Repository_v1
    {
        Task<List<tbRole>> SelectAllAsync();
        Task<Status_Application> InsertAsync(tbRole role);
    }
}

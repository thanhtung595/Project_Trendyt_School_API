using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.TypeAccount;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.TypeAccount_Repository
{
    public interface ITypeAccount_Repository_v1
    {
        Task<List<tbTypeAccount>> SelectAll();
        Task<Status_Application> InsertAsync(tbTypeAccount typeAccount);
    }
}

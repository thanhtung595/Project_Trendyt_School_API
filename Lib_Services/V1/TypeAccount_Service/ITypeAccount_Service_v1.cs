using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.TypeAccount;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.TypeAccount_Service
{
    public interface ITypeAccount_Service_v1
    {
        Task<List<TypeAccount_Select_v1>> SelectAll();
        Task<Status_Application> InsertAsync(TypeAccount_Insert_v1 request);
    }
}

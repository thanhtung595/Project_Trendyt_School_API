using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Register_Service
{
    public interface IRegister_Service_v1
    {
        Task<Status_Application> RegisterUserName(Register_Insert_v1 register);
    }
}

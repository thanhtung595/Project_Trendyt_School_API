using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.Account;
using Lib_Models.Models_Select.Login;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.PublicServices.Auth_Service
{
    public interface IAtuh_Service
    {
        Task<Account_Login_Select_v1> LoginAsync(Login_Select_v1 login);
        Task<Status_Application> RegisterUserName(Register_Insert_v1 register);
    }
}

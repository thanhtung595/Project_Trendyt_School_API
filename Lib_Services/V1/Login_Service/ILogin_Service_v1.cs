using Lib_Models.Models_Select.Account;
using Lib_Models.Models_Select.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Login_Service
{
    public interface ILogin_Service_v1
    {
        Task<Account_Login_Select_v1> LoginAsync(Login_Select_v1 login);
    }
}

using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Account;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Account_Repository
{
    public interface IAccount_Repository_v1
    {
        Task<Account_Login_Select_v1> SelectByAccount(int id_Account);
        Task<Status_Application> InsertAccount(tbAccount account);
    }
}

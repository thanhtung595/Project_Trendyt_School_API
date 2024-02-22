using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Account;
using Lib_Models.Status_Model;
using Lib_Repository.V1.Account_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Account_Service
{
    public class Account_Service_v1 : IAccount_Service_v1
    {
        private readonly IAccount_Repository_v1 _account_Repository_V1;
        public Account_Service_v1(IAccount_Repository_v1 account_Repository_V1)
        {
            _account_Repository_V1 = account_Repository_V1;
        }

        #region InsertAccount
        public async Task<Status_Application> InsertAccount(tbAccount account)
        {
            return await _account_Repository_V1.InsertAccount(account);
        }
        #endregion

        #region SelectByAccount
        public async Task<Account_Login_Select_v1> SelectByAccount(int id_Account)
        {
            return await _account_Repository_V1.SelectByAccount(id_Account);
        }
        #endregion
    }
}

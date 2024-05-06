using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Account;
using Lib_Models.Models_Select.Login;
using Lib_Repository.V1.Account_Repository;
using Lib_Services.Token_Service;
using Lib_Services.V1.Account_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Login_Service
{
    public class Login_Service_v1 : ILogin_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly IToken_Service_v1 _token_Service_V1;
        private readonly IToken_Service_v2 _token_Service_V2;
        private readonly IAccount_Service_v1 _account_Service_V1;
        public Login_Service_v1(Trendyt_DbContext db, IToken_Service_v1 token_Service_V1,
            IToken_Service_v2 token_Service_V2, IAccount_Service_v1 account_Service_V1)
        {
            _db = db;
            _token_Service_V1 = token_Service_V1;
            _token_Service_V2 = token_Service_V2;
            _account_Service_V1 = account_Service_V1;
        }

        #region LoginAsync
        public async Task<Account_Login_Select_v1> LoginAsync(Login_Select_v1 login)
        {
            login.user_Name = login.user_Name!.Trim();
            login.user_Password = login.user_Password!.Trim();

            // Kiểm tra null value
            if (string.IsNullOrEmpty(login.user_Name))
            {
                return new Account_Login_Select_v1
                {
                    StatusBool = false,
                    StatusType = "Chưa nhập user name"
                };
            }
            if (string.IsNullOrEmpty(login.user_Password))
            {
                return new Account_Login_Select_v1
                {
                    StatusBool = false,
                    StatusType = "Chưa nhập user password"
                };
            }

            // Kiểm tra user_Name có tồn tại
            var account = await _db.tbAccount.Include(x => x.tbRole).FirstOrDefaultAsync(x => x.user_Name == login.user_Name);
            if (account == null)
            {
                return new Account_Login_Select_v1
                {
                    StatusBool = false,
                    StatusType = "Sai tài khoản."
                };
            }

            // Kiểm tra user_Password mã hóa BCrypt có tồn tại
            bool veryHassPass = BCrypt.Net.BCrypt.Verify(login.user_Password, account.user_Password);
            if (!veryHassPass)
            {
                return new Account_Login_Select_v1
                {
                    StatusBool = false,
                    StatusType = "Sai mật khẩu."
                };
            }
            // Cấp bộ Token mới
            var rfToken_v2 = await _token_Service_V2.CreateToken(account.id_Account, account.tbRole!.name_Role!,"");

            // Trả lại dữ liệu cho Controller
            Account_Login_Select_v1 loginRpo = await _account_Service_V1.SelectByAccount(account.id_Account);
            loginRpo.access_Token = rfToken_v2.access_Token;
            loginRpo.refesh_Token = rfToken_v2.refresh_Token;
            loginRpo.access_Expire_Token = rfToken_v2.access_Expire_Token;
            loginRpo.refresh_Expire_Token = rfToken_v2.refresh_Expire_Token;

            return loginRpo;
        }
        #endregion
    }
}

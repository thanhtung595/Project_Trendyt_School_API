using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using Lib_Services.Token_Service;
using Lib_Services.V1.Account_Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Register_Service
{
    public class Register_Service_v1 : IRegister_Service_v1
    {
        private readonly Trendyt_DbContext _db;
        private readonly IAccount_Service_v1 _account_Service_V1;
        private readonly IToken_Service_v1 _token_Service_V1;
        public Register_Service_v1(Trendyt_DbContext db, IAccount_Service_v1 account_Service_V1,
            IToken_Service_v1 token_Service_V1)
        {
            _db = db;
            _account_Service_V1 = account_Service_V1;
            _token_Service_V1 = token_Service_V1;
        }

        #region RegisterUserName
        public async Task<Status_Application> RegisterUserName(Register_Insert_v1 register)
        {
            register.user_Name = register.user_Name!.Trim();
            register.user_Password = register.user_Password!.Trim();
            register.email_User = register.email_User!.Trim();
            register.fullName = register.fullName!.Trim();

            // Kiểm tra is null value
            if (string.IsNullOrEmpty(register.user_Name) || string.IsNullOrWhiteSpace(register.user_Name))
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = "Chưa nhập user name"
                };
            }

            // Kiểm tra user_Name có chứa ký tự đặc biệt
            string non_UserName = non_userName(register.user_Name);
            if (non_UserName != null)
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = non_UserName
                };
            }
            if (string.IsNullOrEmpty(register.user_Password) || string.IsNullOrWhiteSpace(register.user_Password))
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = "Chưa nhập user password"
                };
            }
            if (string.IsNullOrEmpty(register.fullName) || string.IsNullOrWhiteSpace(register.fullName))
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = "Chưa nhập full name"
                };
            }
            if (string.IsNullOrEmpty(register.email_User) || string.IsNullOrWhiteSpace(register.email_User))
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = "Chưa nhập value email"
                };
            }

            // Kiểm tra user_Name có tồn tại trong data
            var checkNull_userName = await _db.tbAccount
                .FirstOrDefaultAsync(x => x.user_Name!.ToLower() == register.user_Name.ToLower());

            if (checkNull_userName != null)
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = $"{register.user_Name} đã tồn tại."
                };
            }
            // Kiểm tra email có tồn tại trong data
            var checkNull_Email = await _db.tbAccount
                .FirstOrDefaultAsync(x => x.email_User!.ToLower() == register.email_User.ToLower());

            if (checkNull_Email != null)
            {
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = $"{register.email_User} đã tồn tại."
                };
            }

            //Add Account
            tbAccount account = new tbAccount
            {
                user_Name = register.user_Name,
                user_Password = HasPassword_BCrypt(register.user_Password),
                id_Role = await _db.tbRole.Where(x => x.name_Role == "guest").Select(x => x.id_Role).FirstOrDefaultAsync(),
                id_TypeAccount = await _db.tbTypeAccount.Where(x => x.name_TypeAccount == "account name").Select(x => x.id_TypeAccount).FirstOrDefaultAsync(),
                is_Delete = false,
                is_Ban = false,
                Time_Create = DateTime.Now,
                OTP = "",
                fullName = register.fullName,
                birthday_User = DateTime.Now,
                sex_User = "Khác",
                email_User = register.email_User,
                phone_User = "",
                image_User = "img/avatar/image_default.png",
            };

            // Trả data về controller
            Status_Application statusInserAccount = await _account_Service_V1.InsertAccount(account);
            if (!statusInserAccount.StatusBool)
            {
                return statusInserAccount;
            }
            await _token_Service_V1.CreateToken(statusInserAccount.Id_Int);
            return statusInserAccount;
        }
        #endregion

        private string HasPassword_BCrypt(string password)
        {
            string hasPass = BCrypt.Net.BCrypt.HashPassword(password);
            return hasPass;
        }

        private string non_userName(string user_Name)
        {
            user_Name = user_Name.ToLower().Trim();
            string admin = "admin";
            string nonAdmin = user_Name.Substring(0, 5);
            if (admin == nonAdmin)
            {
                return "Tài khoản không thể bắt đầu bằng admin";
            }
            return null!;
        }
    }
}

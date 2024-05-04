using App_Models.Models_Table_CSDL;
using Dapper;
using Lib_Models.Models_Select.Account;
using Lib_Models.Models_Select.Login;
using Lib_Repository.Abstract;
using Lib_Repository.Abstract_DapperHelper;
using Lib_Repository.Repository_Class;
using Lib_Services.Token_Service;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.Login_Service
{
    public class Login_Service_v2 : ILogin_Service_v2
    {
        private readonly IRepository<tbAccount> _accountRepository;
        private readonly IRepository<tbRole> _roleRepository;
        private readonly IRepository<tbMenberSchool> _menberSchooRepository;
        private readonly IToken_Service_v2 _token_Service_V2;
        public Login_Service_v2(IRepository<tbAccount> accountRepository, IRepository<tbRole> roleRepository,
            IRepository<tbMenberSchool> menberSchooRepository, IToken_Service_v2 token_Service_V2)
        {
            _accountRepository = accountRepository;
            _menberSchooRepository = menberSchooRepository;
            _roleRepository = roleRepository;
            _token_Service_V2 = token_Service_V2;
        }
        
        public async Task<Account_Login_Select_v1> LoginAsync(Login_Select_v1 login)
        {
            string userName = login.user_Name!;
            string userPass = login.user_Password!;
            if (userName.IsNullOrEmpty() || userPass.IsNullOrEmpty())
            {
                return new Account_Login_Select_v1
                {
                    StatusBool = false,
                    StatusType = "Chưa nhập tài khoản hoặc mật khẩu"
                };
            }
            // Lấy account có user_Name
            var accountByUserName = await _accountRepository.GetAll(a => a.user_Name == userName);
            if (accountByUserName.Any())
            {
                var account = accountByUserName.First();

                // Check pass bằng BCrypt.Net
                Account_Login_Select_v1 checkPass = CheckPasswordAccount(userPass, account.user_Password!);
                if (checkPass != null)
                {
                    return checkPass;
                }

                return await SendReponse(account);
            }
            else
            {
                return new Account_Login_Select_v1
                {
                    StatusBool = false,
                    StatusType = "Tài khoản không chính xác"
                };
            }
        }

        private Account_Login_Select_v1 CheckPasswordAccount(string userPass, string userPassAccount)
        {
            bool veryHassPass = BCrypt.Net.BCrypt.Verify(userPass, userPassAccount);

            if (!veryHassPass)
            {
                return new Account_Login_Select_v1
                {
                    StatusBool = false,
                    StatusType = "Mật khẩu không chính xác"
                };
            }
            return null!;
        }

        private async Task<Account_Login_Select_v1> SendReponse(tbAccount account)
        {
            string roleSchool = "";
            // Lấy role của account với idRole từ account
            var role = await _roleRepository.GetById(account.id_Role);

            // Lấy Member và roleSchool của account với idAccount từ account
            var member = await _menberSchooRepository.GetAllIncluding(m => m.id_Account == account.id_Account, roleSchool => roleSchool.tbRoleSchool!);

            if (member.Any())
            {
                var memberOne = member.First();
                roleSchool = memberOne.tbRoleSchool!.name_Role!;
            }

            // Cấp bộ Token mới
            var rfToken_v2 = await _token_Service_V2.CreateToken(account.id_Account, account.tbRole!.name_Role!, "");

            return new Account_Login_Select_v1
            {
                StatusBool = true,
                StatusType = "Success",
                access_Token = rfToken_v2.access_Token,
                refesh_Token = rfToken_v2.refresh_Token,
                access_Expire_Token = rfToken_v2.access_Expire_Token,
                refresh_Expire_Token = rfToken_v2.refresh_Expire_Token,
                info = new Account_Info_Select_v1
                {
                    user_Name = account.user_Name,
                    fullName = account.fullName,
                    birthday_User = account.birthday_User,
                    sex_User = account.sex_User,
                    email_User = account.email_User,
                    phone_User = account.phone_User,
                    image_User = account.image_User,
                    time_Create = account.Time_Create,
                    role = role.name_Role,
                    role_School = roleSchool,
                }
            };
        }
    }
}

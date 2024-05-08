using App_Models.Models_Table_CSDL;
using Dapper;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.Account;
using Lib_Models.Models_Select.Login;
using Lib_Models.Status_Model;
using Lib_Repository.Abstract;
using Lib_Repository.Abstract_DapperHelper;
using Lib_Repository.Repository_Class;
using Lib_Services.PublicServices.TokentJwt_Service;
using Lib_Services.Token_Service;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyT_Data.Identity;

namespace Lib_Services.PublicServices.Auth_Service
{
    public class Atuh_Service : IAtuh_Service
    {
        private readonly string _TypeAcountName = "account name";
        private readonly IRepository<tbAccount> _accountRepository;
        private readonly IRepository<tbRole> _roleRepository;
        private readonly IRepository<tbTypeAccount> _typeAccountRepository;
        private readonly IRepository<tbMenberSchool> _menberSchooRepository;
        private readonly ITokentJwt_Service _tokentJwt_Service;
        public Atuh_Service(IRepository<tbAccount> accountRepository, IRepository<tbRole> roleRepository,
            IRepository<tbMenberSchool> menberSchooRepository, ITokentJwt_Service tokentJwt_Service,
            IRepository<tbTypeAccount> typeAccountRepository)
        {
            _accountRepository = accountRepository;
            _menberSchooRepository = menberSchooRepository;
            _roleRepository = roleRepository;
            _tokentJwt_Service = tokentJwt_Service;
            _typeAccountRepository = typeAccountRepository;
        }

        #region Login UserName
        public async Task<Account_Login_Select_v1> LoginAsync(Login_Select_v1 login)
        {
            try
            {
                string userName = login.user_Name!;
                string userPass = login.user_Password!;
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPass))
                {
                    return new Account_Login_Select_v1
                    {
                        StatusBool = false,
                        StatusType = "Chưa nhập tài khoản hoặc mật khẩu"
                    };
                }
                // Lấy account có user_Name
                var accountByUserName = await _accountRepository!.GetAll(a => a.user_Name == userName);
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
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null!;
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
            string roleJwt = "";
            // Lấy role của account với idRole từ account
            var role = await _roleRepository!.GetById(account.id_Role);
            roleJwt = role.name_Role!;
            // Lấy Member và roleSchool của account với idAccount từ account
            var member = await _menberSchooRepository!.GetAllIncluding(m => m.id_Account == account.id_Account, roleSchool => roleSchool.tbRoleSchool!);

            if (member.Any())
            {
                var memberOne = member.First();
                roleSchool = memberOne.tbRoleSchool!.name_Role!;
                roleJwt = roleSchool;
            }


            // Cấp bộ Token mới
            var capTokenMoi = await _tokentJwt_Service!.CreateToken(account.id_Account, roleJwt);

            return new Account_Login_Select_v1
            {
                StatusBool = true,
                StatusType = "Success",
                access_Token = capTokenMoi.access_Token,
                refesh_Token = capTokenMoi.refresh_Token,
                access_Expire_Token = capTokenMoi.access_Expire_Token,
                refresh_Expire_Token = capTokenMoi.refresh_Expire_Token,
                key_refresh_Token = capTokenMoi.key_refresh_Token,
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
        #endregion

        #region Register UserName
        public async Task<Status_Application> RegisterUserName(Register_Insert_v1 register)
        {
            try
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

                // Kiểm tra user_Name hoặc email có tồn tại trong data
                var checkUserName_Email = await _accountRepository.GetAll(a => a.user_Name == register.user_Name
                                                                            || a.email_User == register.email_User);

                if (checkUserName_Email.Any())
                {
                    var check_Already_Exist = checkUserName_Email.First();
                    if (check_Already_Exist.user_Name == register.user_Name)
                    {
                        return new Status_Application
                        {
                            StatusBool = false,
                            StatusType = $"{register.user_Name} đã tồn tại."
                        };
                    }
                    if (check_Already_Exist.email_User == register.email_User)
                    {
                        return new Status_Application
                        {
                            StatusBool = false,
                            StatusType = $"{register.email_User} đã tồn tại."
                        };
                    }
                }

                // Lấy Id Role
                var roleDb = await _roleRepository.GetAll(r => r.name_Role == IdentityData.GuestClientClaimName);
                Guid idRole = roleDb.First().id_Role;
                // Lấy Id TypeAccount
                var typeAccountDb = await _typeAccountRepository.GetAll(tp => tp.name_TypeAccount == _TypeAcountName);
                Guid idTypeAccount = typeAccountDb.First().id_TypeAccount;
                // Insert account

                DateTime date = DateTime.Now;

                tbAccount accountInsert = new tbAccount
                {
                    user_Name = register.user_Name,
                    user_Password = HasPassword_BCrypt(register.user_Password),
                    id_Role = idRole,
                    id_TypeAccount = idTypeAccount,
                    is_Delete = false,
                    is_Ban = false,
                    Time_Create = date,
                    OTP = "",
                    fullName = register.fullName,
                    birthday_User = date,
                    sex_User = "Khác",
                    email_User = register.email_User,
                    phone_User = "",
                    image_User = "img/avatar/image_default.png",
                };
                await _accountRepository.Insert(accountInsert);
                await _accountRepository.Commit();
                return new Status_Application
                {
                    StatusBool = true,
                    StatusType = "success"
                };
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new Status_Application
                {
                    StatusBool = false,
                    StatusType = ex.Message
                };
            }
        }
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
        #endregion
    }
}

using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Account;
using Lib_Models.Status_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Account_Repository
{
    public class Account_Repository_v1 : IAccount_Repository_v1
    {
        private readonly Trendyt_DbContext _db;
        public Account_Repository_v1(Trendyt_DbContext db)
        {
            _db = db;
        }

        #region InsertAccount
        public async Task<Status_Application> InsertAccount(tbAccount account)
        {
            try
            {
                await _db.tbAccount.AddAsync(account);
                await _db.SaveChangesAsync();
                return new Status_Application
                {
                    StatusBool = true,
                    StatusType = "success",
                    Id_Int = account.id_Account
                };

            }
            catch (Exception ex)
            {
                return new Status_Application { StatusBool = false, StatusType = "error: " + ex.Message };
            }
        }
        #endregion

        #region SelectByAccount
        public async Task<Account_Login_Select_v1> SelectByAccount(int id_Account)
        {
            try
            {
                var account = await (from ac in _db.tbAccount
                                     where ac.id_Account == id_Account
                                     join role in _db.tbRole
                                     on ac.id_Role equals role.id_Role
                                     select new Account_Login_Select_v1
                                     {
                                         StatusBool = true,
                                         StatusType = "success",
                                         info = new Account_Info_Select_v1
                                         {
                                             user_Name = ac.user_Name,
                                             role = role.name_Role,
                                             time_Create = ac.Time_Create,
                                             fullName = ac.fullName,
                                             birthday_User = ac.birthday_User,
                                             sex_User = ac.sex_User,
                                             email_User = ac.email_User,
                                             phone_User = ac.phone_User,
                                             image_User = ac.image_User,
                                         }
                                     }).FirstOrDefaultAsync();

                if (account!.info != null)
                {
                    var menber = await _db.tbMenberSchool.Where(x => x.id_Account == id_Account)
                    .Join(_db.tbRoleSchool, ac => ac.id_RoleSchool, role => role.id_RoleSchool,
                    (ac, role) => new { Account = ac, Role = role }).FirstOrDefaultAsync();

                    if (menber != null)
                    {
                        account!.info!.role_School = menber.Role.name_Role;
                    }
                }

                return account;

            }
            catch (Exception ex)
            {
                return new Account_Login_Select_v1
                {
                    StatusBool = false,
                    StatusType = "error: " + ex.Message
                };
            }
        }
        #endregion
    }
}

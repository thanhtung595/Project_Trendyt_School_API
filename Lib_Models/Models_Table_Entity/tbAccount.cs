using Lib_Models.Models_Select.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbAccount
    {
        [Key]
        public int id_Account { get; set; }
        public string? user_Name { get; set; }
        public string? user_Password { get; set; }
        public Guid id_Role { get; set; }
        public Guid id_TypeAccount { get; set; }
        public bool is_Delete { get; set; }
        public bool is_Ban { get; set; }
        public DateTime Time_Create { get; set; }
        public string? OTP { get; set; }
        public string? fullName { get; set; }
        public DateTime birthday_User { get; set; }
        public string? sex_User { get; set; }
        public string? email_User { get; set; }
        public string? phone_User { get; set; }
        public string? image_User { get; set; }

        [ForeignKey("id_Role")]
        public virtual tbRole? tbRole { get; set; }

        [ForeignKey("id_TypeAccount")]
        public virtual tbTypeAccount? tbTypeAccount { get; set; }

        public IEnumerable<Account_Login_Select_v1> Select(Func<tbAccount, Account_Login_Select_v1> mapToAccountLogin)
        {
            throw new NotImplementedException();
        }
    }
}

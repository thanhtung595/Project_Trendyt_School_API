using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Insert.v1
{
    public class MenberSchool_Insert_v1
    {
        // Account
        public int id_Account { get; set; }

        // Tài khoản mới
        public string? user_Name { get; set; } // required
        public string? fullName { get; set; } // required
        public string? user_Password { get; set; } // required
        public string? email_User { get; set; } // required

        // School
        public int id_School { get; set; } // required
        public int id_KhoaSchool { get; set; }

        // Role - required
        public string? name_Role { get; set; }
    }
}

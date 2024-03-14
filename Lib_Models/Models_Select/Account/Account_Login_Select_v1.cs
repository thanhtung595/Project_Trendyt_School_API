using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Select.Account
{
    public class Account_Login_Select_v1
    {
        public string? access_Token { get; set; }
        public string? refesh_Token { get; set; }
        public DateTime access_Expire_Token { get; set; }
        public DateTime refresh_Expire_Token { get; set; }
        public bool StatusBool { get; set; }
        public string? StatusType { get; set; }

        public Account_Info_Select_v1? info { get; set; }
    }
}

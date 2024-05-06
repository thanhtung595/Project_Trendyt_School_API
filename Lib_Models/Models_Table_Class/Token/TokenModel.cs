using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Class.Token
{
    public class TokenModel
    {
        public Guid id_Token { get; set; }
        public string? access_Token { get; set; }
        public string? refresh_Token { get; set; }
        public DateTime access_Expire_Token { get; set; }
        public DateTime refresh_Expire_Token { get; set; }
        public int id_Account { get; set; }
        public string? key_refresh_Token { get; set; }
    }
}

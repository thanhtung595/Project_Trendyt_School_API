using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbToken
    {
        [Key]
        public Guid id_Token { get; set; }
        public string? access_Token { get; set; }
        public string? refresh_Token { get; set; }
        public string? key_refresh_Token { get; set; }
        public DateTime access_Expire_Token { get; set; }
        public DateTime refresh_Expire_Token { get; set; }
        public int id_Account { get; set; }
        public bool is_Active { get; set; }
        public string? ipv4 { get; set; }
        public string? ipv6 { get; set; }
        public string? hostName { get; set; }
        public string? browserName { get; set; }
        public DateTime? time_login { get; set; }

        [ForeignKey("id_Account")]
        public virtual tbAccount? tbAccount { get; set; }
    }
}

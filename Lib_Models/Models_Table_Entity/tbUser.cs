using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbUser
    {
        [Key]
        public int id_User { get; set; }
        public string? fullName { get; set; }
        public DateTime birthday_User { get; set; }
        public string? sex_User { get; set; }
        public string? email_User { get; set; }
        public string? phone_User { get; set; }
        public string? image_User { get; set; }
    }
}

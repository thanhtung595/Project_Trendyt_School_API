using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Select.Menber
{
    public class Menber_SclectAll_v1
    {
        public int id_MenberSchool { get; set; }
        public string? user_Name { get; set; }
        public string? fullName { get; set; }
        public DateTime birthday_User { get; set; }
        public string? sex_User { get; set; }
        public string? email_User { get; set; }
        public string? phone_User { get; set; }
        public string? name_KhoaSchool { get; set; }
        public string? name_RoleSchool { get; set; }
        public float danhGiaTb { get; set; }
        public string? tags { get; set; }
    }
}

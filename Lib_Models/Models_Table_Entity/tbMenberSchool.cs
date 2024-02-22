using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbMenberSchool
    {
        [Key]
        public int id_MenberSchool { get; set; }
        public int id_Account { get; set; }
        public int id_School { get; set; }
        public int id_KhoaSchool { get; set; }
        public Guid id_RoleSchool { get; set; }
        public float danhGiaTb { get; set; }
        public string? tags { get; set; }

        [ForeignKey("id_Account")]
        public virtual tbAccount? tbAccount { get; set; }

        [ForeignKey("id_School")]
        public virtual tbSchool? tbSchool { get; set; }

        [ForeignKey("id_RoleSchool")]
        public virtual tbRoleSchool? tbRoleSchool { get; set; }
    }
}

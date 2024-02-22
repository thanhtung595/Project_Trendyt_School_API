using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbClassSchool_Menber
    {
        [Key]
        public int id_MonHocClass_Student { get; set; }
        public int id_MenberSchool { get; set; }
        public int id_ClassSchool { get; set; }

        [ForeignKey("id_MenberSchool")]
        public virtual tbMenberSchool? tbMenberSchool { get; set; }

        [ForeignKey("id_ClassSchool")]
        public virtual tbClassSchool? tbClassSchool { get; set; }
    }
}

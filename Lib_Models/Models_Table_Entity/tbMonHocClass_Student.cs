using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbMonHocClass_Student
    {
        [Key]
        public int id_MonHocClass_Student { get; set; }
        public int id_MenberSchool { get; set; }
        public int id_ClassSchool_MonHoc { get; set; }

        [ForeignKey("id_MenberSchool")]
        public virtual tbMenberSchool? tbMenberSchool { get; set; }

        [ForeignKey("id_ClassSchool_MonHoc")]
        public virtual tbClassSchool_MonHoc? tbClassSchool_MonHoc { get; set; }
    }
}

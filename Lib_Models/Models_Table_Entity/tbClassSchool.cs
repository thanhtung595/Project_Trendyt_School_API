using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbClassSchool
    {
        [Key]
        public int id_ClassSchool { get; set; }
        public string? name_ClassSchool { get; set; }
        public int id_KhoaSchool { get; set; }
        public string? tags { get; set; }

        [ForeignKey("id_KhoaSchool")]
        public virtual tbKhoaSchool? tbKhoaSchool { get; set;}
    }
}

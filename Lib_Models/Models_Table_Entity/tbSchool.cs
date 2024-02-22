using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbSchool
    {
        [Key]
        public int id_School { get; set; }
        public int id_Account { get; set; }
        public string? name_School { get; set; }
        public string? description_School { get; set; }
        public string? adderss_School { get; set; }
        public float evaluate_School { get; set; }

        [ForeignKey("id_Account")]
        public virtual tbAccount? tbAccount { get; set; }
    }
}

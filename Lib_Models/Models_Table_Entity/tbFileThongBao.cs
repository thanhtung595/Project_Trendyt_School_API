using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbFileThongBao
    {
        [Key]
        public int idFileThongBao { get; set; }
        public string? file { get; set; }
        public int idThongBao { get; set; }

        [ForeignKey("idThongBao")]
        public virtual tbThongBao? tbThongBao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbFileBaiTap
    {
        [Key]
        public int idFileBaiTap { get; set; }
        public string? file { get; set; }
        public int idBaiTap { get; set; }

        [ForeignKey("idBaiTap")]
        public virtual tbBaiTap? tbBaiTap { get; set; }
    }
}

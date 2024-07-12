using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbFileNopBaiTap
    {
        [Key]
        public int idFileNopBaiTap { get; set; }
        public string? file { get; set; }
        public int idNopBaiTap { get; set; }

        [ForeignKey("idNopBaiTap")]
        public virtual tbNopBaiTap? tbNopBaiTap { get; set; }
    }
}

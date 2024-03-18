using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbLichHoc
    {
        [Key] 
        public int id_LichHoc { get; set; }
        public int id_MonHoc { get; set; }
        public DateTime thoiGianBatDau { get; set; }
        public DateTime thoiGianKetThuc { get; set; }

        [ForeignKey("id_MonHoc")]
        public virtual tbMonHoc? tbMonHoc { get; set; }
    }
}

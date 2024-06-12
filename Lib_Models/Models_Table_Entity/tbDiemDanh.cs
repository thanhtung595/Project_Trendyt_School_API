using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbDiemDanh
    {
        [Key]
        public int id_DiemDanh { get; set; }
        public int id_LichHoc { get; set; }
        public int id_MonHocClass_Student { get; set; }
        public bool _DauGio { get; set; }
        public bool _CuoiGio { get; set; }
        public bool _DiMuon { get; set; }
        public DateTime editLastTime { get; set; }

        [ForeignKey("id_LichHoc")]
        public virtual tbLichHoc? tbLichHoc {  set; get; }

        [ForeignKey("id_MonHocClass_Student")]
        public virtual tbMonHocClass_Student? tbMonHocClass_Student {  set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbNopBaiTap
    {
        [Key]
        public int idNopBaiTap { get; set; }
        public int idBaiTap { get; set; }
        public int id_MonHocClass_Student { get; set; }
        public float diem { get; set; }
        public string? danhGia { get; set; }
        public DateTime createTime { get; set; }

        [ForeignKey("idBaiTap")]
        public virtual tbBaiTap? tbBaiTap { get; set; }

        [ForeignKey("id_MonHocClass_Student")]
        public virtual tbMonHocClass_Student? tbMonHocClass_Student { get; set; }
    }
}

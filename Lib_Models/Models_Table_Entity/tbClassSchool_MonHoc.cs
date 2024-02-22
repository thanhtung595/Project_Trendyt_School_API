using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbClassSchool_MonHoc
    {
        [Key]
        public int id_ClassSchool_MonHoc { get; set; }
        public int id_ClassSchool { get; set; }
        public int _SoBuoiNghi { get; set; }
        public int id_MonHoc { get; set; }
        public float _danhGiaTrungBinh { get; set; }
        public string? tags { get; set; }

        [ForeignKey("id_MonHoc")]
        public virtual tbMonHoc? tbMonHoc { get; set; }

        [ForeignKey("id_ClassSchool")]
        public virtual tbClassSchool? tbClassSchool { get; set; }
    }
}

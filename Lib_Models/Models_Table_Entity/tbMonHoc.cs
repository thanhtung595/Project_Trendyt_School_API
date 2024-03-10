using App_Models.Models_Table_CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbMonHoc
    {
        [Key]
        public int id_MonHoc { get; set; }
        public int id_School { get; set; }
        public string? name_MonHoc { get; set; }
        public float _danhGiaTrungBinh { get; set; }
        public string? tags { get; set; }
        public DateTime ngayKetThuc { get; set; }
        public DateTime ngayBatDau { get; set; }
        public int _SoBuoiNghi { get; set; }

        [ForeignKey("id_School")]
        public virtual tbSchool? tbSchool { get; set; }
    }
}

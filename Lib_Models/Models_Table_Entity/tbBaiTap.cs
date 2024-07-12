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
    public class tbBaiTap
    {
        [Key]
        public int idBaiTap { get; set; }
        public string? nameBaiTap { get; set; }
        public string? moTa { get; set; }
        public DateTime hanNopBai { get; set; }
        public DateTime createTime { get; set; }
        public int id_MonHoc { get; set; }
        public int id_MenberSchool { get; set; }

        [ForeignKey("id_MonHoc")]
        public virtual tbMonHoc? tbMonHoc { get; set; }

        [ForeignKey("id_MenberSchool")]
        public virtual tbMenberSchool? tbMenberSchool { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbBuoiHoc_MonHoc
    {
        [Key]
        public int id_BuoiHoc_MonHoc { get; set; }
        public int id_ClassSchool_MonHoc { get; set; }
        public int id_BuoiHoc { get; set; }

        [ForeignKey("id_ClassSchool_MonHoc")]
        public virtual tbClassSchool_MonHoc? tbClassSchool_MonHoc { get; set; }

        [ForeignKey("id_BuoiHoc")]
        public virtual tbBuoiHoc? tbBuoiHoc { get; set; }
    }
}

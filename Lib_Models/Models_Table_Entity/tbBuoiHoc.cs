using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbBuoiHoc
    {
        [Key]
        public int id_BuoiHoc { get; set; }
        public string? name_BuoiHoc { get; set; }
        public DateTime create_Time { get; set; }
    }
}

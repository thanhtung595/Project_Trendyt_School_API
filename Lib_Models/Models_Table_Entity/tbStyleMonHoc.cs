using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbStyleBuoiHoc
    {
        [Key] 
        public int id_StyleBuoiHoc { get; set; }
        public string? name { get; set; }
    }
}

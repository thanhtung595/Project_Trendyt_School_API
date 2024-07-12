using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbTypeThongBao
    {
        [Key]
        public int idTypeThongBao { get; set; }
        public string? type { get; set; }
    }
}

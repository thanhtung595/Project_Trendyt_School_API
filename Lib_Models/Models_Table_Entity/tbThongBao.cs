using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbThongBao
    {
        [Key] 
        public int idThongBao { get; set; }
        public string? title { get; set; }
        public string? body { get; set; }
        public DateTime timeThongBao { get; set; }
        public int idTypeThongBao { get; set; }

        [ForeignKey("idTypeThongBao")]
        public virtual tbTypeThongBao? tbTypeThongBao { get; set; }
    }
}

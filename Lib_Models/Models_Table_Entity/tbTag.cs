using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Entity
{
    public class tbTag
    {
        [Key] 
        public int id_Tag { get; set; }
        public string? name { get; set; }
    }
}

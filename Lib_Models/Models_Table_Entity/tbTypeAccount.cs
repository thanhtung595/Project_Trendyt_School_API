using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbTypeAccount
    {
        [Key]
        public Guid id_TypeAccount { get; set; }
        public string? name_TypeAccount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbRole
    {
        [Key]
        public Guid id_Role { get; set; }
        public string? name_Role {  get; set; }
    }
}

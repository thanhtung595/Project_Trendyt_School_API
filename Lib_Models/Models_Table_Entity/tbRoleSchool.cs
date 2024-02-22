using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbRoleSchool
    {
        [Key]
        public Guid id_RoleSchool { get; set; }
        public string? name_Role { get; set; }
    }
}

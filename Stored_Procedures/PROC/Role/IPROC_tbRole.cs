using Lib_Models.Models_Select.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stored_Procedures.PROC.Role
{
    public interface IPROC_tbRole
    {
        public bool CreateProc_GetRoles();
        public List<Role_Select_v1> Proc_GetAllRole();
    }
}

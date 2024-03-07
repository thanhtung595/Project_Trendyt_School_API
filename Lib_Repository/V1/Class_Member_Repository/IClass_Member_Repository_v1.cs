using App_Models.Models_Table_CSDL;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Class_Member_Repository
{
    public interface IClass_Member_Repository_v1
    {
        Task<Status_Application> Insert(tbClassSchool_Menber classMember);
        Task<Status_Application> Delete(tbClassSchool_Menber classMember);
    }
}

using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Class;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Class_Repository
{
    public interface IClass_Repository_v1
    {
        Task<List<Class_Select_v1>> SelectAll(tbMenberSchool menberSchoolManager);
        Task<Class_Select_v1> SelectById();
        Task<Status_Application> InsertAsync(tbClassSchool classSchool);
        Task<Status_Application> UpdateAsync(tbClassSchool classSchool);
    }
}

using App_Models.Models_Table_CSDL;
using Lib_Models.Model_Update.School;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.School;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.School_Repository
{
    public interface ISchool_Repository_v1
    {
        Task<List<School_SelectAll_v1>> SelectAllAsync();
        Task<Status_Application> InsertAsync(tbSchool school);
        Task<Status_Application> UpdateAsync(School_Update_v1 request);
    }
}

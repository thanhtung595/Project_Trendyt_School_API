using Lib_Models.Model_Update.School;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.School;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.School_Service
{
    public interface ISchool_Service_v1
    {
        Task<List<School_SelectAll_v1>> SelectAllAsync();
        Task<Status_Application> InsertAsync(School_Insert_v1 request);
        Task<Status_Application> UpdateAsync(School_Update_v1 request);
    }
}

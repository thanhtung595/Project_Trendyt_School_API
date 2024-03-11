using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.MonHoc
{
    public interface IMonHoc_Service_v1
    {
        Task<List<MonHoc_SelectAll_v1>> GetAll();
        Task<Status_Application> Insert(tbMonHoc monHoc);
    }
}

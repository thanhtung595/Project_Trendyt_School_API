using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Models_Select.LichHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.LichHoc_Service
{
    public interface ILichHoc_Service_v1
    {
        Task<Status_Application> Insert(LichHoc_Insert_v1 lichHoc);
        Task<List<LichHoc_Select_All_v1>> SelectAll();
    }
}

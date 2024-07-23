using Lib_Models.Models_Insert.v2.MonHoc;
using Lib_Models.Models_Select.MonHoc;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.MonHoc_Service
{
    public interface IMonHoc_Service_v2
    {
        Task<Status_Application> InsertAsync(MonHoc_Insert_Request_v2 request);
        Task<MonHoc_Member_Select> GetMember(int idMonHoc);
    }
}

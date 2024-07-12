using Lib_Models.Model_Update.BaiTap;
using Lib_Models.Models_Insert.v2.BaiTap;
using Lib_Models.Models_Select.BaiTap;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.NopBaiTap_Service
{
    public interface INopBaiTap_Service_v2
    {
        Task<Status_Application> Add(NopBaiTap_Insert_v2 baiTap);
        Task ChamDiem(ChamDiemModel chamDiem);
        Task<List<BaiTapNopModelSelect>> GetAll(int idBaiTap);
    }
}

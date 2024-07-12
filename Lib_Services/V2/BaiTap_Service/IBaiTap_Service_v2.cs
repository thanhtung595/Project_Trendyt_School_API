using Lib_Models.Models_Insert.v2.BaiTap;
using Lib_Models.Models_Select.BaiTap;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.BaiTap_Service
{
    public interface IBaiTap_Service_v2
    {
        Task<Status_Application> Add(BaiTap_Insert_v2 baiTap);
        Task<List<BaiTapModelSelecAll>> GetAll();
    }
}

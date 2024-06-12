using Lib_Models.Models_Insert.v2.MonHoc;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.LịchHoc
{
    public interface ILichHoc_Service_v2
    {
        Task<Status_Application> InsertAsync(List<LichHoc_MonHoc_Insert_v2>? lichHocs, int soBuoiHoc, int idMonHoc);
    }
}

using Lib_Models.Models_Table_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.StyleBuoiHoc
{
    public interface IStyleBuoiHoc_Service_v2
    {
        Task<List<tbStyleBuoiHoc>> GetAllAsync();
    }
}

using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.Menber;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Menber_Service
{
    public interface IMenber_Service_v1
    {
        Task<List<Menber_SclectAll_v1>> SelectAllAsync();
        Task<Status_Application> InsertAsync(MenberSchool_Insert_v1 request);
    }
}

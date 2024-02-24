using Lib_Models.Model_Update.Khoa;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Models_Select.Khoa;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.KhoaSchool_Service
{
    public interface IKhoaSchool_Service_v1
    {
        Task<List<KhoaSchool_Select_v1>> SelectAll();
        Task<Status_Application> InsertAysnc(KhoaSchool_Insert_v1 reques);
        Task<Status_Application> UpdateAysnc(KhoaSchool_Update_v1 reques);
    }
}

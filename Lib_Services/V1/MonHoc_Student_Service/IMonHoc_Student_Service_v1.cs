using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.MonHoc_Student_Service
{
    public interface IMonHoc_Student_Service_v1
    {
        Task<Status_Application> Insert(MonHocClass_Student_Insert_v1 student);
        Task<Status_Application> Delete(MonHocClass_Student_Insert_v1 student);
    }
}

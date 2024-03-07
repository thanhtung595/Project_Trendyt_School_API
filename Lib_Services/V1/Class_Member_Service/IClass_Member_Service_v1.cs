using Lib_Models.Models_Insert.v1.Class_School;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Class_Member_Service
{
    public interface IClass_Member_Service_v1
    {
        Task<Status_Application> Insert(Class_Member_Insert_v1 request);
        Task<Status_Application> Delete(int id_ClassSchool,int id_Student);
    }
}

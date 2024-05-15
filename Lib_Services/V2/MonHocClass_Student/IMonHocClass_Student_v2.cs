using Lib_Models.Models_Insert.v2.MonHoc;
using Lib_Models.Status_Model;
using Lib_Repository.Repository_Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.MonHocClass_Student
{
    public interface IMonHocClass_Student_v2
    {
        Task<Status_Application> InsertAsync(int idMonHoc, List<ListStudent_MonHoc_Insert_v2> member);
        Task<Status_Application> InsertAsync(int idMonHoc, List<ListStudent_MonHoc_Insert_v2> member, int idTeacher);
    }
}

using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.MonHoc_Student_Repository
{
    public interface IMonHoc_Student_Repository_v1
    {
        Task<Status_Application> Insert(tbMonHocClass_Student student);
    }
}

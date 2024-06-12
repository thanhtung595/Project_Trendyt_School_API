using Lib_Models.Models_Select.Student;
using Lib_Models.Models_Select.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V2.Student_Service
{
    public interface IStudent_Service_v2
    {
        Task<List<Student_Select_v1>> SelectAllNotJoinClassAsync();
    }
}

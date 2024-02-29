using Lib_Models.Models_Select.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Student_Service
{
    public interface IStudent_Service_v1
    {
        Task<List<Student_Select_v1>> SelectAllAsync();
    }
}

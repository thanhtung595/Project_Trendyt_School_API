using Lib_Models.Models_Select.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.V1.Teacher_Service
{
    public interface ITeacher_Service_v1
    {
        Task<List<Select_All_Teacher_v1>> Select_All_Teacher();
    }
}

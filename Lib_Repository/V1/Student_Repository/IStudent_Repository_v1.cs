using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Select.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Repository.V1.Student_Repository
{
    public interface IStudent_Repository_v1
    {
        Task<List<Student_Select_v1>> SelectAllAsync(tbMenberSchool menberKhoaManager);
    }
}

using Lib_Models.Models_Select.Student;
using Lib_Models.Models_Select.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Select.MonHoc
{
    public class MonHoc_Member_Select
    {
        public Select_One_Teacher_v1? giangvien { get; set; }
        public List<Student_Select_v1>? student { get; set; }
    }
}

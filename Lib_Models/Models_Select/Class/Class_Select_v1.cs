using Lib_Models.Models_Select.Khoa;
using Lib_Models.Models_Select.Student;
using Lib_Models.Models_Select.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Select.Class
{
    public class Class_Select_v1
    {
        public int id_ClassSchool { get; set; }
        public string? name_ClassSchool { get; set; }
        public KhoaSchool_Select_v1? khoa { get; set; }
        public string? tags { get; set; }
        public Select_All_Teacher_v1? chu_nhiem { get; set; }
        public int count_student { get; set; }
        public List<Student_Select_v1>? student { get; set; }
    }
}

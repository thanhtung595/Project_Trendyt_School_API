using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Insert.v1.Class_School
{
    public class ClassSchool_Insert_v1
    {
        public string? name_ClassSchool { get; set; }
        public int id_Teacher { get; set; }
        public List<Class_Member_Insert_v1>? student { get; set; }
    }
}

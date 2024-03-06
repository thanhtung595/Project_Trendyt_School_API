using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Model_Update.School
{
    public class School_Menber_Update_v1
    {
        public int id_MenberSchool { get; set; }
        public string? fullName { get; set; }
        public int id_KhoaSchool { get; set; }
        public string? chuc_vu { get; set; }
        public string? tags { get; set; }
    }
}

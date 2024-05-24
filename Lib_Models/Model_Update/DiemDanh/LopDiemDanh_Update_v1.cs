using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Model_Update.DiemDanh
{
    public class LopDiemDanh_Update_v1
    {
        public int id_MonHoc { get; set; }
        public List<StudentDiemDanh_Update_v1>? students {  get; set; }
    }
}

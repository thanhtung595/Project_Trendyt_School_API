using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Insert.v2.MonHoc
{
    public class MonHoc_Insert_Request_v2
    {
        public MonHoc_Insert_v2? monHoc { get; set; }
        public List<ListStudent_MonHoc_Insert_v2>? students { get; set; }
        public List<LichHoc_MonHoc_Insert_v2>? lichHocs { get; set; }
    }
}

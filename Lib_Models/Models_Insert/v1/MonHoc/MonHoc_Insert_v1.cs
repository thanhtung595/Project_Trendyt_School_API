using Lib_Models.Models_Insert.v1.Class_School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Insert.v1.MonHoc
{
    public class MonHoc_Insert_v1
    {
        public string? name_MonHoc { get; set; }
        public DateTime ngayKetThuc { get; set; }
        public DateTime ngayBatDau { get; set; }
        public int _SoBuoiNghi { get; set; }
        public int _SoBuoiHoc { get; set; }
        public int id_Teacher { get; set; }

        public List<Class_Member_Insert_v1>? student { get; set; }
    }
}

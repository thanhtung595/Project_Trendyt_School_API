using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Model_Update.MonHoc
{
    public class MonHoc_Update_v1
    {
        public int id_MonHoc { get; set; }
        public string? name_MonHoc { get; set; }
        public string? tag { get; set; }
        public int soBuoiNghi { get; set; }
        public DateTime ngayBatDau { get; set; }
        public DateTime ngayKetThuc { get; set; }
        public int id_Teacher { get; set; }
    }
}

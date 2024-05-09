using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Insert.v1.MonHoc
{
    public class LichHoc_Insert_v1
    {
        public int id_MonHoc { get; set; }
        public DateTime thoiGianBatDau { get; set; }
        public DateTime thoiGianKetThuc { get; set; }
        public string? phonghoc { get; set; }
        public string? phuongPhapHoc { get; set; }
        public string? tinhTrangBuoiHoc { get; set; }
    }
}

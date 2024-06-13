using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Select.DiemDanh
{
    public class DiemDanh_Select_v1
    {
        public int id_DiemDanh { get; set; }
        public int id_LichHoc { get; set; }
        public int msv { get; set; }
        public string? fullName { get; set; }
        public bool _DauGio { get; set; }
        public bool _CuoiGio { get; set; }
        public bool _DiMuon { get; set; }
    }
}

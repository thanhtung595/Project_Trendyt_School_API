using Lib_Models.Models_Table_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Select.BaiTap
{
    public class BaiTapModelSelecAll
    {
        public int idBaiTap { get; set; }
        public string? nameBaiTap { get; set; }
        public string? moTa { get; set; }
        public DateTime hanNopBai { get; set; }
        public DateTime createTime { get; set; }
        public int id_MonHoc { get; set; }
        public string? giaoVienGiao { get; set; }
        public bool isDaNopBai { get; set; }
        public List<tbFileBaiTap>? file { get; set; }
    }
}

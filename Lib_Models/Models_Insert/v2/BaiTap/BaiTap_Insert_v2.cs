using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Insert.v2.BaiTap
{
    public class BaiTap_Insert_v2
    {
        public int id_MonHoc { get; set; }
        public string? nameBaiTap { get; set; }
        public string? moTa { get; set; }
        public DateTime? hanNopBai { get; set; } = null;
        public List<IFormFile>? files { get; set; } = new List<IFormFile> { };
    }
}

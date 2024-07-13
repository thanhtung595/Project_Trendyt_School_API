using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Insert.v2.BaiTap
{
    public class NopBaiTap_Insert_v2
    {
        public int idBaiTap { get; set; }
        public List<IFormFile>? files { get; set; } = new List<IFormFile> { };
    }
}

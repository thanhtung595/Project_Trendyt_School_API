using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Select.DiemDanh
{
    public class DiemDanh_Select_v1
    {
        public DateTime ThoiGianBatDau { get; set; }
        public List<DiemDanhStudent_Select>? Students { get; set; }
       
    }
}

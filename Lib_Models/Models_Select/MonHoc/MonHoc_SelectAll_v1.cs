using Lib_Models.Models_Select.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Select.MonHoc
{
    public class MonHoc_SelectAll_v1
    {
        public int id_MonHoc { get; set; }
        public string? name_MonHoc { get; set; }
        public float danhGiaTrungBinh { get; set; }
        public string? tag { get; set; }
        public int soBuoiNghi { get; set; }
        public DateTime ngayBatDau { get; set; }
        public DateTime ngayKetThuc { get; set; }
        public Select_One_Teacher_v1? giangvien { get; set; }
        public int coutnStudent { get; set; }
    }
}

﻿using Lib_Models.Models_Table_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Select.BaiTap
{
    public class BaiTapNopModel
    {
        public int idNopBaiTap { get; set; }
        public float diem { get; set; }
        public string? danhGia { get; set; }
        public DateTime createTime { get; set; }
        public List<tbFileNopBaiTap>? file { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Models.Models_Table_CSDL
{
    public class tbKhoaSchool
    {
        [Key]
        public int id_KhoaSchool { get; set; }
        public string? name_Khoa { get; set; }
        public string? ma_Khoa { get; set; }
        public int id_School { get; set; }


        [ForeignKey("id_School")]
        public virtual tbSchool? tbSchool { get; set; }
    }
}

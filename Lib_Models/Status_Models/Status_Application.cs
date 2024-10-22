﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Status_Model
{
    public class Status_Application
    {
        public bool StatusBool { get; set; }
        public string? StatusType { get; set; }
        public int Id_Int { get; set; }
        public List<int>? List_Id_Int { get; set; }
        public List<string>? List_String_Int { get; set; }
        public Guid Id_Guid { get; set; }
        public object? myObj { get; set; }
        public List<object>? myListObj { get; set; }
        public string? Id_String { get; set; }
        public string? TypeImageString { get; set; }
        public string? TypeVideoString { get; set; }
    }
}

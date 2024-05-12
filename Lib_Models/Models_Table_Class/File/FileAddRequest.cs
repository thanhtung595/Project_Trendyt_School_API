using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Models_Table_Class.File
{
    public class FileAddRequest
    {
        public string? path { get; set; }
        public string? newFileName { get; set; }
        public IFormFile? file { get; set; }
    }
}

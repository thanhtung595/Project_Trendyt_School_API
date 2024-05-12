using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Models.Model_Update.Member
{
    public class UpdateMember_Profile
    {
        public string? fullName { get; set; }
        public DateTime birthday_User { get; set; }
        public string? sex_User { get; set; }
        public string? phone_User { get; set; }
        public IFormFile? imageUser { get; set; }
    }
}

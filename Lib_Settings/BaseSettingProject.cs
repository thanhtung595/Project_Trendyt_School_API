using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Settings
{
    public static class BaseSettingProject
    {
        public static readonly int EXPIRES_ACCESSTOKEN = 5;
        public static readonly int EXPIRES_REFESHTOKEN = 10080;

        public static readonly string ACCESSTOKEN = "access_token";
        public static readonly string KEYSCRFT = "KEYSCRFT";
    }
}

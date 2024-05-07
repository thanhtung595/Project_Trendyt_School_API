using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.PublicServices.CookieService
{
    public interface ICustomCookieService
    {
        void DeleteCokie(string key);
        public void SetCookie(string domain, string key, string value, int expiresMinutes);
        void SetCookieAllTime(string domain, string key, string value);
    }
}

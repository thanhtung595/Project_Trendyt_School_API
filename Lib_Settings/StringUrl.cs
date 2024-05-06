using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Settings
{
    public static class StringUrl
    {
        public static readonly string PortServer = ":7777";
        public static readonly string PortClient1 = ":5500";
        public static readonly string PortClient2 = ":3000";

        public static readonly string UrlServer = "http://thanhtung.com";
        public static readonly string UrlClient1 = "http://thanhtung.com";
        public static readonly string UrlClient2 = "http://localhost";
        public static readonly string UrlClientProduction1 = "https://trendyt.netlify.app";
        public static readonly string UrlClientProduction2 = "https://trendyt.online";

        public static readonly string DomainCookieServer = "thanhtung.com";
        //public static string DomainCookieClient1 = "http://thanhtung.com";
        public static readonly string DomainCookieClient2 = "localhost";
        //public static string DomainCookieProduction = "http://thanhtung.com";
    }
}

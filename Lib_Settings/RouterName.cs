using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Settings
{
    public static class RouterName
    {
        private const string ADMIN = "/admin";
        public static class VersionApi
        {
            public const string VERSIION1 = "1.0";
        }
        public static class API
        {
            public const string APIV1 = "api/v" + VersionApi.VERSIION1;
        }
        public static class RouterAdminControllerName
        {
            public const string Authentication = API.APIV1 + ADMIN + "/auth";
            public const string School = API.APIV1 + ADMIN + "/school";
        } // RouterName.RouterAdminControllerName.

        public static class RouterControllerName
        {
            public const string Authentication = API.APIV1 + "/auth";
            public const string Class_School = API.APIV1 + "/class-school";
            public const string ClassSchool_Menber = API.APIV1 + "/class-school-member";
            public const string Khoa = API.APIV1 + "/khoa";
            public const string LichHoc = API.APIV1 + "/lichhoc";
            public const string Member = API.APIV1 + "/member";
            public const string Member_Profile = API.APIV1 + "/member-profile";
            public const string MonHoc = API.APIV1 + "/monhoc";
            public const string MonHocSudent = API.APIV1 + "/monhoc-student";
            public const string RoleSchool = API.APIV1 + "/role-school";
            public const string School = API.APIV1 + "/school";
            public const string Student = API.APIV1 + "/student";
            public const string Teacher = API.APIV1 + "/teacher";
            public const string DiemDanh = API.APIV1 + "/diemdanh";
            public const string StyleBuoiHoc = API.APIV1 + "/tinhtrangbuoihoc";
            public const string BaiTap = API.APIV1 + "/baitap";
        } // RouterName.RouterControllerName.
    }
}
//api/v1/login
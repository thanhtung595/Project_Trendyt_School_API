using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyT_Data.Identity
{
    public class IdentityData
    {
        public const string TypeRole = "typeRole";
        public const string QuanLyKhoaManager = "QuanLyKhoaManager";
        public const string QuanLySchoolManager = "QuanLySchoolManager";
        public const string ScuritySchool = "QuanLySchoolManager";
        public const string TeacherAndStudent = "TeacherStudent";

        public const string AdminServerPolicyName = "admin";
        public const string AdminServerClaimName = "admin";

        public const string GuestClientPolicyName = "guest";
        public const string GuestClientClaimName = "guest";

        public const string AdminSchoolPolicyName = "school management";
        public const string AdminSchoolClaimName = "school management";

        public const string IndustryPolicyName = "industry management";
        public const string IndustryClaimName = "industry management";

        public const string SecretaryPolicyName = "secretary management";
        public const string SecretaryClaimName = "secretary management";

        public const string TeacherPolicyName = "teacher";
        public const string TeacherClaimName = "teacher";

        public const string StudentPolicyName = "student";
        public const string StudentClaimName = "student";

    }
}
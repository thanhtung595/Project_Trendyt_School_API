using App_Models.Models_Table_CSDL;
using Lib_Models.Models_Table_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyT_Data.Identity;

namespace Lib_DataEntity.ContextSeed
{
    public static class SeedData
    {
        public static readonly tbStyleBuoiHoc[] tbStyleBuoiHoc =
        {
            new tbStyleBuoiHoc{id_StyleBuoiHoc = 1, name = "Học binh thường"},
            new tbStyleBuoiHoc{id_StyleBuoiHoc = 2, name = "Kiểm tra giữ môn"},
            new tbStyleBuoiHoc{id_StyleBuoiHoc = 3, name = "Kiểm tra cuối môn"},
        };

        public static readonly tbTag[] tbTag =
        {
            new tbTag{id_Tag = 1, name = "active"},
            new tbTag{id_Tag = 2, name = "delete"},
            new tbTag{id_Tag = 3, name = "done"},
        };

        public static readonly tbTypeAccount[] tbTypeAccount =
        {
            new tbTypeAccount{id_TypeAccount = Guid.NewGuid(), name_TypeAccount = "account name"},
            new tbTypeAccount{id_TypeAccount = Guid.NewGuid(), name_TypeAccount = "gmail"},
            new tbTypeAccount{id_TypeAccount = Guid.NewGuid(), name_TypeAccount = "facebook"},
        };

        public static readonly tbRole[] tbRoles =
        {
            new tbRole{id_Role = Guid.NewGuid(), name_Role = IdentityData.AdminServerClaimName},
            new tbRole{id_Role = Guid.NewGuid(), name_Role = IdentityData.GuestClientClaimName},
        };
        public static readonly tbRoleSchool[] tbRoleSchool =
        {
            new tbRoleSchool{id_RoleSchool = Guid.NewGuid(), name_Role = IdentityData.AdminSchoolClaimName},
            new tbRoleSchool{id_RoleSchool = Guid.NewGuid(), name_Role = IdentityData.IndustryClaimName},
            new tbRoleSchool{id_RoleSchool = Guid.NewGuid(), name_Role = IdentityData.SecretaryClaimName},
            new tbRoleSchool{id_RoleSchool = Guid.NewGuid(), name_Role = IdentityData.TeacherClaimName},
            new tbRoleSchool{id_RoleSchool = Guid.NewGuid(), name_Role = IdentityData.StudentClaimName},
        };
    }
}

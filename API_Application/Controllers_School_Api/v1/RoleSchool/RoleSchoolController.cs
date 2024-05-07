using Lib_Services.V1.RoleSchool_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

namespace API_Application.Controllers_School_Api.v1.RoleSchool
{
    [Route(RouterName.RouterControllerName.RoleSchool)]
    [ApiController]
    public class RoleSchoolController : ControllerBase
    {
        private readonly IRoleSchool_Service_v1 _roleSchool_Service_V1;
        public RoleSchoolController(IRoleSchool_Service_v1 roleSchool_Service_V1)
        {
            _roleSchool_Service_V1 = roleSchool_Service_V1;
        }

        // Role school start
        #region Get All Role School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpGet]
        public async Task<IActionResult> GetAllRoleSchool()
        {
            return Ok(await _roleSchool_Service_V1.SelectAllAsync());
        }
        #endregion
    }
}

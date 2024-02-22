using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using Lib_Services.V1.School_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

namespace API_Application.Areas.Admin.Controllers_Api.v1
{
    [Route("api/admin/v1/school")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchool_Service_v1 _schoolService_v1;
        public SchoolController(ISchool_Service_v1 schoolService_v1)
        {
            _schoolService_v1 = schoolService_v1;
        }

        #region Insert School
        [Authorize(Policy = IdentityData.AdminServerPolicyName)]
        [HttpPost]
        public async Task<IActionResult> Insert(School_Insert_v1 request)
        {
            Status_Application status = await _schoolService_v1.InsertAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400,status.StatusType);
            }
            return StatusCode(201);
        }
        #endregion
    }
}

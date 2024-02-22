using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using Lib_Services.V1.TypeAccount_Service;
using Microsoft.AspNetCore.Mvc;

namespace API_Application.Areas.Admin.Controllers_Api.v1
{
    [Route("api/admin/v1/typeaccount")]
    [ApiController]
    public class TypeAccountController : ControllerBase
    {
        private readonly ITypeAccount_Service_v1 _typeAccountService_v1;

        public TypeAccountController(ITypeAccount_Service_v1 typeAccount_Service_V1)
        {
            _typeAccountService_v1 = typeAccount_Service_V1;
        }

        #region Select All TypeAccount
        [HttpGet]
        public async Task<IActionResult> SelectAll()
        {
            return Ok(await _typeAccountService_v1.SelectAll());
        }
        #endregion

        #region Insert TypeAccount
        [HttpPost]
        public async Task<IActionResult> Insert(TypeAccount_Insert_v1 request)
        {
            Status_Application status = await _typeAccountService_v1.InsertAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400,status.StatusType);
            }
            return StatusCode(201);
        }
        #endregion
    }
}

using API_Application.Service.FileService;
using Lib_Models.Models_Insert.v2.BaiTap;
using Lib_Models.Status_Model;
using Lib_Services.V2.BaiTap_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.BaiTap
{
    [Route(RouterName.RouterControllerName.BaiTap)]
    [ApiController]
    public class BaiTapController : ControllerBase
    {
        private readonly IBaiTap_Service_v2 _baiTapService_v2;
        public BaiTapController(IBaiTap_Service_v2 baiTapService_v2)
        {
            _baiTapService_v2 = baiTapService_v2;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _baiTapService_v2.GetAll());
        }

        [Authorize(Policy = IdentityData.TeacherPolicyName)]
        [HttpPost]
        public async Task<IActionResult> Add(BaiTap_Insert_v2 baiTap)
        {
            Status_Application status = await _baiTapService_v2.Add(baiTap);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }

            if (baiTap.files!.Count() > 0)
            {
                FileInFolder.AddFileBaiTapInFolder_FileSrc(baiTap.files!, status.List_String_Int!); 
            }
            return StatusCode(201);
        }
    }
}

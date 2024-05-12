using API_Application.Service.FileService;
using Lib_Models.Model_Update.Member;
using Lib_Models.Models_Table_Class.File;
using Lib_Services.V1.Menber_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

namespace API_Application.Controllers_School_Api.v1.Menber
{
    [Route(RouterName.RouterControllerName.Member_Profile)]
    [ApiController]
    public class Member_ProfileController : ControllerBase
    {
        private readonly IMenber_Service_v1 _menber_Service_V1;
        public Member_ProfileController(IMenber_Service_v1 menber_Service_V1)
        {
            _menber_Service_V1 = menber_Service_V1;
        }

        [Authorize(Policy = IdentityData.ScuritySchool)]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            return Ok(await _menber_Service_V1.Profile());
        }

        //[Authorize(Policy = IdentityData.ScuritySchool)]
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateMember_Profile request)
        {
            FileAddRequest addFile = new FileAddRequest
            {
                file = request.imageUser,
                newFileName = "thanhtung",
                path = StringFilePath.WWWROOT_IMG
            };
            FileInFolder.AddFileInFolder_FileSrc(addFile);
            return Ok(request);
        }
    }
}

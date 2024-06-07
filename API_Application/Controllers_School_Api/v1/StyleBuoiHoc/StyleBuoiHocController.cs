using Lib_Services.V2.StyleBuoiHoc;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.StyleBuoiHoc
{
    [Route(RouterName.RouterControllerName.StyleBuoiHoc)]
    [ApiController]
    public class StyleBuoiHocController : ControllerBase
    {
        private readonly IStyleBuoiHoc_Service_v2 _styleBuoiHoc_Service_v2;
        public StyleBuoiHocController(IStyleBuoiHoc_Service_v2 styleBuoiHoc_Service_v2)
        {
            _styleBuoiHoc_Service_v2 = styleBuoiHoc_Service_v2;
        }
        [Authorize(Policy = IdentityData.ScuritySchool)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _styleBuoiHoc_Service_v2.GetAllAsync());
        }
    }
}

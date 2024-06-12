using Lib_Models.Model_Update.DiemDanh;
using Lib_Services.V2.DiemDanh;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.DiemDanh
{
    [Route(RouterName.RouterControllerName.DiemDanh)]
    [ApiController]
    public class DiemDanhController : ControllerBase
    {
        private readonly IDiemDanh_Service_v2 _diemDanh_Service_v2;
        public DiemDanhController(IDiemDanh_Service_v2 diemDanh_Service_v2)
        {
            _diemDanh_Service_v2 = diemDanh_Service_v2;
        }



        [Authorize(Policy = IdentityData.ScuritySchool)]
        [HttpPost]
        public async Task<IActionResult> Update(LopDiemDanh_Update_v1 request)
        {
            return StatusCode(201);
        }
    }
}

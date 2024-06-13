using Lib_Models.Model_Update.DiemDanh;
using Lib_Models.Status_Model;
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

        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        public async Task<IActionResult> GetAllDiemDanhMonHoc(int idMonHoc)
        {
            return Ok(await _diemDanh_Service_v2.GetDiemDanhMonHocAsync(idMonHoc));
        }

        [Authorize(Policy = IdentityData.TeacherPolicyName)]
        [HttpPost]
        public async Task<IActionResult> Update(List<LopDiemDanh_Update_v1> request)
        {
            Status_Application status = await _diemDanh_Service_v2.UpdateAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(201 , status.StatusType);
        }
    }
}

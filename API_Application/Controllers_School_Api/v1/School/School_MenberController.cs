using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Model_Update.School;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using Lib_Services.V1.Menber_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.School
{
    [Route("api/v1/school/menber")]
    [ApiController]
    public class School_MenberController : ControllerBase
    {
        private readonly IMenber_Service_v1 _menber_Service_V1;
        private readonly Trendyt_DbContext _db;
        public School_MenberController(IMenber_Service_v1 menber_Service_V1, Trendyt_DbContext db)
        {
            _menber_Service_V1 = menber_Service_V1;
            _db = db;
        }

        // Menber school start
        #region Get all Menber School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpGet]
        public async Task<IActionResult> GetAllMenberSchool()
        {
            return Ok(await _menber_Service_V1.SelectAllAsync());
        }
        #endregion

        #region Add Menber School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPost]
        public async Task<IActionResult> AddMenberSchool(List<MenberSchool_Insert_v1> listRequest)
        {
            var executionStrategy = _db.Database.CreateExecutionStrategy();

            IActionResult result = null!;

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var itemMenber in listRequest)
                        {
                            Status_Application status_add = await _menber_Service_V1.InsertAsync(itemMenber);
                            if (!status_add.StatusBool)
                            {
                                // Nếu có lỗi, rollback giao dịch và trả về lỗi
                                dbContextTransaction.Rollback();
                                result = StatusCode(400, status_add.StatusType);
                                return;
                            }
                        }
                        // Nếu mọi thứ thành công, commit giao dịch
                        dbContextTransaction.Commit();
                        result = StatusCode(201);
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, rollback giao dịch và trả về lỗi
                        dbContextTransaction.Rollback();
                        result = StatusCode(500, $"Error: {ex.Message}");
                    }
                }
            });

            return result!;
        }
        #endregion

        #region Edit Menber School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPut]
        public async Task<IActionResult> EditMenberSchool(School_Menber_Update_v1 request)
        {
            Status_Application status = await _menber_Service_V1.SchoolMenberUpdateAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion

        #region Delete Menber School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpDelete]
        public async Task<IActionResult> DeleteMenberSchool()
        {
            return Ok();
        }
        #endregion
        // Menber school end
    }
}

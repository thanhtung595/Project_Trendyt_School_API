using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Model_Update.Khoa;
using Lib_Models.Model_Update.School;
using Lib_Models.Models_Insert.v1;
using Lib_Models.Status_Model;
using Lib_Services.V1.KhoaSchool_Service;
using Lib_Services.V1.Menber_Service;
using Lib_Services.V1.RoleSchool_Service;
using Lib_Services.V1.School_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1
{
    [Route("api/v1/school")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchool_Service_v1 _school_Service_v1;
        private readonly IKhoaSchool_Service_v1 _khoaSchool_Service_V1;
        private readonly IRoleSchool_Service_v1 _roleSchool_Service_V1;
        private readonly IMenber_Service_v1 _menber_Service_V1;
        private readonly Trendyt_DbContext _db;
        public SchoolController(ISchool_Service_v1 school_Service_V1, IRoleSchool_Service_v1 roleSchool_Service_V1,
            IMenber_Service_v1 menber_Service_V1, Trendyt_DbContext db, IKhoaSchool_Service_v1 khoaSchool_Service_V1)
        {
            _school_Service_v1 = school_Service_V1;
            _roleSchool_Service_V1 = roleSchool_Service_V1;
            _menber_Service_V1 = menber_Service_V1;
            _db = db;
            _khoaSchool_Service_V1 = khoaSchool_Service_V1;
        }

        // School start
        #region Get All School
        [HttpGet]
        public async Task<IActionResult> GetAllSchool()
        {
            return Ok(await _school_Service_v1.SelectAllAsync());
        }
        #endregion

        #region Edit School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPut]        
        public async Task<IActionResult> EditSchool(School_Update_v1 request)
        {
            Status_Application status = await _school_Service_v1.UpdateAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion

        #region Get All Role School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpGet("get-all-role")]
        public async Task<IActionResult> GetAllRoleSchool()
        {
            return Ok(await _roleSchool_Service_V1.SelectAllAsync());
        }
        #endregion
        // School end

        // Khoa school start
        #region Get All Khoa School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpGet("khoa")]
        public async Task<IActionResult> GellAllKhoaSchool()
        {
            return Ok(await _khoaSchool_Service_V1.SelectAll());
        }
        #endregion

        #region Add Khoa School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPost("khoa")]
        public async Task<IActionResult> AddKhoaSchool(KhoaSchool_Insert_v1 reques)
        {
            Status_Application status = await _khoaSchool_Service_V1.InsertAysnc(reques);
            if (!status.StatusBool)
            {
                return StatusCode(400,status.StatusType);
            }
            return StatusCode(201);
        }
        #endregion

        #region Edit Khoa School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPut("khoa")]
        public async Task<IActionResult> EditKhoaSchool(KhoaSchool_Update_v1 khoaSchool)
        {
            Status_Application status = await _khoaSchool_Service_V1.UpdateAysnc(khoaSchool);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion
        // Khoa school end

        // Menber school start
        #region Get all Menber School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpGet("menber")]
        public async Task<IActionResult> GetAllMenberSchool()
        {
            return Ok(await _menber_Service_V1.SelectAllAsync());
        }
        #endregion

        #region Add Menber School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpPost("menber")]
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
        [HttpPut("menber")]
        public async Task<IActionResult> EditMenberSchool()
        {
            return Ok();
        }
        #endregion

        #region Delete Menber School
        [Authorize(Policy = IdentityData.AdminSchoolPolicyName)]
        [HttpDelete("menber")]
        public async Task<IActionResult> DeleteMenberSchool()
        {
            return Ok();
        }
        #endregion
        // Menber school end
    }
}

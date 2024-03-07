using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Insert.v1.Class_School;
using Lib_Models.Status_Model;
using Lib_Services.V1.Class_Member_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.Class_School
{
    [Route("api/v1/class/menber")]
    [ApiController]
    public class ClassSchool_MenberController : ControllerBase
    {
        private readonly Trendyt_DbContext _db;
        private readonly IClass_Member_Service_v1 _class_member_service_V1;
        public ClassSchool_MenberController(Trendyt_DbContext db, IClass_Member_Service_v1 class_Member_Service_V1)
        {
            _db = db;
            _class_member_service_V1 = class_Member_Service_V1;
        }

        #region Add
        [Authorize(Policy = IdentityData.QuanLyKhoaManager)]
        [HttpPost]
        public async Task<IActionResult> Add(List<Class_Member_Insert_v1> request)
        {
            var executionStrategy = _db.Database.CreateExecutionStrategy();
            IActionResult result = null!;

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var db = _db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in request)
                        {
                            Class_Member_Insert_v1 addStudent = new Class_Member_Insert_v1
                            {
                                id_ClassSchool = item.id_ClassSchool,
                                id_Student = item.id_Student
                            };
                            Status_Application statusStudent = await _class_member_service_V1.Insert(addStudent);
                            if (!statusStudent.StatusBool)
                            {
                                db.Rollback();
                                result = StatusCode(400, statusStudent.StatusType);
                                return;
                            }
                        }
                        // Nếu mọi thứ thành công, commit giao dịch
                        db.Commit();
                        result = StatusCode(201);
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, rollback giao dịch và trả về lỗi
                        db.Rollback();
                        result = StatusCode(500, $"Error: {ex.Message}");
                    }
                }
            });
            return result;
        }
        #endregion

        #region Delete
        [Authorize(Policy = IdentityData.QuanLyKhoaManager)]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery(Name = "id_ClassSchool")] int id_ClassSchool, [FromBody]List<int> id_Student)
        {
            var executionStrategy = _db.Database.CreateExecutionStrategy();
            IActionResult result = null!;

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var db = _db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in id_Student)
                        {
                            Status_Application status = await _class_member_service_V1.Delete(id_ClassSchool, item);
                            if (!status.StatusBool)
                            {
                                db.Rollback();
                                result = StatusCode(400, status.StatusType);
                                return;
                            }
                        }
                        db.Commit();
                        result = StatusCode(201);
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, rollback giao dịch và trả về lỗi
                        db.Rollback();
                        result = StatusCode(500, $"Error: {ex.Message}");
                    }
                }
            });
            return result;
        }
        #endregion
    }
}

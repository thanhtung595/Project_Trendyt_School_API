using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Models_Insert.v1.Class_School;
using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Status_Model;
using Lib_Services.V1.MonHoc_Student_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.MonHoc
{
    [Route(RouterName.RouterControllerName.MonHocSudent)]
    [ApiController]
    public class MonHocSudentController : ControllerBase
    {
        private readonly Trendyt_DbContext _db;
        private readonly IMonHoc_Student_Service_v1 _monHoc_Student_Service_V1;
        public MonHocSudentController(Trendyt_DbContext db, IMonHoc_Student_Service_v1 monHoc_Student_Service_V1)
        {
            _db = db;
            _monHoc_Student_Service_V1 = monHoc_Student_Service_V1;
        }

        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpPost]
        public async Task<IActionResult> AddStudent(List<MonHocClass_Student_Insert_v1> student)
        {
            var executionStrategy = _db.Database.CreateExecutionStrategy();
            IActionResult result = null!;

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        if (student!.Count() != 0)
                        {
                            // Add student
                            foreach (var st in student)
                            {
                                MonHocClass_Student_Insert_v1 addStudent = new MonHocClass_Student_Insert_v1
                                {
                                    id_MonHoc = st.id_MonHoc,
                                    id_Student = st.id_Student,
                                };

                                Status_Application statusAddStudent = await _monHoc_Student_Service_V1.Insert(addStudent);
                                if (!statusAddStudent.StatusBool)
                                {
                                    dbContextTransaction.Rollback();
                                    result = StatusCode(400, statusAddStudent.StatusType);
                                    return;
                                }
                            }
                        }
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
            return result;
        }

        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(List<MonHocClass_Student_Insert_v1> student)
        {
            var executionStrategy = _db.Database.CreateExecutionStrategy();
            IActionResult result = null!;

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var dbContextTransaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        if (student!.Count() != 0)
                        {
                            // Add student
                            foreach (var st in student)
                            {
                                MonHocClass_Student_Insert_v1 addStudent = new MonHocClass_Student_Insert_v1
                                {
                                    id_MonHoc = st.id_MonHoc,
                                    id_Student = st.id_Student,
                                };

                                Status_Application statusAddStudent = await _monHoc_Student_Service_V1.Delete(addStudent);
                                if (!statusAddStudent.StatusBool)
                                {
                                    dbContextTransaction.Rollback();
                                    result = StatusCode(400, statusAddStudent.StatusType);
                                    return;
                                }
                            }
                        }
                        dbContextTransaction.Commit();
                        result = StatusCode(204);
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, rollback giao dịch và trả về lỗi
                        dbContextTransaction.Rollback();
                        result = StatusCode(500, $"Error: {ex.Message}");
                    }
                }
            });
            return result;
        }
    }
}

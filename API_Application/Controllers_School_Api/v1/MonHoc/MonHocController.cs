using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Model_Update.MonHoc;
using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Services.V1.MonHoc;
using Lib_Services.V1.MonHoc_Student_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.MonHoc
{
    [Route("api/v1/mon-hoc")]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        private readonly Trendyt_DbContext _db;
        private readonly IMonHoc_Service_v1 _monHoc_Service_V1;
        private readonly IMonHoc_Student_Service_v1 _monHoc_Student_Service_V1;
        public MonHocController(Trendyt_DbContext db, IMonHoc_Service_v1 monHoc_Service_V1, IMonHoc_Student_Service_v1 monHoc_Student_Service_V1)
        {
            _db = db;
            _monHoc_Service_V1 = monHoc_Service_V1;
            _monHoc_Student_Service_V1 = monHoc_Student_Service_V1;
        }

        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _monHoc_Service_V1.GetAll());
        }

        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpPost]
        public async Task<IActionResult> Add(MonHoc_Insert_v1 request)
        {
            var executionStrategy = _db.Database.CreateExecutionStrategy();
            IActionResult result = null!;

            await executionStrategy.ExecuteAsync(async () =>
            {
                using(var dbContextTransaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        // Add mon hoc
                        tbMonHoc monHocAdd = new tbMonHoc
                        {
                            name_MonHoc = request.name_MonHoc,
                            ngayBatDau = request.ngayBatDau,
                            ngayKetThuc = request.ngayKetThuc,
                            _SoBuoiNghi = request._SoBuoiNghi
                        };
                        Status_Application statusAddMonHoc = await _monHoc_Service_V1.Insert(monHocAdd);
                        if (!statusAddMonHoc.StatusBool)
                        {
                            dbContextTransaction.Rollback();
                            result = StatusCode(400, statusAddMonHoc.StatusType);
                            return;
                        }
                        // Add teacher

                        MonHocClass_Student_Insert_v1 addTeacher = new MonHocClass_Student_Insert_v1
                        {
                            id_MonHoc = statusAddMonHoc.Id_Int,
                            id_Student = request.id_Teacher
                        };
                        Status_Application statusAddTeacher = await _monHoc_Student_Service_V1.Insert(addTeacher);
                        if (!statusAddTeacher.StatusBool)
                        {
                            dbContextTransaction.Rollback();
                            result = StatusCode(400, statusAddTeacher.StatusType);
                            return;
                        }
                        if (request.student!.Count() != 0)
                        {
                            MonHocClass_Student_Insert_v1 addStudent = new MonHocClass_Student_Insert_v1
                            {
                                id_MonHoc = statusAddMonHoc.Id_Int,
                                
                            };
                            // Add student
                            foreach (var st in request.student!)
                            {
                                addStudent.id_Student = st.id_Student;
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
        [HttpGet]
        [Route("by-id")]
        public async Task<IActionResult> GetById([FromQuery(Name = "id")] int id)
        {
            return Ok(await _monHoc_Service_V1.GetById(id));
        }

        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpPut]
        public async Task<IActionResult> Edit(MonHoc_Update_v1 request)
        {
            Status_Application status = await _monHoc_Service_V1.Edit(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType); 
            }
            return StatusCode(204);
        }
    }
}

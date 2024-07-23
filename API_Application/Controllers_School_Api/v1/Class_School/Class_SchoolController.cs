using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Model_Update.Class;
using Lib_Models.Models_Insert.v1.Class_School;
using Lib_Models.Status_Model;
using Lib_Services.V1.Class_Member_Service;
using Lib_Services.V1.Class_Service;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.Class_School
{
    [Route(RouterName.RouterControllerName.Class_School)]
    [ApiController]
    public class Class_SchoolController : ControllerBase
    {
        private readonly IClass_Service_v1 _class_Service_V1;
        private readonly Trendyt_DbContext _db;
        private readonly IClass_Member_Service_v1 _class_Member_Service_V1;
        public Class_SchoolController(IClass_Service_v1 class_Service_V1, Trendyt_DbContext db,
            IClass_Member_Service_v1 class_Member_Service_V1)
        {
            _class_Service_V1 = class_Service_V1;
            _db = db;
            _class_Member_Service_V1 = class_Member_Service_V1;
        }

        #region Show All
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        public async Task<IActionResult> ShowAll()
        {
            return Ok(await _class_Service_V1.SelectAll());
        }
        #endregion

        //#region Show one
        //[Authorize(Policy = IdentityData.QuanLySchoolManager)]
        //[HttpGet]
        //[Route("show")]
        //public async Task<IActionResult> ShowOne([FromQuery(Name = "id_ClassSchool")] int id_ClassSchool)
        //{
        //    return Ok();
        //}
        //#endregion

        #region Add class
        [Authorize(Policy = IdentityData.QuanLyKhoaManager)]
        [HttpPost]
        public async Task<IActionResult> AddClas(ClassSchool_Insert_v1 request)
        {
            // Tạo CreateExecutionStrategy Rollback
            var executionStrategy = _db.Database.CreateExecutionStrategy();

            IActionResult result = null!;

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var dbConnectTration = _db.Database.BeginTransaction())
                {
                    try
                    {
                        Status_Application status = await _class_Service_V1.InsertAsync(request.name_ClassSchool!);
                        if (!status.StatusBool)
                        {
                            // Nếu có lỗi, rollback giao dịch và trả về lỗi
                            dbConnectTration.Rollback();
                            result = StatusCode(400, status.StatusType);
                            return;
                        }
                        Class_Member_Insert_v1 addTeacher = new Class_Member_Insert_v1
                        {
                            id_ClassSchool = status.Id_Int,
                            id_Student = request.id_Teacher
                        };
                        Status_Application statusTeacher = await _class_Member_Service_V1.Insert(addTeacher);
                        if (!statusTeacher.StatusBool)
                        {
                            dbConnectTration.Rollback();
                            result = StatusCode(400, statusTeacher.StatusType);
                            return;
                        }

                        if (request.student!.Count() != 0)
                        {
                            Class_Member_Insert_v1 addStudent = new Class_Member_Insert_v1
                            {
                                id_ClassSchool = status.Id_Int,
                            };
                            foreach (var item in request.student!)
                            {
                                addStudent.id_Student = item.id_Student;
                                Status_Application statusStudent = await _class_Member_Service_V1.Insert(addStudent);
                                if (!statusStudent.StatusBool)
                                {
                                    dbConnectTration.Rollback();
                                    result = StatusCode(400, statusStudent.StatusType);
                                    return;
                                }
                            }
                        }
                        // Nếu mọi thứ thành công, commit giao dịch
                        dbConnectTration.Commit();
                        result = StatusCode(201);
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, rollback giao dịch và trả về lỗi
                        dbConnectTration.Rollback();
                        result = StatusCode(500, $"Error: {ex.Message}");
                    }
                }
           });
            return result;
        }
        #endregion

        #region Editclass
        [Authorize(Policy = IdentityData.QuanLyKhoaManager)]
        [HttpPut]
        public async Task<IActionResult> EditClas(Class_Update_v1 request)
        {
            Status_Application status = await _class_Service_V1.UpdateAsync(request);
            if (!status.StatusBool)
            {
                return StatusCode(400, status.StatusType);
            }
            return StatusCode(204);
        }
        #endregion

        #region Get by id
        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpGet]
        [Route("by-id")]
        public async Task<IActionResult> ShowAllById([FromQuery(Name ="id")] int id)
        {
            return Ok(await _class_Service_V1.SelectById(id));
        }
        #endregion

        [Authorize(Policy = IdentityData.ScuritySchool)]
        [HttpGet("member")]
        public async Task<IActionResult> GetMemberMonHoc([FromQuery] int idClass)
        {
            return Ok(await _class_Service_V1.GetMember(idClass));
            //return Ok(await _pROC_MonHoc.GetAllMonHocPROC(5));
        }
    }
}

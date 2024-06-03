using App_DataBaseEntity.DbContextEntity_SQL_Sever;
using Lib_Models.Model_Update.MonHoc;
using Lib_Models.Models_Insert.v1.MonHoc;
using Lib_Models.Models_Insert.v2.MonHoc;
using Lib_Models.Models_Table_Entity;
using Lib_Models.Status_Model;
using Lib_Services.PublicServices.SignalRService.NotificationHub;
using Lib_Services.V1.MonHoc;
using Lib_Services.V1.MonHoc_Student_Service;
using Lib_Services.V2.MonHoc_Service;
using Lib_Services.V2.MonHocClass_Student;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Stored_Procedures.PROC.MonHoc;
using TrendyT_Data.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.MonHoc
{
    [Route(RouterName.RouterControllerName.MonHoc)]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        private readonly Trendyt_DbContext _db;
        private readonly IPROC_MonHoc _pROC_MonHoc;
        private readonly IMonHoc_Service_v1 _monHoc_Service_V1;
        private readonly IMonHoc_Student_Service_v1 _monHoc_Student_Service_V1;
        private readonly IMonHoc_Service_v2 _monHoc_ServiceV2;
        private readonly IMonHocClass_Student_v2 _monHocClass_Student_V2;
        private readonly IHubContext<NotificationHub> _hubContext;
        public MonHocController(Trendyt_DbContext db, IMonHoc_Service_v1 monHoc_Service_V1, IMonHoc_Student_Service_v1 monHoc_Student_Service_V1,
                                IPROC_MonHoc pROC_MonHoc, IMonHoc_Service_v2 monHoc_ServiceV2, IMonHocClass_Student_v2 monHocClass_Student_V2,
                                IHubContext<NotificationHub> hubContext)
        {
            _db = db;
            _monHoc_Service_V1 = monHoc_Service_V1;
            _monHoc_Student_Service_V1 = monHoc_Student_Service_V1;
            _pROC_MonHoc = pROC_MonHoc;
            _monHoc_ServiceV2 = monHoc_ServiceV2;
            _monHocClass_Student_V2 = monHocClass_Student_V2;
            _hubContext = hubContext;
        }

        [Authorize(Policy = IdentityData.ScuritySchool)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _monHoc_Service_V1.GetAll());
            //return Ok(await _pROC_MonHoc.GetAllMonHocPROC(5));
        }

        [Authorize(Policy = IdentityData.QuanLySchoolManager)]
        [HttpPost]
        public async Task<IActionResult> Add(MonHoc_Insert_Request_v2 request)
        {
            Status_Application statusMonHoc = await _monHoc_ServiceV2.InsertAsync(request);
            if (!statusMonHoc.StatusBool)
            {
                return BadRequest(statusMonHoc.StatusType);
            }
            try
            {
                List<string> userIdNotifications = new List<string>();
                foreach (var student in request.students!)
                {
                    userIdNotifications.Add(student.id_Student.ToString());
                }
                userIdNotifications.Add(request.monHoc!.id_Teacher.ToString());
                // Sử dụng Parallel.ForEach để gửi thông báo đồng thời
                var tasks = new List<Task>();
                Parallel.ForEach(userIdNotifications, userId =>
                {
                    var connectionIds = NotificationHub.GetConnectionIds(userId);
                    foreach (var connectionId in connectionIds)
                    {
                        tasks.Add(_hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", request));
                    }
                });
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return StatusCode(201, statusMonHoc.StatusType);
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

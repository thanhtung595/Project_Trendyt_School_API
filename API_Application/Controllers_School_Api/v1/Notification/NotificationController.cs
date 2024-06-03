using Lib_Services.PublicServices.NotificationService;
using Lib_Services.PublicServices.SignalRService;
using Lib_Services.PublicServices.SignalRService.NotificationHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_School_Api.v1.Notification
{
    [Route("api/Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult ThongBao([FromBody] string message)
        {
            //NotificationQueue.EnqueueNotification(message);
            return Ok(new { success = true });
        }

        [HttpPost("ThongBao")]
        public async Task<IActionResult> ThongBao([FromBody] NotificationRequest request)
        {
            //// Sử dụng Parallel.ForEach để gửi thông báo đồng thời
            //var tasks = new List<Task>();
            //Parallel.ForEach(request.UserIds, userId =>
            //{
            //    var connectionIds = NotificationHub.GetConnectionIds(userId);
            //    foreach (var connectionId in connectionIds)
            //    {
            //        tasks.Add(_hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", request.Message));
            //    }
            //});
            //await Task.WhenAll(tasks);
            return Ok(new { success = true });
        }
    }

    public class NotificationRequest
    {
        public List<string> UserIds { get; set; }
        public string Message { get; set; }
    }
}

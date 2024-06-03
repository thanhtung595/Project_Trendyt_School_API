using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.PublicServices.NotificationService
{
    public class NotificationBackgroundService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (NotificationQueue.TryDequeue(out var notification))
                {
                    // Gửi thông báo đến tất cả user ở đây
                    await SendNotificationToAllUsers(notification);
                }

                await Task.Delay(1000, stoppingToken); // Thời gian chờ giữa các lần kiểm tra hàng đợi
            }
        }
       private Task SendNotificationToAllUsers(string notification)
       {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Oke: " + i);
            }
            return Task.CompletedTask;
       }
    }
}

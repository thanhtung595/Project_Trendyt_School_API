using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.PublicServices.NotificationService
{
    public static class NotificationQueue
    {
        private static readonly ConcurrentQueue<string> _notifications = new ConcurrentQueue<string>();

        public static void EnqueueNotification(string notification)
        {
            _notifications.Enqueue(notification);
        }

        public static bool TryDequeue(out string notification)
        {
            return _notifications.TryDequeue(out notification!);
        }
    }
}

using App_Models.Models_Table_CSDL;
using Lib_Services.Token_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Services.PublicServices.SignalRService.NotificationHub
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly IToken_Service_v2 _token_Service_V2;
        // Dictionary để lưu trữ connectionId và userId
        private static readonly ConcurrentDictionary<string, string> UserConnections = new ConcurrentDictionary<string, string>();
        // Dictionary ngược để lưu trữ userId và danh sách connectionId
        private static readonly ConcurrentDictionary<string, HashSet<string>> UserIdConnections = new ConcurrentDictionary<string, HashSet<string>>();

        public NotificationHub(IToken_Service_v2 token_Service_V2)
        {
            _token_Service_V2 = token_Service_V2;
        }
        // Gọi khi một client kết nối tới Hub
        public override async Task OnConnectedAsync()
        {
            tbMenberSchool memberManager = await _token_Service_V2.Get_Menber_Token();
            var userId = memberManager.id_MenberSchool.ToString(); // Lấy userId từ AccessToken

            if (!string.IsNullOrEmpty(userId))
            {
                //// Lưu trữ connectionId và userId
                //UserConnections.TryAdd(Context.ConnectionId, userId);

                // Lưu trữ connectionId và userId
                UserConnections[Context.ConnectionId] = userId;
                UserIdConnections.AddOrUpdate(userId,
                _ => new HashSet<string> { Context.ConnectionId },
                (_, connections) => { connections.Add(Context.ConnectionId); return connections; });
            }

            await base.OnConnectedAsync();
        }

        // Gọi khi một client ngắt kết nối
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (UserConnections.TryRemove(Context.ConnectionId, out var userId))
            {
                if (UserIdConnections.TryGetValue(userId, out var connections))
                {
                    connections.Remove(Context.ConnectionId);
                    if (connections.Count == 0)
                    {
                        UserIdConnections.TryRemove(userId, out _);
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Lấy danh sách connectionId của userId cụ thể
        public static IEnumerable<string> GetConnectionIds(string userId)
        {
            if (UserIdConnections.TryGetValue(userId, out var connections))
            {
                return connections;
            }
            return Enumerable.Empty<string>();
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace PRN222.RoomBooking.Services.Hubs
{
    public class NotificationHub : Hub
    {
        // Khi user hoặc manager kết nối, thêm họ vào group dựa trên campus
        public override async Task OnConnectedAsync()
        {
            var userCode = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
            var userCampus = Context.User?.FindFirst(ClaimTypes.GivenName)?.Value;

            Console.WriteLine($"User connected: {userCode}, Role: {userRole}, Campus: {userCampus}, ConnectionId: {Context.ConnectionId}");

            if (userRole == "Manager" && !string.IsNullOrEmpty(userCampus))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userCampus);
            }
            // Không cần thêm user vào group vì ta dùng Clients.User(userCode) để gửi trực tiếp

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userCampus = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var userRole = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == "Manager" && !string.IsNullOrEmpty(userCampus))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userCampus);
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Gửi thông báo đến một user cụ thể
        public async Task SendNotificationToUser(string userCode, string message)
        {
            await Clients.User(userCode).SendAsync("ReceiveNotification", message);
        }

        // Gửi thông báo đến tất cả manager của một campus
        public async Task SendNotificationToCampusManagers(string campusName, string message)
        {
            await Clients.Group(campusName).SendAsync("ReceiveNotification", message);
        }
    }
}

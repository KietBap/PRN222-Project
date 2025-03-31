using Microsoft.AspNetCore.SignalR;

namespace PRN222.RoomBooking.Services.Hubs
{
    public class NotificationHub : Hub
    {
        // Phương thức gửi thông báo đến một user cụ thể dựa trên UserCode
        public async Task SendNotificationToUser(string userCode, string message)
        {
            await Clients.User(userCode).SendAsync("ReceiveNotification", message);
        }

        // Phương thức gửi thông báo đến tất cả users (nếu cần)
        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}

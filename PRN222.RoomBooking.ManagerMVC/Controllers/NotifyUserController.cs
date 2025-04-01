using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PRN222.RoomBooking.Services.Hubs;

namespace PRN222.RoomBooking.ManagerMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyUserController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotifyUserController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotifyUserRequest request)
        {
            // Gửi thông báo tới user cụ thể
            await _hubContext.Clients.User(request.UserCode)
                .SendAsync("ReceiveNotification", request.Message);

            // Gửi sự kiện làm mới Booking History
            await _hubContext.Clients.User(request.UserCode)
                .SendAsync("RefreshBookingHistory", request.BookingId);

            return Ok();
        }
    }

    public class NotifyUserRequest
    {
        public string UserCode { get; set; }
        public int BookingId { get; set; }
        public string Message { get; set; }
    }
}
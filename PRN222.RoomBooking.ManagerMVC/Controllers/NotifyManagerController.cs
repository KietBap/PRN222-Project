using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PRN222.RoomBooking.Services.Hubs;

namespace PRN222.RoomBooking.ManagerMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyManagerController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotifyManagerController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotifyRequest request)
        {
            // Gửi ReceiveNotification để hiển thị thông báo
            await _hubContext.Clients.Group(request.CampusName)
                .SendAsync("ReceiveNotification", request.Message);

            // Gửi ReceiveBookingUpdate để làm mới danh sách
            await _hubContext.Clients.Group(request.CampusName)
                .SendAsync("ReceiveBookingUpdate", request.Message);

            return Ok();
        }
    }

    public class NotifyRequest
    {
        public string CampusName { get; set; }
        public string Message { get; set; }
    }
}

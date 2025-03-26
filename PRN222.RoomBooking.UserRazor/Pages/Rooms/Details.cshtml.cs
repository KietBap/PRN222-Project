using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Services;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.UserRazor.Pages.Rooms
{
    public class DetailsModel : PageModel
    {
        private readonly IRoomService _roomService;

        public DetailsModel(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public Room Room { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _roomService.GetRoomById(id.Value);
            if (Room == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
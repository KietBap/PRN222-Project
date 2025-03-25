using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.UserRazor.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly IRoomService _roomService;

        public IndexModel(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public List<Room> Rooms { get; set; } = new List<Room>();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public string? RoomName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Campus { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        public async Task<IActionResult> OnGetAsync(int page = 1)
        {
            CurrentPage = page;
            
            var (rooms, totalItems) = await _roomService.GetRoom(
                RoomName,
                Campus,
                SortBy,
                CurrentPage,
                PageSize
            );

            Rooms = rooms;
            TotalItems = totalItems;

            return Page();
        }
    }
}
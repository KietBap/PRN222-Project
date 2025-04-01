using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.UserRazor.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        public IndexModel(IRoomService roomService, IUserService userService)
        {
            _roomService = roomService;
            _userService = userService;
        }

        public List<Room> Rooms { get; set; } = new List<Room>();
        public int TotalItems { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        [BindProperty(SupportsGet = true)]
        public string? RoomName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Campus { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    TempData["LoginMessage"] = "You need to log in to view the room list.";
                    return RedirectToPage("/User/Login", new { ReturnUrl = HttpContext.Request.Path + HttpContext.Request.QueryString });
                }

                PageNumber = pageNumber < 1 ? 1 : pageNumber;

                // Lấy email hoặc UserCode từ claim
                //var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                var userCode = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Lấy thông tin người dùng từ cơ sở dữ liệu
                var user = await _userService.GetUserByCode(userCode); // Cần thêm phương thức này trong IUserService
                if (user == null || user.CampusId == null)
                {
                    TempData["ErrorMessage"] = "Unable to determine your campus. Please contact support.";
                    return RedirectToPage("/Index");
                }

                int userCampusId = user.CampusId;

                var (rooms, totalItems) = await _roomService.GetRoom(
                    RoomName,
                    Campus,
                    SortBy,
                    PageNumber,
                    PageSize,
                    userCampusId
                );

                Rooms = rooms;
                TotalItems = totalItems;

                if (PageNumber > TotalPages && TotalPages > 0)
                {
                    PageNumber = TotalPages;
                }

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while loading the room list: {ex.Message}";
                return RedirectToPage("/Index");
            }
        }

    }
}
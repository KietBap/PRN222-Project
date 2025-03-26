using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN222.RoomBooking.Services;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.ManagerMVC.Controllers
{
    [Authorize(Roles = "Manager")] // Restrict access to Manager role
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        public RoomsController(IRoomService roomService, IUserService userService)
        {
            _roomService = roomService;
            _userService = userService;
        }

        // GET: /Rooms/Index
        public async Task<IActionResult> Index(string roomName, string campus, string sortBy, int pageNumber = 1, int pageSize = 10)
        {
            // Get the logged-in user's UserCode from claims
            var userCode = User.FindFirst("UserCode")?.Value;
            if (string.IsNullOrEmpty(userCode))
            {
                return RedirectToAction("Login", "User");
            }

            // Fetch the user to get their CampusId
            var user = await _userService.GetUserByCode(userCode);
            if (user == null || user.CampusId == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Fetch rooms using the RoomService
            var (rooms, totalItems) = await _roomService.GetRoom(roomName, campus, sortBy, pageNumber, pageSize, user.CampusId);

            // Calculate total pages
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Pass data to the view using ViewBag or a view model
            ViewBag.RoomName = roomName;
            ViewBag.Campus = campus;
            ViewBag.SortBy = sortBy;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(rooms);
        }

        // GET: /Rooms/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var room = await _roomService.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }

            // Fetch booked room slots for this room
            var bookedRoomSlots = await _roomService.GetBookedRoomSlotsForRoomAsync(id);

            // Pass the booked room slots to the view using ViewBag
            ViewBag.BookedRoomSlots = bookedRoomSlots;

            return View(room);
        }

        // GET: /Rooms/ManageBookedSlots
        public async Task<IActionResult> ManageBookedSlots(int pageNumber = 1, int pageSize = 10)
        {
            // Get the logged-in user's UserCode from claims
            var userCode = User.FindFirst("UserCode")?.Value;
            if (string.IsNullOrEmpty(userCode))
            {
                return RedirectToAction("Login", "User");
            }

            // Fetch the user to get their CampusId
            var user = await _userService.GetUserByCode(userCode);
            if (user == null || user.CampusId == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Fetch booked room slots for the user's campus
            var (roomSlots, totalItems) = await _roomService.GetBookedRoomSlotsForCampusAsync(user.CampusId, pageNumber, pageSize);

            // Calculate total pages
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Pass data to the view using ViewBag
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(roomSlots);
        }


    }
}
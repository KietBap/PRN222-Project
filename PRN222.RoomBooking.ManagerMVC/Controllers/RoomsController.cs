using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.Enums;
using PRN222.RoomBooking.Repositories.UnitOfWork;
using PRN222.RoomBooking.Services;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.ManagerMVC.Controllers
{
    [Authorize(Roles = "Manager")] // Restrict access to Manager role
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork; // Added field

        public RoomsController(IRoomService roomService, IUserService userService, IUnitOfWork unitOfWork)
        {
            _roomService = roomService;
            _userService = userService;
            _unitOfWork = unitOfWork; // Added to constructor
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var campuses = await _unitOfWork.CampusRepository().GetAllAsync();
            ViewBag.Campuses = campuses;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            try
            {
                var userCode = User.FindFirst("UserCode")?.Value;
                if (string.IsNullOrEmpty(userCode))
                {
                    Console.WriteLine("UserCode not found");
                    return RedirectToAction("Login", "User");
                }

                var user = await _userService.GetUserByCode(userCode);
                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return RedirectToAction("Login", "User");
                }
                if (user.CampusId == null)
                {
                    Console.WriteLine("User has no CampusId");
                    return RedirectToAction("Login", "User");
                }

                // 🔹 Xóa toàn bộ ModelState trước khi validate lại
                ModelState.Clear();
                room.CampusId = user.CampusId;
                TryValidateModel(room);
                Console.WriteLine($"Assigned CampusId: {room.CampusId}");

                if (!ModelState.IsValid)
                {
                    Console.WriteLine("ModelState invalid:");
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                    var campuses = await _unitOfWork.CampusRepository().GetAllAsync();
                    ViewBag.Campuses = campuses;
                    return View(room);
                }

                await _roomService.CreateRoomAsync(room);
                Console.WriteLine($"Room created: {room.RoomId}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating room: {ex.Message}");
                ModelState.AddModelError("", $"Error creating room: {ex.Message}");
                var campuses = await _unitOfWork.CampusRepository().GetAllAsync();
                ViewBag.Campuses = campuses;
                return View(room);
            }
        }


        // GET: /Rooms/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomService.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: /Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
                TempData["SuccessMessage"] = "Room deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var room = await _roomService.GetRoomById(id);
                if (room == null)
                {
                    return NotFound();
                }
                ModelState.AddModelError("", $"Error deleting room: {ex.Message}");
                return View(room);
            }
        }
        // UPDATE

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomService.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }

            var campuses = await _unitOfWork.CampusRepository().GetAllAsync();
            ViewBag.Campuses = campuses;
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.RoomId)
            {
                return BadRequest();
            }

            try
            {
                var existingRoom = await _roomService.GetRoomById(id);
                if (existingRoom == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin phòng
                existingRoom.RoomName = room.RoomName;
                existingRoom.Capacity = room.Capacity;
                existingRoom.Status = room.Status;
                existingRoom.CampusId = room.CampusId;

                ModelState.Clear();
                TryValidateModel(existingRoom);

                if (!ModelState.IsValid)
                {
                    var campuses = await _unitOfWork.CampusRepository().GetAllAsync();
                    ViewBag.Campuses = campuses;
                    return View(room);
                }

                await _roomService.UpdateRoomAsync(existingRoom);
                TempData["SuccessMessage"] = "Room updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating room: {ex.Message}");
                var campuses = await _unitOfWork.CampusRepository().GetAllAsync();
                ViewBag.Campuses = campuses;
                return View(room);
            }
        }

    }
}
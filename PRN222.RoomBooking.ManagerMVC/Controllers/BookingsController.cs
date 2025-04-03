using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.Enums;
using PRN222.RoomBooking.Services;
using PRN222.RoomBooking.Services.Hubs;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.ManagerMVC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public BookingsController(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        // GET: /Bookings/ManagePending
        public async Task<IActionResult> ManagePending(int pageNumber = 1, int pageSize = 10)
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

            // Fetch pending bookings for the user's campus
            var (bookings, totalItems) = await _bookingService.GetPendingBookingsForCampusAsync(user.CampusId, pageNumber, pageSize);

            // Calculate total pages
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Pass data to the view using ViewBag
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(bookings);
        }

        // POST: /Bookings/ApproveBooking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveBooking(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            var success = await _bookingService.UpdateBookingStatusAsync(bookingId, BookingStatus.Booked);
            if (success)
            {
                TempData["SuccessMessage"] = "Booking has been successfully approved.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to approve the booking. It may no longer be in Pending status.";
            }
            return RedirectToAction("ManagePending");
        }

        // POST: /Bookings/CancelBooking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            var success = await _bookingService.UpdateBookingStatusAsync(bookingId, BookingStatus.Cancelled);
            if (success)
            {
                TempData["SuccessMessage"] = "Booking has been successfully canceled.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to cancel the booking. It may no longer be in Pending status.";
            }
            return RedirectToAction("ManagePending");
        }
    }
}
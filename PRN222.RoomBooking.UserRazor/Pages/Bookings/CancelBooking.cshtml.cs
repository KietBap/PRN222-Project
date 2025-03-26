using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using PRN222.RoomBooking.Repositories.Enums;

namespace PRN222.RoomBooking.UserRazor.Pages.Bookings
{
    public class CancelBookingModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public CancelBookingModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? bookingId)
        {
            if (!bookingId.HasValue)
            {
                ErrorMessage = "No booking ID provided.";
                return Page();
            }

            var userCode = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userCode))
            {
                ErrorMessage = "User is not logged in.";
                return Page();
            }

            var success = await _bookingService.CancelBookingAsync(bookingId.Value, userCode);

            if (success)
            {
                SuccessMessage = "Booking cancelled successfully.";
                return RedirectToPage("/Bookings/BookingHistory"); // Chuyển hướng về BookingHistory
            }
            else
            {
                var booking = await _bookingService.GetBookingByIdAsync(bookingId.Value);
                if (booking == null)
                {
                    ErrorMessage = "Booking does not exist.";
                }
                else if (booking.UserCode != userCode)
                {
                    ErrorMessage = "You do not have permission to cancel this booking.";
                }
                else if (booking.BookingStatus == BookingStatus.Cancelled)
                {
                    ErrorMessage = "Booking is already cancelled.";
                }
                else
                {
                    ErrorMessage = "Failed to cancel booking. It may no longer be cancellable.";
                }
                return Page();
            }
        }
    }
}
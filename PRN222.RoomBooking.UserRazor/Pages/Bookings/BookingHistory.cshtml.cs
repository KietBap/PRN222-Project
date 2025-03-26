using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Services;
using PRN222.RoomBooking.Repositories.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.UserRazor.Pages.Bookings
{
    public class BookingHistoryModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public BookingHistoryModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public int TotalItems { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        [BindProperty(SupportsGet = true)]
        public string StatusFilter { get; set; }

        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var userCode = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userCode))
            {
                ErrorMessage = "User is not logged in.";
                Bookings = new List<Booking>();
                return;
            }

            PageNumber = pageNumber < 1 ? 1 : pageNumber;

            BookingStatus? statusFilter = null;
            if (!string.IsNullOrEmpty(StatusFilter) && Enum.TryParse<BookingStatus>(StatusFilter, true, out var parsedStatus))
            {
                statusFilter = parsedStatus;
            }

            // Get paginated bookings
            var (bookings, totalItems) = await _bookingService.GetUserBookingsPaginatedAsync(userCode, statusFilter, PageNumber, PageSize);
            Bookings = bookings;
            TotalItems = totalItems;

            if (PageNumber > TotalPages && TotalPages > 0)
            {
                PageNumber = TotalPages;
            }
        }
    }
}
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.UnitOfWork;
using PRN222.RoomBooking.Services;
using PRN222.RoomBooking.Services.Hubs;
using System.Security.Claims;

namespace PRN222.RoomBooking.UserRazor.Pages.Bookings
{
    [Authorize(Roles = "User")]
    public class CreateBookingModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;

        public CreateBookingModel(IBookingService bookingService, IRoomService roomService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
        }

        [BindProperty]
        public DateTime BookingDate { get; set; }

        [BindProperty]
        public string Purpose { get; set; }

        [BindProperty]
        public List<int> SelectedRoomSlotIds { get; set; } = new List<int>();

        public List<RoomSlot> AvailableRoomSlots { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public int RoomId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? roomId, DateTime? bookingDate)
        {
            if (!roomId.HasValue || !bookingDate.HasValue)
            {
                ErrorMessage = "Invalid room or booking date selection.";
                return Page();
            }

            RoomId = roomId.Value;
            BookingDate = bookingDate.Value.Date;

            await LoadAvailableRoomSlots();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginMessage"] = "You need to log in to create a booking.";
                return RedirectToPage("/User/Login", new { ReturnUrl = HttpContext.Request.Path + HttpContext.Request.QueryString });
            }

            if (SelectedRoomSlotIds == null || !SelectedRoomSlotIds.Any())
            {
                ErrorMessage = "Please select at least one room slot.";
                await LoadAvailableRoomSlots();
                return Page();
            }

            var userCode = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userCode))
            {
                ErrorMessage = "User is not logged in.";
                await LoadAvailableRoomSlots();
                return Page();
            }

            var dateOnly = DateOnly.FromDateTime(BookingDate);
            var success = await _bookingService.CreateBookingAsync(userCode, dateOnly, SelectedRoomSlotIds, Purpose);

            if (success)
            {
                SuccessMessage = "Booking submitted successfully!";
                return RedirectToPage("/Bookings/BookingHistory");
            }
            else
            {
                ErrorMessage = "Failed to create booking. Some room slots may no longer be available.";
                await LoadAvailableRoomSlots();
                return Page();
            }
        }

        private async Task LoadAvailableRoomSlots()
        {
            var dateOnly = DateOnly.FromDateTime(BookingDate);
            AvailableRoomSlots = await _roomService.GetAvailableRoomSlotsAsync(RoomId, dateOnly);
        }
    }
}
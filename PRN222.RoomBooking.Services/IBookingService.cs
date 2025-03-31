using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.Enums;

namespace PRN222.RoomBooking.Services
{
    public interface IBookingService
    {
        Task<bool> CreateBookingAsync(string userCode, DateOnly bookingDate, List<int> roomSlotIds, string purpose);
        Task<bool> CancelBookingAsync(int bookingId, string userCode);
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<List<Booking>> GetUserBookingsAsync(string userCode, BookingStatus? statusFilter);
        Task<(List<Booking> bookings, int totalItems)> GetUserBookingsPaginatedAsync(string userCode, BookingStatus? statusFilter, int pageNumber, int pageSize);
        Task<List<int>> GetBookedRoomSlotIdsAsync(int roomId, DateOnly bookingDate);

        // For manager
        Task<(List<Booking> bookings, int totalItems)> GetPendingBookingsForCampusAsync(int campusId, int pageNumber, int pageSize);
        Task<bool> UpdateBookingStatusAsync(int bookingId, BookingStatus newStatus);
    }

}


using Microsoft.AspNetCore.SignalR;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.Enums;
using PRN222.RoomBooking.Repositories.UnitOfWork;
using PRN222.RoomBooking.Services.Hubs;
using System.Linq.Expressions;

namespace PRN222.RoomBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> _hubContext;

        public BookingService(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<bool> CreateBookingAsync(string userCode, DateOnly bookingDate, List<int> roomSlotIds, string purpose)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Check if any of the selected RoomSlots are already booked for the given date
                var bookedRoomSlotIds = await GetBookedRoomSlotIdsAsync(roomSlotIds.First(), bookingDate);
                var alreadyBookedSlots = roomSlotIds.Intersect(bookedRoomSlotIds).ToList();

                if (alreadyBookedSlots.Any())
                {
                    return false; // Some slots are already booked
                }

                // Create a new booking
                var booking = new Booking
                {
                    UserCode = userCode,
                    BookingDate = bookingDate,
                    CreatedDate = DateTime.Now,
                    Purpose = purpose,
                    BookingStatus = BookingStatus.Pending,
                    RoomSlots = new List<RoomSlot>()
                };

                // Add the selected RoomSlots to the booking
                foreach (var roomSlotId in roomSlotIds)
                {
                    var roomSlot = await _unitOfWork.RoomSlotRepository().GetByIdAsync(roomSlotId);
                    if (roomSlot != null && roomSlot.Status == RoomSlotStatus.Available)
                    {
                        booking.RoomSlots.Add(roomSlot);
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return false; // Slot is not available
                    }
                }

                await _unitOfWork.BookingRepository().AddAsync(booking);
                await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                await _hubContext.Clients.User(userCode)
                    .SendAsync("ReceiveNotification", "Your booking has been successfully submitted and is awaiting approval!");
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public async Task<List<int>> GetBookedRoomSlotIdsAsync(int roomId, DateOnly bookingDate)
        {
            // Get all bookings for the given room and date
            var bookings = await _unitOfWork.BookingRepository().GetAsync(
                b => b.BookingDate == bookingDate && b.RoomSlots.Any(rs => rs.RoomId == roomId) && b.BookingStatus != BookingStatus.Cancelled,
                includes: new Expression<Func<Booking, object>>[] { b => b.RoomSlots }
            );

            // Extract the RoomSlotIds that are already booked
            var bookedRoomSlotIds = bookings.SelectMany(b => b.RoomSlots).Select(rs => rs.RoomSlotId).Distinct().ToList();
            return bookedRoomSlotIds;
        }
        public async Task<bool> CancelBookingAsync(int bookingId, string userCode)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Fetch the Booking with its associated RoomSlots
                var booking = await _unitOfWork.BookingRepository().GetByIdAsync(
                    bookingId,
                    includes: new Expression<Func<Booking, object>>[] { b => b.RoomSlots }
                );

                // Validate the Booking and user
                if (booking == null || booking.UserCode != userCode || booking.BookingStatus == BookingStatus.Cancelled)
                {
                    return false;
                }

                // Update the Booking status to Cancelled
                booking.BookingStatus = BookingStatus.Cancelled;

                // Revert RoomSlot status to Available
                var roomIds = booking.RoomSlots.Select(rs => rs.RoomId).Distinct().ToList();
                foreach (var roomSlot in booking.RoomSlots)
                {
                    roomSlot.Status = RoomSlotStatus.Available;
                }

                // Update the Booking
                await _unitOfWork.BookingRepository().UpdateAsync(booking);

                // Update Room Status for each unique Room
                foreach (var roomId in roomIds)
                {
                    var room = await _unitOfWork.RoomRepository().GetByIdAsync(roomId);
                    var allSlots = await _unitOfWork.RoomSlotRepository().GetAsync(rs => rs.RoomId == roomId);
                    if (allSlots.Any(rs => rs.Status == RoomSlotStatus.Available))
                    {
                        room.Status = RoomStatus.Available;
                        await _unitOfWork.RoomRepository().UpdateAsync(room);
                    }
                }

                await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Booking>> GetUserBookingsAsync(string userCode, BookingStatus? statusFilter = null)
        {
            // Validate input
            if (string.IsNullOrEmpty(userCode))
            {
                return new List<Booking>();
            }

            // Build the filter expression
            Expression<Func<Booking, bool>> filter = b => b.UserCode == userCode;
            if (statusFilter.HasValue)
            {
                filter = b => b.UserCode == userCode && b.BookingStatus == statusFilter.Value;
            }

            // Fetch the bookings with associated RoomSlots and Rooms
            var bookingsQuery = _unitOfWork.BookingRepository().GetAsync(
                filter,
                includes: new Expression<Func<Booking, object>>[] { b => b.RoomSlots } // Include RoomSlots only
            );

            var bookings = await bookingsQuery;

            // Now, manually include the Room for each RoomSlot
            foreach (var booking in bookings)
            {
                foreach (var roomSlot in booking.RoomSlots)
                {
                    // Explicitly load the Room for each RoomSlot
                    await _unitOfWork.RoomSlotRepository()
                        .GetByIdAsync(roomSlot.RoomSlotId, rs => rs.Room);
                }
            }

            return bookings.OrderByDescending(b => b.CreatedDate).ToList();
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _unitOfWork.BookingRepository().GetByIdAsync(
                bookingId,
                includes: new Expression<Func<Booking, object>>[] { b => b.RoomSlots, b => b.RoomSlots.Select(rs => rs.Room) }
            );
        }

        public async Task<(List<Booking> bookings, int totalItems)> GetUserBookingsPaginatedAsync(string userCode, BookingStatus? statusFilter, int pageNumber, int pageSize)
        {
            // Validate input
            if (string.IsNullOrEmpty(userCode))
            {
                return (new List<Booking>(), 0);
            }

            // Build the filter expression
            Expression<Func<Booking, bool>> filter = b => b.UserCode == userCode;
            if (statusFilter.HasValue)
            {
                filter = b => b.UserCode == userCode && b.BookingStatus == statusFilter.Value;
            }

            // Get total count of bookings
            var totalItems = await _unitOfWork.BookingRepository().CountAsync(new List<Expression<Func<Booking, bool>>> { filter });

            // Fetch paginated bookings with associated RoomSlots and Rooms
            var bookings = await _unitOfWork.BookingRepository().GetAllAsync(
                filter: new List<Expression<Func<Booking, bool>>> { filter },
                orderBy: b => b.CreatedDate,
                ascending: false, // For descending order
                page: pageNumber,
                pageSize: pageSize,
                includes: new Expression<Func<Booking, object>>[] { b => b.RoomSlots }
            );

            // Manually include the Room for each RoomSlot
            foreach (var booking in bookings)
            {
                foreach (var roomSlot in booking.RoomSlots)
                {
                    await _unitOfWork.RoomSlotRepository()
                        .GetByIdAsync(roomSlot.RoomSlotId, rs => rs.Room);
                }
            }

            return (bookings, totalItems);
        }

        public async Task<(List<Booking> bookings, int totalItems)> GetPendingBookingsForCampusAsync(int campusId, int pageNumber, int pageSize)
        {
            // Build the filter expression to get Pending bookings for the campus
            Expression<Func<Booking, bool>> filter = b => b.BookingStatus == BookingStatus.Pending &&
                                                         b.RoomSlots.Any(rs => rs.Room.CampusId == campusId);

            // Get total count of bookings
            var totalItems = await _unitOfWork.BookingRepository().CountAsync(new List<Expression<Func<Booking, bool>>> { filter });

            // Fetch paginated bookings with associated RoomSlots
            var bookings = await _unitOfWork.BookingRepository().GetAllAsync(
                filter: new List<Expression<Func<Booking, bool>>> { filter },
                orderBy: b => b.CreatedDate,
                ascending: false,
                page: pageNumber,
                pageSize: pageSize,
                includes: new Expression<Func<Booking, object>>[] { b => b.RoomSlots } // Include only RoomSlots
            );

            // Manually include the Room for each RoomSlot
            foreach (var booking in bookings)
            {
                foreach (var roomSlot in booking.RoomSlots)
                {
                    // Explicitly load the Room for each RoomSlot
                    await _unitOfWork.RoomSlotRepository()
                        .GetByIdAsync(roomSlot.RoomSlotId, rs => rs.Room);
                }
            }

            return (bookings, totalItems);
        }

        public async Task<bool> UpdateBookingStatusAsync(int bookingId, BookingStatus newStatus)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Fetch the Booking with its associated RoomSlots
                var booking = await _unitOfWork.BookingRepository().GetByIdAsync(
                    bookingId,
                    includes: new Expression<Func<Booking, object>>[] { b => b.RoomSlots }
                );

                // Validate the Booking
                if (booking == null || booking.BookingStatus != BookingStatus.Pending)
                {
                    return false;
                }

                // Update the Booking status
                booking.BookingStatus = newStatus;

                // If the status is Cancelled, update RoomSlot status to Available
                if (newStatus == BookingStatus.Cancelled)
                {
                    var roomIds = booking.RoomSlots.Select(rs => rs.RoomId).Distinct().ToList();
                    foreach (var roomSlot in booking.RoomSlots)
                    {
                        roomSlot.Status = RoomSlotStatus.Available;
                    }

                    // Update Room Status for each unique Room
                    foreach (var roomId in roomIds)
                    {
                        var room = await _unitOfWork.RoomRepository().GetByIdAsync(roomId);
                        var allSlots = await _unitOfWork.RoomSlotRepository().GetAsync(rs => rs.RoomId == roomId);
                        if (allSlots.Any(rs => rs.Status == RoomSlotStatus.Available))
                        {
                            room.Status = RoomStatus.Available;
                            await _unitOfWork.RoomRepository().UpdateAsync(room);
                        }
                    }
                }
                else if (newStatus == BookingStatus.Booked)
                {
                    // If the status is Booked, update RoomSlot status to Booked
                    var roomIds = booking.RoomSlots.Select(rs => rs.RoomId).Distinct().ToList();
                    foreach (var roomSlot in booking.RoomSlots)
                    {
                        roomSlot.Status = RoomSlotStatus.Booked;
                    }

                    // Update Room Status for each unique Room
                    foreach (var roomId in roomIds)
                    {
                        var room = await _unitOfWork.RoomRepository().GetByIdAsync(roomId);
                        var allSlots = await _unitOfWork.RoomSlotRepository().GetAsync(rs => rs.RoomId == roomId);
                        if (!allSlots.Any(rs => rs.Status == RoomSlotStatus.Available))
                        {
                            room.Status = RoomStatus.Booked;
                            await _unitOfWork.RoomRepository().UpdateAsync(room);
                        }
                    }
                }

                // Update the Booking
                await _unitOfWork.BookingRepository().UpdateAsync(booking);
                await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                string message = newStatus == BookingStatus.Booked
                    ? "Your reservation has been accepted!"
                    : "Your reservation has been cancelled.";
                await _hubContext.Clients.User(booking.UserCode)
                    .SendAsync("ReceiveNotification", message);

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}

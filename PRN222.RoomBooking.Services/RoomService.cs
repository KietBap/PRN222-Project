using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.UnitOfWork;
using System.Linq.Expressions;
using PRN222.RoomBooking.Repositories.Enums;

namespace PRN222.RoomBooking.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public RoomService(IUnitOfWork unitOfWork, IBookingService bookingService, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _bookingService = bookingService;
            _userService = userService;
        }

        public async Task<List<RoomSlot>> GetAvailableRoomSlotsAsync(int roomId, DateOnly bookingDate)
        {
            // Lấy tất cả RoomSlots cho phòng
            var roomSlots = await _unitOfWork.RoomSlotRepository().GetAsync(
                rs => rs.RoomId == roomId,
                includes: new Expression<Func<RoomSlot, object>>[] { rs => rs.Room }
            );

            // Lấy danh sách RoomSlotIds đã được booked cho ngày cụ thể
            var bookedRoomSlotIds = await _bookingService.GetBookedRoomSlotIdsAsync(roomId, bookingDate);

            // Lọc ra các slot chưa được booked cho ngày này
            var availableSlots = roomSlots
                .Where(rs => !bookedRoomSlotIds.Contains(rs.RoomSlotId))
                .ToList();

            return availableSlots;
        }

        //For Manager
        public async Task<(List<RoomSlot> roomSlots, int totalItems)> GetBookedRoomSlotsForCampusAsync(int campusId, int pageNumber, int pageSize)
        {
            // Build the filter expression to get Booked room slots for the campus
            Expression<Func<RoomSlot, bool>> filter = rs => rs.Status == RoomSlotStatus.Booked && rs.Room.CampusId == campusId;

            // Get total count of room slots
            var totalItems = await _unitOfWork.RoomSlotRepository().CountAsync(new List<Expression<Func<RoomSlot, bool>>> { filter });

            // Fetch paginated room slots with associated Room and Campus
            var roomSlots = await _unitOfWork.RoomSlotRepository().GetAllAsync(
                filter: new List<Expression<Func<RoomSlot, bool>>> { filter },
                orderBy: rs => rs.RoomId, // Order by RoomId for consistency
                ascending: true,
                page: pageNumber,
                pageSize: pageSize,
                includes: new Expression<Func<RoomSlot, object>>[] { rs => rs.Room, rs => rs.Room.Campus }
            );

            return (roomSlots, totalItems);
        }

        public async Task<List<RoomSlot>> GetBookedRoomSlotsForRoomAsync(int roomId)
        {
            // Build the filter expression to get Booked room slots for the room
            Expression<Func<RoomSlot, bool>> filter = rs => rs.RoomId == roomId && rs.Status == RoomSlotStatus.Booked;

            // Fetch room slots with associated Room and Bookings
            var roomSlots = await _unitOfWork.RoomSlotRepository().GetAllAsync(
                filter: new List<Expression<Func<RoomSlot, bool>>> { filter },
                orderBy: rs => rs.SlotNumber,
                ascending: true,
                includes: new Expression<Func<RoomSlot, object>>[]
                {
                    rs => rs.Room,
                    rs => rs.Bookings // Include the Bookings collection
                }
            );

            // Fetch the User for each Booking
            foreach (var roomSlot in roomSlots)
            {
                foreach (var booking in roomSlot.Bookings)
                {
                    // Fetch the User using the UserCode
                    var user = await _userService.GetUserByCode(booking.UserCode);
                    // Add the User to the Booking (we'll use a dictionary in the view to access this)
                    booking.UserCode = user?.FullName ?? "Unknown User"; // Temporarily store the FullName in UserCode for display
                }
            }

            return roomSlots;
        }

        public async Task<(List<Room>, int totalItems)> GetRoom(string? roomName, string? campus, string? sortBy, int page, int pageSize, int userCampusId)
        {
            var filters = new List<Expression<Func<Room, bool>>>();

            // Always filter by the user's CampusId
            filters.Add(p => p.CampusId == userCampusId);

            if (!string.IsNullOrEmpty(roomName))
            {
                filters.Add(p => p.RoomName != null && p.RoomName.ToLower().Contains(roomName.ToLower()));
            }

            if (!string.IsNullOrEmpty(campus))
            {
                filters.Add(p => p.Campus != null && p.Campus.CampusName != null && p.Campus.CampusName.ToLower().Contains(campus.ToLower()));
            }

            var totalItems = await _unitOfWork.RoomRepository().CountAsync(filters);

            var rooms = await _unitOfWork.RoomRepository().GetAllAsync(
                filter: filters,
                orderBy: null,
                ascending: true,
                page: page,
                pageSize: pageSize,
                includes: new Expression<Func<Room, object>>[] { r => r.Campus }
            );

            return (rooms, totalItems);
        }

        public async Task<Room> GetRoomById(int id)
        {
            var room = await _unitOfWork.RoomRepository().GetByIdAsync(
                id: id,
                includes: new Expression<Func<Room, object>>[] {
                    r => r.Campus,
                    r => r.RoomSlots
                }
            );

            if (room != null && room.RoomSlots != null)
            {
                room.RoomSlots = room.RoomSlots
                    .OrderBy(rs => rs.RoomId)
                    .ThenBy(rs => rs.StartTime)
                    .ToList();
            }

            return room;
        }
        public async Task CreateRoomAsync(Room room)
        {
            try
            {
                Console.WriteLine($"Creating room: Name={room.RoomName}, Capacity={room.Capacity}, Status={room.Status}, CampusId={room.CampusId}");
                await _unitOfWork.RoomRepository().AddAsync(room);
                await _unitOfWork.SaveAsync();
                Console.WriteLine($"Room created with ID: {room.RoomId}");

                // Auto-generate RoomSlots for the new room
                await GenerateRoomSlotsForRoom(room.RoomId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateRoomAsync: {ex.Message}");
                throw;
            }
        }

        private async Task GenerateRoomSlotsForRoom(int roomId)
        {
            try
            {
                // Generate 8 slots for a 24-hour day (each slot is 3 hours)
                for (int slotNumber = 1; slotNumber <= 8; slotNumber++)
                {
                    var startHour = (slotNumber - 1) * 3; // 0, 3, 6, ..., 21
                    var endHour = startHour + 3; // 3, 6, 9, ..., 24

                    var roomSlot = new RoomSlot
                    {
                        RoomId = roomId,
                        SlotNumber = slotNumber,
                        StartTime = new TimeOnly(startHour, 0), // e.g., 00:00, 03:00, ...
                        EndTime = new TimeOnly(endHour == 24 ? 0 : endHour, 0), // 24:00 becomes 00:00
                        Status = RoomSlotStatus.Available // Default status
                    };

                    await _unitOfWork.RoomSlotRepository().AddAsync(roomSlot);
                }

                await _unitOfWork.SaveAsync();
                Console.WriteLine($"Generated 8 RoomSlots for RoomId: {roomId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating RoomSlots for RoomId {roomId}: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteRoomAsync(int roomId)
        {
            try
            {
                // Get the room with its room slots
                var room = await _unitOfWork.RoomRepository()
                    .GetByIdAsync(roomId, r => r.RoomSlots);
                if (room == null)
                {
                    throw new Exception("Room not found");
                }

                // Get all room slots for this room with their bookings
                var roomSlots = await _unitOfWork.RoomSlotRepository()
                    .GetAsync(rs => rs.RoomId == roomId, rs => rs.Bookings);

                Console.WriteLine($"Found {roomSlots.Count} room slots for RoomId {roomId}");

                // Check if any room slots have bookings
                if (roomSlots.Any(rs => rs.Bookings != null && rs.Bookings.Any()))
                {
                    Console.WriteLine("Room has existing bookings");
                    throw new Exception("Cannot delete room with existing bookings");
                }

                // Delete room slots first
                foreach (var roomSlot in roomSlots)
                {
                    await _unitOfWork.RoomSlotRepository().DeleteAsync(roomSlot);
                }

                // Delete the room
                await _unitOfWork.RoomRepository().DeleteAsync(room);
                await _unitOfWork.SaveAsync();

                Console.WriteLine($"Room {roomId} deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting room {roomId}: {ex.Message}");
                throw;
            }
        }
        public async Task UpdateRoomAsync(Room room)
        {
            try
            {
                // Lấy thông tin phòng hiện tại từ database
                var existingRoom = await _unitOfWork.RoomRepository().GetByIdAsync(room.RoomId);
                if (existingRoom == null)
                {
                    throw new Exception("Room not found");
                }

                // Cập nhật thông tin phòng
                existingRoom.RoomName = room.RoomName;
                existingRoom.Capacity = room.Capacity;
                existingRoom.Status = room.Status;
                existingRoom.CampusId = room.CampusId;

                // Lưu thay đổi
                await _unitOfWork.RoomRepository().UpdateAsync(existingRoom);
                await _unitOfWork.SaveAsync();

                Console.WriteLine($"Room {room.RoomId} updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating room {room.RoomId}: {ex.Message}");
                throw;
            }
        }

        public async Task<RoomSlot> GetRoomSlotById(int roomSlotId)
        {
            return await _unitOfWork.RoomSlotRepository().GetByIdAsync(
                id: roomSlotId,
                includes: new Expression<Func<RoomSlot, object>>[] { rs => rs.Room }
            );
        }

        public async Task CreateRoomSlotAsync(RoomSlot roomSlot)
        {
            try
            {
                await _unitOfWork.RoomSlotRepository().AddAsync(roomSlot);
                await _unitOfWork.SaveAsync();
                Console.WriteLine($"RoomSlot created with ID: {roomSlot.RoomSlotId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateRoomSlotAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateRoomSlotAsync(RoomSlot roomSlot)
        {
            try
            {
                var existingRoomSlot = await _unitOfWork.RoomSlotRepository().GetByIdAsync(roomSlot.RoomSlotId);
                if (existingRoomSlot == null)
                {
                    throw new Exception("RoomSlot not found");
                }

                existingRoomSlot.SlotNumber = roomSlot.SlotNumber;
                existingRoomSlot.StartTime = roomSlot.StartTime;
                existingRoomSlot.EndTime = roomSlot.EndTime;
                existingRoomSlot.Status = roomSlot.Status;

                await _unitOfWork.RoomSlotRepository().UpdateAsync(existingRoomSlot);
                await _unitOfWork.SaveAsync();
                Console.WriteLine($"RoomSlot {roomSlot.RoomSlotId} updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating RoomSlot {roomSlot.RoomSlotId}: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteRoomSlotAsync(int roomSlotId)
        {
            try
            {
                var roomSlot = await _unitOfWork.RoomSlotRepository().GetByIdAsync(roomSlotId, rs => rs.Bookings);
                if (roomSlot == null)
                {
                    throw new Exception("RoomSlot not found");
                }

                if (roomSlot.Bookings != null && roomSlot.Bookings.Any())
                {
                    throw new Exception("Cannot delete RoomSlot with existing bookings");
                }

                await _unitOfWork.RoomSlotRepository().DeleteAsync(roomSlot);
                await _unitOfWork.SaveAsync();
                Console.WriteLine($"RoomSlot {roomSlotId} deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting RoomSlot {roomSlotId}: {ex.Message}");
                throw;
            }
        }
    }
}
using PRN222.RoomBooking.Repositories.Data;

namespace PRN222.RoomBooking.Services
{
    public interface IRoomService
    {
        Task<(List<Room>, int totalItems)> GetRoom(string? roomName, string? campus,string? sortBy, int page, int pageSize, int userCampusId);

        Task<Room> GetRoomById(int roomId);

        Task<List<RoomSlot>> GetAvailableRoomSlotsAsync(int roomId, DateOnly bookingDate);

        //For Manager
        Task<(List<RoomSlot> roomSlots, int totalItems)> GetBookedRoomSlotsForCampusAsync(int campusId, int pageNumber, int pageSize);
        Task<List<RoomSlot>> GetBookedRoomSlotsForRoomAsync(int roomId);
        Task CreateRoomAsync(Room room);
        Task DeleteRoomAsync(int roomId);
        Task UpdateRoomAsync(Room room);

        // New methods for RoomSlot management
        Task<RoomSlot> GetRoomSlotById(int roomSlotId);
        Task CreateRoomSlotAsync(RoomSlot roomSlot);
        Task UpdateRoomSlotAsync(RoomSlot roomSlot);
        Task DeleteRoomSlotAsync(int roomSlotId);
    }
}

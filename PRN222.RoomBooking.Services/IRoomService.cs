using PRN222.RoomBooking.Repositories.Data;

namespace PRN222.RoomBooking.Services
{
    public interface IRoomService
    {
        Task<(List<Room>, int totalItems)> GetRoom(string? roomName, string? campus,string? sortBy, int page, int pageSize, int userCampusId);

        Task<Room> GetRoomById(int roomId);

        Task<List<RoomSlot>> GetAvailableRoomSlotsAsync(int roomId, DateOnly bookingDate);
    }
}

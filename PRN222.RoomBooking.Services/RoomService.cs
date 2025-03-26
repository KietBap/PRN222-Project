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

        public RoomService(IUnitOfWork unitOfWork, IBookingService bookingService)
        {
            _unitOfWork = unitOfWork;
            _bookingService = bookingService;
        }

        public async Task<List<RoomSlot>> GetAvailableRoomSlotsAsync(int roomId, DateOnly bookingDate)
        {
            // Get all RoomSlots for the room
            var roomSlots = await _unitOfWork.RoomSlotRepository().GetAsync(
                rs => rs.RoomId == roomId && rs.Status == RoomSlotStatus.Available,
                includes: new Expression<Func<RoomSlot, object>>[] { rs => rs.Room }
            );

            // Get booked RoomSlotIds for the given date
            var bookedRoomSlotIds = await _bookingService.GetBookedRoomSlotIdsAsync(roomId, bookingDate);

            // Filter out booked slots
            return roomSlots.Where(rs => !bookedRoomSlotIds.Contains(rs.RoomSlotId)).ToList();
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
    }
}
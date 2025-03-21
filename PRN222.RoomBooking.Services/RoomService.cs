using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(List<Room>, int totalItems)> GetRoom(string? roomName, string? campus,string? sortBy, int page, int pageSize)
        {
            var filters = new List<Expression<Func<Room, bool>>>
            {
                p => string.IsNullOrEmpty(roomName) || p.RoomName.Contains(roomName),
                p => string.IsNullOrEmpty(roomName) || p.Campus.CampusName.Contains(roomName),
            };
            var totalItems = await _unitOfWork.RoomRepository().CountAsync(filters);
            var rooms = await _unitOfWork.RoomRepository().GetAllAsync(filters, null, true, page,pageSize);
            return (rooms, totalItems);
        }
    }
}

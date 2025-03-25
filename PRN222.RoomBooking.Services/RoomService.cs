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
        public async Task<(List<Room>, int totalItems)> GetRoom(string? roomName, string? campus, string? sortBy, int page, int pageSize)
        {
            var filters = new List<Expression<Func<Room, bool>>>();

            // Tìm kiếm theo RoomName, xử lý null và không phân biệt chữ hoa/thường
            if (!string.IsNullOrEmpty(roomName))
            {
                filters.Add(p => p.RoomName != null && p.RoomName.ToLower().Contains(roomName.ToLower()));
            }

            // Tìm kiếm theo CampusName, xử lý null và không phân biệt chữ hoa/thường
            if (!string.IsNullOrEmpty(campus))
            {
                filters.Add(p => p.Campus != null && p.Campus.CampusName != null && p.Campus.CampusName.ToLower().Contains(campus.ToLower()));
            }

            // Đếm tổng số phòng
            var totalItems = await _unitOfWork.RoomRepository().CountAsync(filters);

            // Lấy danh sách phòng và bao gồm dữ liệu Campus
            var rooms = await _unitOfWork.RoomRepository().GetAllAsync(
                filter: filters,
                orderBy: null,
                ascending: true,
                page: page,
                pageSize: pageSize,
                includes: new Expression<Func<Room, object>>[] { r => r.Campus } // Bao gồm Campus
            );

            return (rooms, totalItems);
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _unitOfWork.RoomRepository().GetByIdAsync(
                id: id,
                includes: new Expression<Func<Room, object>>[] { r => r.Campus }
            );
        }
    }
}

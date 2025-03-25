using PRN222.RoomBooking.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.Services
{
    public interface IRoomService
    {
        Task<(List<Room>, int totalItems)> GetRoom(string? roomName, string? campus,string? sortBy, int page, int pageSize);

        Task<Room> GetRoomById(int id);
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using PRN222.RoomBooking.Repositories.Data;

namespace PRN222.RoomBooking.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        IGenericRepository<User> UserRepository();
        IGenericRepository<Room> RoomRepository();
        IGenericRepository<Campus> CampusRepository();
        IGenericRepository<Booking> BookingRepository();
        IGenericRepository<RoomSlot> RoomSlotRepository();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

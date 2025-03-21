using PRN222.RoomBooking.Repositories.Data;

namespace PRN222.RoomBooking.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        IGenericRepository<User> UserRepository();
        IGenericRepository<Room> RoomRepository();
        IGenericRepository<Campus> CampusRepository();
    }
}

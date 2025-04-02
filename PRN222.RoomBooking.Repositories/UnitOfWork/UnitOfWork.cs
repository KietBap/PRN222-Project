using Microsoft.EntityFrameworkCore.Storage;
using PRN222.RoomBooking.Repositories.Data;

namespace PRN222.RoomBooking.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FpturoomBookingDbContext _dbContext;

        public UnitOfWork(FpturoomBookingDbContext myStoreContext)
        {
            _dbContext = myStoreContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }
        public IGenericRepository<User> UserRepository()
        {
            return GetRepository<User>();
        }
        public IGenericRepository<Room> RoomRepository()
        {
            return GetRepository<Room>();
        }
        public IGenericRepository<Campus> CampusRepository()
        {
            return GetRepository<Campus>();
        }
        public IGenericRepository<Booking> BookingRepository() 
        {
            return GetRepository<Booking>();
        }

        public IGenericRepository<RoomSlot> RoomSlotRepository() 
        {
            return GetRepository<RoomSlot>();
        }

        public IGenericRepository<Department> DepartmentRepository()
        {
            return GetRepository<Department>();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

    }
}
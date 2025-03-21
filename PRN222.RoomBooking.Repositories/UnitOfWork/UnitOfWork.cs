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

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }
    }
}
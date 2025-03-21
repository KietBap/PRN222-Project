namespace PRN222.RoomBooking.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        IGenericRepository<T> GetRepository<T>() where T : class;
    }
}

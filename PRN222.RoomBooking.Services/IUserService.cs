using PRN222.RoomBooking.Repositories.Data;

namespace PRN222.RoomBooking.Services
{
    public interface IUserService
    {
        Task<User> Login(string email, string password);
    }
}


using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.UnitOfWork;

namespace PRN222.RoomBooking.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Login(string email, string password)
        {
            return await _unitOfWork.UserRepository().FindByConditionAsync(u => u.Email == email && u.Password == password);
        }
    }
}

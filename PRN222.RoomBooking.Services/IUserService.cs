using PRN222.RoomBooking.Repositories.Data;

namespace PRN222.RoomBooking.Services
{
    public interface IUserService
    {
        Task<User> GetUserByCode(string userCode);
        Task<User> Login(string email, string password);
        Task<(List<User> Users, int TotalCount)> GetUsers(int? campusId, int? departmentId, string? role, string? search, int page, int pageSize, string? sortColumn, bool sortAscending = true);
        Task<bool> CreateUser(User newUser);
        Task<bool> UpdateUser(User updatedUser);
        Task<bool> DeleteUser(string userCode);
        Task<User> GetLastUser();
    }
}

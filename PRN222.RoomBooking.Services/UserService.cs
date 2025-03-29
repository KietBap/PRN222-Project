
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.UnitOfWork;
using System.Linq.Expressions;

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
            return await _unitOfWork.UserRepository()
                .FindByConditionAsync(
                    u => u.Email == email && u.Password == password
                );
        }

        public async Task<User> GetUserByCode(string userCode)
        {
            return await _unitOfWork.UserRepository()
                .FindByConditionAsync(
                    u => u.UserCode == userCode,
                    u => u.Campus
                );
        }

        public async Task<(List<User> Users, int TotalCount)> GetUsers(int? campusId, int? departmentId, string? role, string? search, int page, int pageSize, string? sortColumn, bool sortAscending = true)
        {
            if (page < 1 || pageSize < 1)
                throw new ArgumentException("Page and pageSize must be greater than 0");

            var filters = new List<Expression<Func<User, bool>>>();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLowerInvariant();
                filters.Add(u => (u.FullName != null && u.FullName.ToLower().Contains(search)) ||
                             (u.Email != null && u.Email.ToLower().Contains(search)));
            }
            if (campusId.HasValue)
            {
                filters.Add(u => u.CampusId == campusId.Value);
            }
            if (departmentId.HasValue)
            {
                filters.Add(u => u.DepartmentId == departmentId.Value);
            }
            if (!string.IsNullOrEmpty(role))
            {
                filters.Add(u => u.Role == role);
            }

            Expression<Func<User, object>> orderBy = sortColumn switch
            {
                "FullName" => u => u.FullName ?? "",
                _ => u => u.UserCode 
            };

            var includes = new Expression<Func<User, object>>[] { u => u.Campus!, u => u.Department! };

            var users = await _unitOfWork.UserRepository().GetAllAsync(
                filter: filters,
                orderBy: orderBy,
                ascending: sortAscending,
                page: page,
                pageSize: pageSize,
                includes: includes
            );

            var totalCount = await _unitOfWork.UserRepository().CountAsync(filters);
            return (users ?? new List<User>(), totalCount);
        }

        public async Task<bool> CreateUser(User newUser)
        {
            var existingUser = await _unitOfWork.UserRepository().FindByConditionAsync(
                u => u.Email == newUser.Email
            );

            if (existingUser != null)
            {
                return false;
            }

            await _unitOfWork.UserRepository().AddAsync(newUser);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateUser(User updatedUser)
        {
            var existingUser = await _unitOfWork.UserRepository().GetByIdAsync(updatedUser.UserCode);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.FullName = updatedUser.FullName;
            existingUser.Email = updatedUser.Email;
            existingUser.CampusId = updatedUser.CampusId;
            existingUser.DepartmentId = updatedUser.DepartmentId;
            existingUser.Role = updatedUser.Role;

            await _unitOfWork.UserRepository().UpdateAsync(existingUser);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteUser(string userCode)
        {
            var user = await _unitOfWork.UserRepository().GetByIdAsync(userCode);

            if (user == null)
            {
                return false;
            }

            await _unitOfWork.UserRepository().DeleteAsync(user);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<User?> GetLastUser()
        {
            var query = @"  SELECT TOP 1 * FROM Users 
                            WHERE UserCode LIKE 'U%' 
                            ORDER BY CAST(SUBSTRING(UserCode, 2, LEN(UserCode) - 1) AS INT) DESC";
            return await _unitOfWork.UserRepository().ExecuteQuerySingleAsync(query) ?? null;
        }

    }
}

using System.Linq.Expressions;

namespace PRN222.RoomBooking.Repositories.UnitOfWork
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id, params Expression<Func<T, object>>[] includes);
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync(
            IEnumerable<Expression<Func<T, bool>>> filter = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            int? page = null,
            int? pageSize = null,
            params Expression<Func<T, object>>[] includes);
        Task<int> CountAsync(IEnumerable<Expression<Func<T, bool>>> filter = null);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T?> ExecuteQuerySingleAsync(string query, params object[] parameters);

    }
}

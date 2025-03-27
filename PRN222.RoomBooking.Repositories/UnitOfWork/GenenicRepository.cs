using Microsoft.EntityFrameworkCore;
using PRN222.RoomBooking.Repositories.Data;
using System.Linq.Expressions;

namespace PRN222.RoomBooking.Repositories.UnitOfWork
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FpturoomBookingDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(FpturoomBookingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(IEnumerable<Expression<Func<T, bool>>> filter = null,
                                               Expression<Func<T, object>> orderBy = null,
                                               bool ascending = true,
                                               int? page = null,
                                               int? pageSize = null,
                                               params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                foreach (var additionalFilter in filter)
                {
                    query = query.Where(additionalFilter);
                }
            }

            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            var entityType = _context.Model.FindEntityType(typeof(T));
            var keyName = entityType?.FindPrimaryKey()?.Properties.FirstOrDefault()?.Name;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<object>(e, keyName).Equals(id));
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> CountAsync(IEnumerable<Expression<Func<T, bool>>> filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                foreach (var additionalFilter in filter)
                {
                    query = query.Where(additionalFilter);
                }
            }

            return await query.CountAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T?> ExecuteQuerySingleAsync(string query, params object[] parameters)
        {
            return await _context.Set<T>()
                .FromSqlRaw(query, parameters)
                .AsNoTracking() 
                .FirstOrDefaultAsync();
        }

    }

}

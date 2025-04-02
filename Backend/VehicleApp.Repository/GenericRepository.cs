using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<PagedList<T>> FindAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            var query = _dbSet.Where(predicate);
            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, pageNumber, pageSize, totalCount);
        }

        public async Task<PagedList<T>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsQueryable();
            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, pageNumber, pageSize, totalCount);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

    }
}

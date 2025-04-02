using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Repository.Common;
using System.Linq.Dynamic.Core;

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

        public async Task<PagedList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Paging paging, Sorting sorting)
        {
            var query = _dbSet.Where(predicate);

            if (!string.IsNullOrEmpty(sorting.SortBy))
            {
                string orderDirection = sorting.SortOrder.ToLower() == "desc" ? "descending" : "ascending";
                query = query.OrderBy($"{sorting.SortBy} {orderDirection}");
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new PagedList<T>(items, paging.PageNumber, paging.PageSize, totalCount);
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

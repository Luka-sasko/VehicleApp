using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;

namespace VehicleApp.Repository.Common
{
    public interface IGenericRepository<T> where T : class
    {


        Task<PagedList<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<PagedList<T>> FindAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}

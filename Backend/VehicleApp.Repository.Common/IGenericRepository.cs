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


        Task<PagedList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Paging paging,Sorting sorting);
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}

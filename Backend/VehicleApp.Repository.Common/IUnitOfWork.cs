using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();

        IGenericRepository<T> GetRepository<T>() where T : class;
    }
}



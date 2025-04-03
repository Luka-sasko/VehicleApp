using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Model;

namespace VehicleApp.Service.Common
{
    public interface IVehicleModelService
    {
        Task<PagedList<VehicleModelView>> GetAllAsync(Expression<Func<VehicleModel, bool>> predicate, Paging paging, Sorting sorting);

        Task<VehicleModelView> GetVehicleModelByIdAsync(Guid id);
        Task AddVehicleModelAsync(VehicleModel vehicleModel);
        Task UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task DeleteVehicleModelAsync(Guid id);
    }
}


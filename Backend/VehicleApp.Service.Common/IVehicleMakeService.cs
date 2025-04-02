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
    

    public interface IVehicleMakeService
    {
        Task<PagedList<VehicleMake>> GetAllVehicleMakesAsync(int pageNumber, int pageSize);
        Task<PagedList<VehicleMake>> SearchAsync(Expression<Func<VehicleMake, bool>> predicate, int pageNumber, int pageSize);

        Task<VehicleMake> GetVehicleMakeByIdAsync(Guid id);
        Task AddVehicleMakeAsync(VehicleMake vehicleMake);
        Task UpdateVehicleMakeAsync(VehicleMake vehicleMake);
        Task DeleteVehicleMakeAsync(Guid id);
    }

}

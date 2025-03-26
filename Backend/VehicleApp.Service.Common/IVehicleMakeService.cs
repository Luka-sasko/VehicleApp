using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model;

namespace VehicleApp.Service.Common
{
    public interface IVehicleMakeService
    {
        Task<IEnumerable<VehicleMake>> GetAllAsync();
        Task<VehicleMake> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(VehicleMake vehicleMake);
        Task<bool> PostAsync(VehicleMake vehicleMake);
    }
}

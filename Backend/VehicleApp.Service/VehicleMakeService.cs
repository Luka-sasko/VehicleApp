using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Repository.Common;
using VehicleApp.Service.Common;


namespace VehicleApp.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IVehicleMakeRepository _vehicleMakeRepository;

        public VehicleMakeService (IVehicleMakeRepository vehicleMakeRepository)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _vehicleMakeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<VehicleMake>> GetAllAsync()
        {
            return await _vehicleMakeRepository.GetAllAsync();
        }

        public async Task<VehicleMake> GetByIdAsync(Guid id)
        {
            return await _vehicleMakeRepository.GetByIdAsync(id);
        }

        public async Task<bool> PostAsync(VehicleMake vehicleMake)
        {
            vehicleMake.Id = Guid.NewGuid();
            return await _vehicleMakeRepository.PostAsync(vehicleMake);
        }

        public async Task<bool> UpdateAsync(VehicleMake vehicleMake)
        {
            return await _vehicleMakeRepository.UpdateAsync(vehicleMake);
        }
    }
}

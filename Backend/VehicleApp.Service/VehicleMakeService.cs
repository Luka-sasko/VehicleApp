using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Repository.Common;
using VehicleApp.Service.Common;


namespace VehicleApp.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleMakeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<VehicleMake>> GetAllVehicleMakesAsync(int pageNumber, int pageSize)
        {
            return await _unitOfWork.GetRepository<VehicleMake>().GetAllAsync(pageNumber,pageSize);
        }

        public async Task<PagedList<VehicleMake>> SearchAsync(Expression<Func<VehicleMake, bool>> predicate, int pageNumber, int pageSize)
        {

            return await _unitOfWork.GetRepository<VehicleMake>().FindAsync(predicate, pageNumber, pageSize);
        }

        public async Task<VehicleMake> GetVehicleMakeByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<VehicleMake>().GetByIdAsync(id);
        }

        public async Task AddVehicleMakeAsync(VehicleMake vehicleMake)
        {
            await _unitOfWork.GetRepository<VehicleMake>().AddAsync(vehicleMake);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            await _unitOfWork.GetRepository<VehicleMake>().Update(vehicleMake);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteVehicleMakeAsync(Guid id)
        {
            var repository = _unitOfWork.GetRepository<VehicleMake>();
            var entity = await repository.GetByIdAsync(id);
            if (entity != null)
            {
                await repository.Delete(entity);
                await _unitOfWork.CommitAsync();
            }
        }

    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;
using VehicleApp.Service.Common;


namespace VehicleApp.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleMakeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        

        public async Task<PagedList<VehicleMakeView>> GetAllAsync(Expression<Func<VehicleMake, bool>> predicate, Paging paging,Sorting sorting)
        {
            var vehiclesMake =  await _unitOfWork.GetRepository<VehicleMake>().GetAllAsync(predicate, paging, sorting);
            var vehicleMakeViews = _mapper.Map<PagedList<VehicleMakeView>>(vehiclesMake);
            return vehicleMakeViews;
        }

        public async Task<VehicleMakeView> GetVehicleMakeByIdAsync(Guid id)
        {
            var vehicleMake = await _unitOfWork.GetRepository<VehicleMake>().GetByIdAsync(id);
            var vehicleMakeView = _mapper.Map<VehicleMakeView>(vehicleMake);
            return vehicleMakeView;
            
        }

        public async Task<bool> AddVehicleMakeAsync(VehicleMake vehicleMake)
        {
            if (vehicleMake == null)
            {
                throw new ArgumentNullException(nameof(vehicleMake), "Vehicle model cannot be null");
            }
            var isAdded = await _unitOfWork.GetRepository<VehicleMake>().AddAsync(vehicleMake);
            await _unitOfWork.CommitAsync();
            return isAdded;
        }

        public async Task<bool> UpdateVehicleMakeAsync(VehicleMake vehicleMake)

        {
            if (vehicleMake == null)
            {
                throw new ArgumentNullException(nameof(vehicleMake), "Vehicle model cannot be null");
            }
            var isUpdated = await _unitOfWork.GetRepository<VehicleMake>().Update(vehicleMake);
            await _unitOfWork.CommitAsync();
            return isUpdated;
        }

        public async Task<bool> DeleteVehicleMakeAsync(Guid id)
        {
            var repository = _unitOfWork.GetRepository<VehicleMake>();
            var entity = await repository.GetByIdAsync(id);
            var isDeleted = false;
            if (entity != null)
            {
                isDeleted = await repository.Delete(entity);
                await _unitOfWork.CommitAsync();

            }
             return isDeleted;

        }

    }
}

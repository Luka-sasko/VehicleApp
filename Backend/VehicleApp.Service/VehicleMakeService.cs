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

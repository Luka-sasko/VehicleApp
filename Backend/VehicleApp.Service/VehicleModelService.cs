using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;
using VehicleApp.Service.Common;

namespace VehicleApp.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleModelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddVehicleModelAsync(VehicleModel vehicleModel)
        {
            await _unitOfWork.GetRepository<VehicleModel>().AddAsync(vehicleModel);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteVehicleModelAsync(Guid id)
        {
            var repository = _unitOfWork.GetRepository<VehicleModel>();
            var entity = await repository.GetByIdAsync(id);
            if (entity != null)
            {
                await repository.Delete(entity);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<PagedList<VehicleModelView>> GetAllAsync(Expression<Func<VehicleModel, bool>> predicate, Paging paging, Sorting sorting)
        {
            var vehiclesModel = await _unitOfWork.GetRepository<VehicleModel>().GetAllAsync(predicate, paging, sorting);
            var vehicleModelViews = _mapper.Map<PagedList<VehicleModelView>>(vehiclesModel);
            return vehicleModelViews;
        }

        public async Task<VehicleModelView> GetVehicleModelByIdAsync(Guid id)
        {
            var vehicleModel = await _unitOfWork.GetRepository<VehicleModel>().GetByIdAsync(id);
            var vehicleModelView = _mapper.Map<VehicleModelView>(vehicleModel);
            return vehicleModelView;
        }

        public async Task UpdateVehicleModelAsync(VehicleModel vehicleModel)
        {
            await _unitOfWork.GetRepository<VehicleModel>().Update(vehicleModel);
            await _unitOfWork.CommitAsync();
        }
    }
}

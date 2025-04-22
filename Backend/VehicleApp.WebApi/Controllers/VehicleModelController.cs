using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Service.Common;
using AutoMapper;
using VehicleApp.Common;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using VehicleApp.Service;

namespace VehicleApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IMapper _mapper;

        public VehicleModelController(IVehicleModelService VehicleModelService, IMapper mapper)
        {
            _vehicleModelService = VehicleModelService;
            _mapper = mapper;
        }

        // GET: api/vehiclmodel
        [HttpGet]
        public async Task<ActionResult<PagedList<VehicleModelView>>> GetAllAsync(
            string name = null,
            string abrv = null,
            Guid? makeId = null,
            string sortBy = "Name",
            string sortOrder = "asc",
            int pageNumber = 1,
            int pageSize = 10)
        {
            Paging paging = new Paging() { PageNumber = pageNumber, PageSize = pageSize };
            Sorting sorting = new Sorting() { SortBy = sortBy, SortOrder = sortOrder };
            try
            {
                Expression<Func<VehicleModel, bool>> predicate = x =>
                (string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name.ToLower())) &&
                (string.IsNullOrEmpty(abrv) || (x.Abrv != null && x.Abrv.ToLower().Contains(abrv.ToLower()))) &&
                (!makeId.HasValue || x.MakeId == makeId.Value);

                var VehicleModels = await _vehicleModelService.GetAllAsync(predicate, paging, sorting);

                if (VehicleModels.Items == null || !VehicleModels.Items.Any())
                {
                    return NotFound("No vehicle models found.");
                }

                return Ok(VehicleModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/vehiclemodel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModelView>> GetByIdAsync(Guid id)
        {
            try
            {
                var vehicleModel = await _vehicleModelService.GetVehicleModelByIdAsync(id);
                if (vehicleModel == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<VehicleModelView>(vehicleModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/vehiclemodel
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] VehicleModelView vehicleModelView)
        {
            if (vehicleModelView == null || vehicleModelView.Abrv == "" || vehicleModelView.Name == "" || vehicleModelView.MakeId == Guid.Empty)
            {
                return BadRequest("Invalid data.");
            }

            var vehicleModel = _mapper.Map<VehicleModel>(vehicleModelView);
            var isAdded = false;
            try
            {
                isAdded = await _vehicleModelService.AddVehicleModelAsync(vehicleModel);
                if (!isAdded) { return BadRequest("Error"); }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = vehicleModel.Id }, _mapper.Map<VehicleModelView>(vehicleModel));
        }


        // PUT: api/vehiclemodel/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] VehicleModelView vehicleModelView)
        {
            if (vehicleModelView == null || vehicleModelView.Abrv == "" || vehicleModelView.Name == "")
            {
                return BadRequest("Invalid data.");
            }

            var VehicleModel = _mapper.Map<VehicleModel>(vehicleModelView);
            VehicleModel.Id = id;
            var isUpdated = false;
            try
            {
                isUpdated = await _vehicleModelService.UpdateVehicleModelAsync(VehicleModel);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            return Ok(isUpdated);
        }

        // DELETE: api/vehiclemodel/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var isDeleted = false;
            try
            {
               isDeleted = await _vehicleModelService.DeleteVehicleModelAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(isDeleted);
        }
    }
}

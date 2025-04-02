using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Service.Common;
using AutoMapper;
using VehicleApp.Common;
using System.Linq.Expressions;

namespace VehicleApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
        }

        // GET: api/vehiclemake
        [HttpGet]
        public async Task<ActionResult<PagedList<VehicleMakeView>>> GetAllAsync(
            string name = null,
            string abrv = null,
            string sortBy = "Name",
            string sortOrder = "asc",
            int pageNumber = 1,
            int pageSize = 10)
        {
            Paging paging = new Paging() { PageNumber = pageNumber, PageSize=pageSize};
            Sorting sorting = new Sorting() { SortBy = sortBy, SortOrder = sortOrder };
            try
            {
                Expression<Func<VehicleMake, bool>> predicate = x =>
                    (string.IsNullOrEmpty(name) || x.Name.Contains(name)) &&
                    (string.IsNullOrEmpty(abrv) || x.Abrv.Contains(abrv));

                var vehicleMakes = await _vehicleMakeService.GetAllAsync(predicate, paging,sorting);

                if (vehicleMakes.Items == null || !vehicleMakes.Items.Any())
                {
                    return NotFound("No vehicle makes found.");
                }

                return Ok(vehicleMakes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/vehiclemake/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMakeView>> GetByIdAsync(Guid id)
        {
            try
            {
                var vehicleMake = await _vehicleMakeService.GetVehicleMakeByIdAsync(id);
                if (vehicleMake == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<VehicleMakeView>(vehicleMake));

            }catch (Exception ex) { return BadRequest(ex.Message); }

        }

        // POST: api/vehiclemake
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] VehicleMakeView vehicleMakeView)
        {
            if (vehicleMakeView == null || vehicleMakeView.Abrv == "" || vehicleMakeView.Name=="")
            {
                return BadRequest("Invalid data.");
            }

            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeView);
            try
            {
                await _vehicleMakeService.AddVehicleMakeAsync(vehicleMake);
            }
            catch (Exception ex) {  return BadRequest(ex.Message); }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = vehicleMake.Id }, _mapper.Map<VehicleMakeView>(vehicleMake));
        }

        // PUT: api/vehiclemake/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] VehicleMakeView vehicleMakeView)
        {
            if (vehicleMakeView == null || vehicleMakeView.Abrv == "" || vehicleMakeView.Name == "")
            {
                return BadRequest("Invalid data.");
            }

            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeView);
            vehicleMake.Id = id;
            try
            {
                await _vehicleMakeService.UpdateVehicleMakeAsync(vehicleMake);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            return Ok("Updated");
        }

        // DELETE: api/vehiclemake/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _vehicleMakeService.DeleteVehicleMakeAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Deleted");
        }
    }
}

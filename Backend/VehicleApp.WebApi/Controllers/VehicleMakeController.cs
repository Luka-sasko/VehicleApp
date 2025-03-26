using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Service.Common;
using AutoMapper;

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
        public async Task<ActionResult<IEnumerable<VehicleMakeView>>> GetAllAsync()
        {
            var vehicleMakes = await _vehicleMakeService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<VehicleMakeView>>(vehicleMakes));
        }

        // GET: api/vehiclemake/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMakeView>> GetByIdAsync(Guid id)
        {
            var vehicleMake = await _vehicleMakeService.GetByIdAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VehicleMakeView>(vehicleMake));
        }

        // POST: api/vehiclemake
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] VehicleMakeView vehicleMakeView)
        {
            if (vehicleMakeView == null)
            {
                return BadRequest("Invalid data.");
            }

            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeView);
            var success = await _vehicleMakeService.PostAsync(vehicleMake);

            if (!success)
            {
                return StatusCode(500, "Error creating resource.");
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = vehicleMake.Id }, _mapper.Map<VehicleMakeView>(vehicleMake));
        }

        // PUT: api/vehiclemake/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] VehicleMakeView vehicleMakeView)
        {
            if (vehicleMakeView == null)
            {
                return BadRequest("Invalid data.");
            }

            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeView);
            vehicleMake.Id = id;
            var success = await _vehicleMakeService.UpdateAsync(vehicleMake);

            if (!success)
            {
                return StatusCode(500, "Error updating resource.");
            }

            return NoContent();
        }

        // DELETE: api/vehiclemake/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var success = await _vehicleMakeService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

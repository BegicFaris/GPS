using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class ShiftController : MyControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShifts() =>
            Ok(await _shiftService.GetAllShiftsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShift(int id)
        {
            var shift = await _shiftService.GetShiftByIdAsync(id);
            if (shift == null) return NotFound();
            return Ok(shift);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift(Shift shift)
        {
            var createdShift = await _shiftService.CreateShiftAsync(shift);
            return CreatedAtAction(nameof(CreateShift), new { id = createdShift.Id }, createdShift);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, Shift shift)
        {
            if (id != shift.Id) return BadRequest();
            var updatedShift = await _shiftService.UpdateShiftAsync(shift);
            return Ok(updatedShift);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var success = await _shiftService.DeleteShiftAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

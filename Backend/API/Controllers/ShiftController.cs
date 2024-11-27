using GPS.API.Data.Models;
using GPS.API.Dtos.ShiftDtos;
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
        public async Task<IActionResult> CreateShift(ShiftCreateDto shiftCreateDto)
        {
            var shift = new Shift
            {
                BusId = shiftCreateDto.BusId,
                DriverId = shiftCreateDto.DriverId,
                ShiftDate = shiftCreateDto.ShiftDate,
                ShiftEndingTime = shiftCreateDto.ShiftEndingTime,
                ShiftStartingTime = shiftCreateDto.ShiftStartingTime,
            };

            var createdShift = await _shiftService.CreateShiftAsync(shift);
            return CreatedAtAction(nameof(CreateShift), new { id = createdShift.Id }, createdShift);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, ShiftUpdateDto shiftUpdateDto)
        {
            if (id != shiftUpdateDto.Id) return BadRequest();

            var existingShift = await _shiftService.GetShiftByIdAsync(id);
            if (existingShift == null) return NotFound($"Shift with Id:{id} not found!");

            if (shiftUpdateDto.BusId != null)
                existingShift.BusId = shiftUpdateDto.BusId.Value;
            if (shiftUpdateDto.DriverId != null)
                existingShift.DriverId = shiftUpdateDto.DriverId.Value;
            if (shiftUpdateDto.ShiftDate != null)
                existingShift.ShiftDate = shiftUpdateDto.ShiftDate.Value;
            if (shiftUpdateDto.ShiftStartingTime != null)
                existingShift.ShiftStartingTime = shiftUpdateDto.ShiftStartingTime.Value;
            if (shiftUpdateDto.ShiftEndingTime != null)
                existingShift.ShiftEndingTime = shiftUpdateDto.ShiftEndingTime.Value;

            var updatedShift = await _shiftService.UpdateShiftAsync(existingShift);
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

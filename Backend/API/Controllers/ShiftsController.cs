using GPS.API.Data.Models;
using GPS.API.Dtos.ShiftDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class ShiftsController : MyControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftsController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Driver)}")]
        [HttpGet]
        public async Task<IActionResult> GetAllShifts(CancellationToken cancellationToken) =>
            Ok(await _shiftService.GetAllShiftsAsync(cancellationToken));

        [Authorize(Roles = $"{nameof(UserRole.Manager)},{nameof(UserRole.Driver)}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShift(int id, CancellationToken cancellationToken)
        {
            var shift = await _shiftService.GetShiftByIdAsync(id, cancellationToken);
            if (shift == null) return NotFound();
            return Ok(shift);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateShift(ShiftCreateDto shiftCreateDto, CancellationToken cancellationToken)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
            if (role != UserRole.Manager.ToString())
                return Unauthorized();

            var shift = new Shift
            {
                BusId = shiftCreateDto.BusId,
                DriverId = shiftCreateDto.DriverId,
                ShiftDate = shiftCreateDto.ShiftDate,
                ShiftEndingTime = shiftCreateDto.ShiftEndingTime,
                ShiftStartingTime = shiftCreateDto.ShiftStartingTime,
            };

            var createdShift = await _shiftService.CreateShiftAsync(shift, cancellationToken);
            return CreatedAtAction(nameof(CreateShift), new { id = createdShift.Id }, createdShift);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, ShiftUpdateDto shiftUpdateDto, CancellationToken cancellationToken)
        {
            if (id != shiftUpdateDto.Id) return BadRequest();

            var existingShift = await _shiftService.GetShiftByIdAsync(id, cancellationToken);
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

            var updatedShift = await _shiftService.UpdateShiftAsync(existingShift, cancellationToken);
            return Ok(updatedShift);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id, CancellationToken cancellationToken)
        {
            var success = await _shiftService.DeleteShiftAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

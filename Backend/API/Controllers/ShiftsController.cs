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
        public async Task<IActionResult> GetAllShifts(CancellationToken cancellationToken, bool showDeleted=false) =>
            Ok(await _shiftService.GetAllShiftsAsync(cancellationToken, showDeleted));

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

        [Authorize(Roles = nameof(UserRole.Driver))]
        [HttpGet("driver-shift")]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetDriverShifts(
           CancellationToken cancellationToken,
           [FromQuery] int driverId,
           [FromQuery] DateTime? fromDate = null,
           [FromQuery] DateTime? toDate = null
           )
        {
            var shifts = await _shiftService.GetDriverShiftsAsync(cancellationToken, driverId, fromDate, toDate);
            return Ok(shifts);
        }

        [Authorize(Roles = nameof(UserRole.Driver))]
        [HttpGet("current")]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetCurrentShifts([FromQuery] int driverId, CancellationToken cancellationToken)
        {
            var shifts = await _shiftService.GetCurrentShiftsAsync(driverId,cancellationToken);
            return Ok(shifts);
        }

        [Authorize(Roles = nameof(UserRole.Driver))]
        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetUpcomingShifts([FromQuery] int driverId, CancellationToken cancellationToken)
        {
            var shifts = await _shiftService.GetUpcomingShiftsAsync(driverId, cancellationToken);
            return Ok(shifts);
        }

        [Authorize(Roles = nameof(UserRole.Driver))]
        [HttpGet("ended")]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetEndedShifts([FromQuery] int driverId, CancellationToken cancellationToken)
        {
            var shifts = await _shiftService.GetEndedShiftsAsync(driverId, cancellationToken);
            return Ok(shifts);
        }

        [Authorize(Roles = nameof(UserRole.Driver))]
        [HttpGet("details/{shiftId}")]
        public async Task<ActionResult<ShiftDto>> GetShiftDetails(int shiftId, CancellationToken cancellationToken)
        {
            var shiftDetails = await _shiftService.GetShiftDetailsAsync(shiftId, cancellationToken);
            if (shiftDetails == null)
                return NotFound();

            return Ok(shiftDetails);
        }

        [Authorize(Roles = nameof(UserRole.Driver))]
        [HttpGet("{shiftId}/pdf")]
        public async Task<IActionResult> GetShiftPdf(int shiftId, CancellationToken cancellationToken)
        {
            var pdfData = await _shiftService.GenerateShiftPdfAsync(shiftId, cancellationToken);
            if (pdfData == null)
                return NotFound();

            return File(pdfData, "application/pdf", $"shift-report-{shiftId}.pdf");
        }

    }
}

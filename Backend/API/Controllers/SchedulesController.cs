using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Dtos.ScheduleDtos;
using Microsoft.AspNetCore.Authorization;

namespace GPS.API.Controllers
{
    public class SchedulesController : MyControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllSchedules(CancellationToken cancellationToken, bool includeDeleted = false) =>
            Ok(await _scheduleService.GetAllSchedulesAsync(cancellationToken,includeDeleted));

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchedule(int id, CancellationToken cancellationToken)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id, cancellationToken);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }

        [Authorize]
        [HttpGet("line/{lineId}")]
        public async Task<IActionResult> GetAllSchedulesByLineId(int lineId, CancellationToken cancellationToken, bool includeDeleted = false) =>
            Ok(await _scheduleService.GetAllSchedulesByLineIdAsync(lineId, cancellationToken,includeDeleted ));


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateSchedule(ScheduleCreateDto scheduleCreateDto, CancellationToken cancellationToken)
        {
            var schedule = new Schedule
            {
                DepartureTime = scheduleCreateDto.DepartureTime,
                LineId = scheduleCreateDto.LineId,
            };
            var createdSchedule = await _scheduleService.CreateScheduleAsync(schedule, cancellationToken);
            return CreatedAtAction(nameof(GetSchedule), new { id = createdSchedule.Id }, createdSchedule);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, ScheduleUpdateDto scheduleUpdateDto, CancellationToken cancellationToken)
        {
            if (id != scheduleUpdateDto.Id) return BadRequest();

            var existingSchedule = await _scheduleService.GetScheduleByIdAsync(id, cancellationToken);
            if (existingSchedule == null) return BadRequest($"Schedule with Id:{id} not found!");

            if (scheduleUpdateDto.LineId != null)
                existingSchedule.LineId = scheduleUpdateDto.LineId.Value;
            if (scheduleUpdateDto.DepartureTime != null)
                existingSchedule.DepartureTime = scheduleUpdateDto.DepartureTime.Value;

            var updatedSchedule = await _scheduleService.UpdateScheduleAsync(existingSchedule, cancellationToken);
            return Ok(updatedSchedule);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id, CancellationToken cancellationToken)
        {
            var success = await _scheduleService.DeleteScheduleAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

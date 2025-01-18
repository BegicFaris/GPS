using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Dtos.ScheduleDtos;

namespace GPS.API.Controllers
{
    public class SchedulesController : MyControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedules() =>
            Ok(await _scheduleService.GetAllSchedulesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }
        [HttpGet("line/{lineId}")]
        public async Task<IActionResult> GetAllSchedulesByLineId(int lineId) =>
            Ok(await _scheduleService.GetAllSchedulesByLineIdAsync(lineId));

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(ScheduleCreateDto scheduleCreateDto)
        {
            var schedule = new Schedule
            {
                DepartureTime = scheduleCreateDto.DepartureTime,
                LineId = scheduleCreateDto.LineId,
            };
            var createdSchedule = await _scheduleService.CreateScheduleAsync(schedule);
            return CreatedAtAction(nameof(GetSchedule), new { id = createdSchedule.Id }, createdSchedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, ScheduleUpdateDto scheduleUpdateDto)
        {
            if (id != scheduleUpdateDto.Id) return BadRequest();

            var existingSchedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (existingSchedule == null) return BadRequest($"Schedule with Id:{id} not found!");

            if (scheduleUpdateDto.LineId != null)
                existingSchedule.LineId = scheduleUpdateDto.LineId.Value;
            if (scheduleUpdateDto.DepartureTime != null)
                existingSchedule.DepartureTime = scheduleUpdateDto.DepartureTime.Value;

            var updatedSchedule = await _scheduleService.UpdateScheduleAsync(existingSchedule);
            return Ok(updatedSchedule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var success = await _scheduleService.DeleteScheduleAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;

namespace GPS.API.Controllers
{
    public class ScheduleController: MyControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
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

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(Schedule schedule)
        {
            var createdSchedule = await _scheduleService.CreateScheduleAsync(schedule);
            return CreatedAtAction(nameof(GetSchedule), new { id = createdSchedule.Id }, createdSchedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, Schedule schedule)
        {
            if (id != schedule.Id) return BadRequest();
            var updatedSchedule = await _scheduleService.UpdateScheduleAsync(schedule);
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

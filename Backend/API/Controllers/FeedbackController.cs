using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class FeedbackController : MyControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks() =>
            Ok(await _feedbackService.GetAllFeedbacksAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedback(int id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null) return NotFound();
            return Ok(feedback);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback(Feedback feedback)
        {
            var createdFeedback = await _feedbackService.CreateFeedbackAsync(feedback);
            return CreatedAtAction(nameof(CreateFeedback), new { id = createdFeedback.Id }, createdFeedback);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, Feedback feedback)
        {
            if (id != feedback.Id) return BadRequest();
            var updatedFeedback = await _feedbackService.UpdateFeedbackAsync(feedback);
            return Ok(updatedFeedback);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var success = await _feedbackService.DeleteFeedbackAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

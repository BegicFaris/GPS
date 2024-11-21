using GPS.API.Data.Models;
using GPS.API.Dtos.FeedbackDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

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
        public async Task<IActionResult> CreateFeedback(FeedbackCreateDto feedbackCreateDto)
        {
            var feedback = new Feedback()
            {
                UserId = feedbackCreateDto.UserId,
                Comment = feedbackCreateDto.Comment,
                Rating = feedbackCreateDto.Rating,
                Date = feedbackCreateDto.Date,
                Picture = feedbackCreateDto.Picture,
            };
            var createdFeedback = await _feedbackService.CreateFeedbackAsync(feedback);
            return CreatedAtAction(nameof(CreateFeedback), new { id = createdFeedback.Id }, createdFeedback);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, FeedbackUpdateDto feedbackUpdateDto)
        {
            if (id != feedbackUpdateDto.Id) return BadRequest();

            var existingFeedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (existingFeedback == null) return NotFound($"Feedback with Id:{id} not found!");

            if (feedbackUpdateDto.UserId != null)
                existingFeedback.UserId = feedbackUpdateDto.UserId.Value;
            if(feedbackUpdateDto.Rating != null)
                existingFeedback.Rating= feedbackUpdateDto.Rating.Value;
            if (feedbackUpdateDto.Date != null)
                existingFeedback.Date = feedbackUpdateDto.Date.Value;
            
            existingFeedback.Comment= feedbackUpdateDto.Comment;
            existingFeedback.Picture= feedbackUpdateDto.Picture;


            var updatedFeedback = await _feedbackService.UpdateFeedbackAsync(existingFeedback);
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

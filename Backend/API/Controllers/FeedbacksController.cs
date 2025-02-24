using GPS.API.Data.Models;
using GPS.API.Dtos.FeedbackDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace GPS.API.Controllers
{
    public class FeedbacksController : MyControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

  
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks(CancellationToken cancellationToken) =>
            Ok(await _feedbackService.GetAllFeedbacksAsync(cancellationToken));

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedback(int id, CancellationToken cancellationToken)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id, cancellationToken);
            if (feedback == null) return NotFound();
            return Ok(feedback);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateFeedback(FeedbackCreateDto feedbackCreateDto, CancellationToken cancellationToken)
        {
            var feedback = new Feedback()
            {
                UserId = feedbackCreateDto.UserId,
                Comment = feedbackCreateDto.Comment,
                Rating = feedbackCreateDto.Rating,
                Date = feedbackCreateDto.Date,
                Picture = feedbackCreateDto.Picture,
            };
            var createdFeedback = await _feedbackService.CreateFeedbackAsync(feedback, cancellationToken);
            return CreatedAtAction(nameof(CreateFeedback), new { id = createdFeedback.Id }, createdFeedback);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, FeedbackUpdateDto feedbackUpdateDto, CancellationToken cancellationToken)
        {
            if (id != feedbackUpdateDto.Id) return BadRequest();

            var existingFeedback = await _feedbackService.GetFeedbackByIdAsync(id, cancellationToken);
            if (existingFeedback == null) return NotFound($"Feedback with Id:{id} not found!");

            if (feedbackUpdateDto.UserId != null)
                existingFeedback.UserId = feedbackUpdateDto.UserId.Value;
            if(feedbackUpdateDto.Rating != null)
                existingFeedback.Rating= feedbackUpdateDto.Rating.Value;
            if (feedbackUpdateDto.Date != null)
                existingFeedback.Date = feedbackUpdateDto.Date.Value;
            
            existingFeedback.Comment= feedbackUpdateDto.Comment;
            existingFeedback.Picture= feedbackUpdateDto.Picture;


            var updatedFeedback = await _feedbackService.UpdateFeedbackAsync(existingFeedback, cancellationToken);
            return Ok(updatedFeedback);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id, CancellationToken cancellationToken)
        {
            var success = await _feedbackService.DeleteFeedbackAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

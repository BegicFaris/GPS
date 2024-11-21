using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.FeedbackServices
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;
        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync() =>
            await _context.Feedbacks.ToListAsync();
        public async Task<Feedback> GetFeedbackByIdAsync(int id) =>
          await _context.Feedbacks.FindAsync(id);
        public async Task<Feedback> CreateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }


        public async Task<Feedback> UpdateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return false;
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}

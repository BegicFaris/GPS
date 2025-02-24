using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.FeedbackServices
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync(CancellationToken cancellationToken) =>
            await _context.Feedbacks.Include(x => x.User).ToListAsync(cancellationToken);

        public async Task<Feedback?> GetFeedbackByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Feedbacks.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Feedback> CreateFeedbackAsync(Feedback feedback, CancellationToken cancellationToken)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            return feedback;
        }

        public async Task<Feedback> UpdateFeedbackAsync(Feedback feedback, CancellationToken cancellationToken)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            return feedback;
        }

        public async Task<bool> DeleteFeedbackAsync(int id, CancellationToken cancellationToken)
        {
            var feedback = await _context.Feedbacks.FindAsync(new object[] { id }, cancellationToken);
            if (feedback == null) return false;
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

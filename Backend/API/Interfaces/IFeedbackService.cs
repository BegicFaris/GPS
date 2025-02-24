using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacksAsync(CancellationToken cancellationToken);
        Task<Feedback?> GetFeedbackByIdAsync(int id, CancellationToken cancellationToken);
        Task<Feedback> CreateFeedbackAsync(Feedback feedback, CancellationToken cancellationToken);
        Task<Feedback> UpdateFeedbackAsync(Feedback feedback, CancellationToken cancellationToken);
        Task<bool> DeleteFeedbackAsync(int id, CancellationToken cancellationToken);
    }
}

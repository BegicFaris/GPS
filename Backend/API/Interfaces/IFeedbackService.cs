using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacksAsync();
        Task<Feedback> GetFeedbackByIdAsync(int id);
        Task<Feedback> CreateFeedbackAsync(Feedback feedback);
        Task<Feedback> UpdateFeedbackAsync(Feedback feedback);
        Task<bool> DeleteFeedbackAsync(int id);
    }
}

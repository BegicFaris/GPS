using GPS.API.Data.Models;
using GPS.API.Dtos;
using GPS.API.Services.NotificationServices;

namespace GPS.API.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task<IEnumerable<Notification>> GetRecentNotificationsAsync(string tenantId, int hours = 48);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<PagedResult<Notification>> GetNotificationsAsync(int page, int pageSize);
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task<Notification> UpdateNotificationAsync(Notification notification  );
        Task<bool> DeleteNotificationAsync(int id);
    }
}

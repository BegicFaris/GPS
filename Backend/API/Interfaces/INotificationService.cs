using GPS.API.Data.Models;
using GPS.API.Dtos;
using GPS.API.Services.NotificationServices;

namespace GPS.API.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllNotificationsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Notification>> GetRecentNotificationsAsync(string tenantId, int? hours, CancellationToken cancellationToken);
        Task<Notification?> GetNotificationByIdAsync(int id, CancellationToken cancellationToken);
        Task<Notification> CreateNotificationAsync(Notification notification, CancellationToken cancellationToken);
        Task<PagedResult<Notification>> GetNotificationsAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<Notification> UpdateNotificationAsync(Notification notification, CancellationToken cancellationToken);
        Task<bool> DeleteNotificationAsync(int id, CancellationToken cancellationToken);
    }
}

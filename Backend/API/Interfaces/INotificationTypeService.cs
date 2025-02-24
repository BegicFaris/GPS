using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface INotificationTypeService
    {
        Task<IEnumerable<NotificationType>> GetAllNotificationTypesAsync(CancellationToken cancellationToken );
        Task<NotificationType?> GetNotificationTypeByIdAsync(int id, CancellationToken cancellationToken );
    }
}

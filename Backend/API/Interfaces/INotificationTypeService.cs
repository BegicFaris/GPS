using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface INotificationTypeService
    {
        Task<IEnumerable<NotificationType>> GetAllNotificationTypesAsync();
        Task<NotificationType> GetNotificationTypeByIdAsync(int id);
    }
}

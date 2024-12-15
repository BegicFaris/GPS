using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Services.NotificationTypeService
{
    public class NotificationTypeService : INotificationTypeService
    {

        private readonly ApplicationDbContext _context;
        public NotificationTypeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<NotificationType>> GetAllNotificationTypesAsync() =>
            await _context.NotificationTypes.ToListAsync();
        public async Task<NotificationType> GetNotificationTypeByIdAsync(int id) =>
            await _context.NotificationTypes.SingleOrDefaultAsync(x => x.Id == id);

    }

}

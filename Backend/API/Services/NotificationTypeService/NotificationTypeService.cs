using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.API.Services.NotificationTypeService
{
    public class NotificationTypeService : INotificationTypeService
    {
        private readonly ApplicationDbContext _context;

        public NotificationTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificationType>> GetAllNotificationTypesAsync(CancellationToken cancellationToken) =>
            await _context.NotificationTypes.ToListAsync(cancellationToken);

        public async Task<NotificationType?> GetNotificationTypeByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.NotificationTypes.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}

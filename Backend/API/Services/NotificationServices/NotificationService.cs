using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Dtos;
using GPS.API.Interfaces;
using GPS.API.Services.EmailServices;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GPS.API.Services.NotificationServices
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public NotificationService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync(CancellationToken cancellationToken, bool includeDeleted = false)
        {
            var query = _context.Notifications.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();

                var currentTenantId = _context.CurrentTenantID;
                if (string.IsNullOrEmpty(currentTenantId))
                {
                    return new List<Notification>();
                }

                query = query.Where(x => x.TenantId == currentTenantId);
            }

            query = query.Include(x => x.NotificationType)
                         .Include(x => x.Line);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> GetRecentNotificationsAsync(string tenantId, int? hours, CancellationToken cancellationToken)
        {
            int h = hours ?? 48;

            var cutoffTime = DateTime.UtcNow.AddHours(-h);
            return await _context.Notifications
                .Where(n => n.TenantId == tenantId && n.CreationDate >= cutoffTime)
                .Include(n => n.NotificationType)
                .OrderByDescending(n => n.CreationDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id, CancellationToken cancellationToken) =>
          await _context.Notifications.Include(x => x.NotificationType).Include(x => x.Line).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);


        public async Task<Notification> CreateNotificationAsync(Notification notification, CancellationToken cancellationToken)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync(cancellationToken);
            await CheckFavoriteAndSendEmailAsync(notification, cancellationToken);
            return notification;
        }

        private async Task CheckFavoriteAndSendEmailAsync(Notification notification, CancellationToken cancellationToken)
        {
            var favoriteLines = await _context.FavoriteLines
                .Where(fl => fl.LineId == notification.LineId)
                .Include(fl => fl.User)
                .ToListAsync(cancellationToken);

            foreach (var favoriteLine in favoriteLines)
            {
                if (favoriteLine.User != null && !string.IsNullOrEmpty(favoriteLine.User.Email))
                {
                    await _emailService.SendEmailAsync(
                        favoriteLine.User.Email,
                        $"New Notification for Favorite Line",
                        $"<h1>{notification.Title}</h1><p>{notification.Description}</p>",
                        cancellationToken
                    );
                }
            }
        }

        public async Task<PagedResult<Notification>> GetNotificationsAsync(int page, int pageSize, CancellationToken cancellationToken, bool includeDeleted = false)
        {
            if (page < 1) page = 1;

            var result = new PagedResult<Notification>();


            var query = _context.Notifications.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();

                var currentTenantId = _context.CurrentTenantID;
                if (string.IsNullOrEmpty(currentTenantId))
                {
                    return new PagedResult<Notification>();
                }

                query = query.Where(x => x.TenantId == currentTenantId);
            }


            result.TotalCount = await query
                .OrderByDescending(n => n.CreationDate)
                .CountAsync(cancellationToken);

            result.Items = await query
                .OrderByDescending(n => n.CreationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(n => n.NotificationType)
                .Include(n => n.Line)
                .Include(n => n.Manager)
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<Notification> UpdateNotificationAsync(Notification notification, CancellationToken cancellationToken)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return notification;
        }

        public async Task<bool> DeleteNotificationAsync(int id, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.SingleOrDefaultAsync(x => x.Id == id);
            if (notification == null) return false;
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

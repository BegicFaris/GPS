using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class NotificationTypesController:MyControllerBase
    {
        private readonly INotificationTypeService _notificationTypeService;

        public NotificationTypesController(INotificationTypeService notificationTypeService)
        {
            _notificationTypeService = notificationTypeService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications(CancellationToken cancellationToken) =>
            Ok(await _notificationTypeService.GetAllNotificationTypesAsync(cancellationToken));

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id, CancellationToken cancellationToken)
        {
            var notificationType = await _notificationTypeService.GetNotificationTypeByIdAsync(id, cancellationToken);
            if (notificationType == null) return NotFound();
            return Ok(notificationType);
        }


    }
}

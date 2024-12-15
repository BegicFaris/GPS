using GPS.API.Interfaces;
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

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications() =>
            Ok(await _notificationTypeService.GetAllNotificationTypesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id)
        {
            var notificationType = await _notificationTypeService.GetNotificationTypeByIdAsync(id);
            if (notificationType == null) return NotFound();
            return Ok(notificationType);
        }


    }
}

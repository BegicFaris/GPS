using GPS.API.Data.Models;
using GPS.API.Dtos;
using GPS.API.Dtos.NotificationDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class NotificationsController : MyControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;

        public NotificationsController(INotificationService notificationService, IEmailService emailService)
        {
            _notificationService = notificationService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications() =>
            Ok(await _notificationService.GetAllNotificationsAsync());

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<Notification>>> GetNotifications(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 7)
        {
            try
            {
                var result = await _notificationService.GetNotificationsAsync(page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving notifications");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationCreateDto notificationCreateDto)
        {
            var notification = new Notification
            {
                Title = notificationCreateDto.Title,
                Description = notificationCreateDto.Description,
                Image= notificationCreateDto.Image,
                NotificationTypeId = notificationCreateDto.NotificationTypeId,
                CreationDate = notificationCreateDto.CreationDate,
                LineId = notificationCreateDto.LineId,
                ManagerId = notificationCreateDto.ManagerId,
            };
            var createdNotification = await _notificationService.CreateNotificationAsync(notification);
            return CreatedAtAction(nameof(CreateNotification), new { id = createdNotification.Id }, createdNotification);
        }

        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetRecentNotifications([FromQuery] int hours = 48)
        {
            var tenantId = User.FindFirst("TenantId")?.Value;
            if (string.IsNullOrEmpty(tenantId))
                return Unauthorized();

            var notifications = await _notificationService.GetRecentNotificationsAsync(tenantId, hours);
            return Ok(notifications);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, NotificationUpdateDto notificationUpdateDto)
        {
            if (id != notificationUpdateDto.Id) return BadRequest();

            var existingNotification = await _notificationService.GetNotificationByIdAsync(id);
            if (existingNotification == null) return NotFound($"Notification with Id:{id} not found!");

            if (notificationUpdateDto.Title != null)
                existingNotification.Title = notificationUpdateDto.Title;
            if (notificationUpdateDto.Description !=null)
                existingNotification.Description = notificationUpdateDto.Description;
            if (notificationUpdateDto.Image != null)
                existingNotification.Image = notificationUpdateDto.Image;
            if (notificationUpdateDto.NotificationTypeId != null)
                existingNotification.NotificationTypeId = notificationUpdateDto.NotificationTypeId.Value;
            if (notificationUpdateDto.CreationDate != null)
                existingNotification.CreationDate = notificationUpdateDto.CreationDate.Value;
            if (notificationUpdateDto.LineId != null)
                existingNotification.LineId = notificationUpdateDto.LineId.Value;
            if(notificationUpdateDto.ManagerId != null)
                existingNotification.ManagerId= notificationUpdateDto.ManagerId.Value;

            var updatedNotification = await _notificationService.UpdateNotificationAsync(existingNotification);
            return Ok(updatedNotification);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var success = await _notificationService.DeleteNotificationAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

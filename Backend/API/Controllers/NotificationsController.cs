using GPS.API.Data.Models;
using GPS.API.Dtos;
using GPS.API.Dtos.NotificationDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications(CancellationToken cancellationToken) =>
            Ok(await _notificationService.GetAllNotificationsAsync(cancellationToken));

        [Authorize]
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<Notification>>> GetNotifications(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 7,
        CancellationToken cancellationToken=default)
        {
            try
            {
                var result = await _notificationService.GetNotificationsAsync(page, pageSize, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving notifications: {ex}");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id, CancellationToken cancellationToken)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id, cancellationToken);
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationCreateDto notificationCreateDto, CancellationToken cancellationToken)
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
            var createdNotification = await _notificationService.CreateNotificationAsync(notification, cancellationToken);
            return CreatedAtAction(nameof(CreateNotification), new { id = createdNotification.Id }, createdNotification);
        }

        [Authorize]
        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetRecentNotifications([FromQuery] int hours = 48, CancellationToken cancellationToken = default)
        {
            var tenantId = User.FindFirst("TenantId")?.Value;
            if (string.IsNullOrEmpty(tenantId))
                return Unauthorized();

            var notifications = await _notificationService.GetRecentNotificationsAsync(tenantId, hours, cancellationToken);
            return Ok(notifications);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, NotificationUpdateDto notificationUpdateDto, CancellationToken cancellationToken)
        {
            if (id != notificationUpdateDto.Id) return BadRequest();

            var existingNotification = await _notificationService.GetNotificationByIdAsync(id, cancellationToken);
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

            existingNotification.LineId = notificationUpdateDto.LineId == null ? null: notificationUpdateDto.LineId.Value;
            if(notificationUpdateDto.ManagerId != null)
                existingNotification.ManagerId= notificationUpdateDto.ManagerId.Value;

            var updatedNotification = await _notificationService.UpdateNotificationAsync(existingNotification, cancellationToken);
            return Ok(updatedNotification);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id, CancellationToken cancellationToken)
        {
            var success = await _notificationService.DeleteNotificationAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

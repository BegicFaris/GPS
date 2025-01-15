using GPS.API.Data.Models;
using GPS.API.Dtos.NotificationDtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.API.Controllers
{
    public class NotificationsController : MyControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications() =>
            Ok(await _notificationService.GetAllNotificationsAsync());

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

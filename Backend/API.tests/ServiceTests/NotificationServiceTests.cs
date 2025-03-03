using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.NotificationServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.ServiceTests
{
    public class NotificationServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<ICurrentTenantService> _mockCurrentTenantService;
        private readonly NotificationService _service;

        public NotificationServiceTests()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
            .Options;

            _mockCurrentTenantService = new Mock<ICurrentTenantService>();
            _mockCurrentTenantService.Setup(t => t.TenantId).Returns("tenant1");

            _context = new ApplicationDbContext(options, _mockCurrentTenantService.Object);
            _mockEmailService = new Mock<IEmailService>();
            _service = new NotificationService(_context, _mockEmailService.Object);
        }

        private async Task<Notification> CreateTestNotificationAsync(string title = "TestNotif")
        {
            var line = new Line { CompleteDistance = "10", IsActive = true, Name = "21: Bisce polje : Humi" };
            var notifType = new NotificationType { Name = "Regular", Description = "Nothing special about this" };

            using var hmac = new HMACSHA512();
            var manager = new Manager
            {
                Email = "11@gmail.com",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123")),
                BirthDate = new DateOnly(2000, 5, 14),
                RegistrationDate = DateTime.UtcNow,
                PasswordSalt = hmac.Key,
                FirstName = "Dzanan",
                HireDate = new DateOnly(2024, 12, 1),
                LastName = "Musa",
                Address = "123 Main Street",
                Department = "HR",
                ManagerLevel = "1",
                TenantId = "tenant1",
                TwoFactorEnabled = false
            };

            _context.Lines.Add(line);
            _context.NotificationTypes.Add(notifType);
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();

            var notification = new Notification
            {
                Title = title,
                Description = "Test Description",
                TenantId = "tenant1",
                LineId = line.Id,
                ManagerId = manager.Id,
                NotificationTypeId = notifType.Id,
                CreationDate = DateTime.UtcNow
            };

            return notification;
        }

        [Fact]
        public async Task CreateNotificationAsync_AddsNotificationToDatabase()
        {
            var notification = await CreateTestNotificationAsync();

            var result = await _service.CreateNotificationAsync(notification, CancellationToken.None);

            var savedNotification = await _context.Notifications.FindAsync(result.Id);
            Assert.NotNull(savedNotification);
            Assert.Equal(notification.Title, savedNotification.Title);
        }

        [Fact]
        public async Task GetNotificationByIdAsync_Should_ReturnNotification_WhenExists()
        {
            var notification = await CreateTestNotificationAsync();
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            var result = await _service.GetNotificationByIdAsync(notification.Id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(notification.Title, result.Title);
        }


        [Fact]
        public async Task GetNotificationByIdAsync_Should_ReturnNull_WhenNotExists()
        {
            var result = await _service.GetNotificationByIdAsync(999, CancellationToken.None);
            Assert.Null(result);
        }


        [Fact]
        public async Task GetAllNotificationsAsync_Should_ReturnAllNotifications()
        {
            var notif1 = await CreateTestNotificationAsync("Notif1");
            var notif2 = await CreateTestNotificationAsync("Notif2");
            _context.Notifications.Add(notif1);
            _context.Notifications.Add(notif2);
            await _context.SaveChangesAsync();

            var result = await _service.GetAllNotificationsAsync(CancellationToken.None);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetRecentNotificationsAsync_Should_Return_NotificationsWithinGivenHours()
        {
            var notif1 = await CreateTestNotificationAsync("Old Notification");
            notif1.CreationDate = DateTime.UtcNow.AddHours(-49);
            var notif2 = await CreateTestNotificationAsync("Recent Notification");
            notif2.CreationDate = DateTime.UtcNow.AddHours(-10);
            _context.Notifications.Add(notif1);
            _context.Notifications.Add(notif2);
            await _context.SaveChangesAsync();

            var result = await _service.GetRecentNotificationsAsync("tenant1", 48, CancellationToken.None);

            Assert.Single(result);
            Assert.Equal("Recent Notification", result.First().Title);
        }

        [Fact]
        public async Task UpdateNotificationAsync_Should_UpdateNotification_WhenExists()
        {
            var notification = await CreateTestNotificationAsync();
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            notification.Title = "Updated Title";
            notification.Description = "Updated Description";

            var updatedNotification = await _service.UpdateNotificationAsync(notification, CancellationToken.None);

            Assert.NotNull(updatedNotification);
            Assert.Equal("Updated Title", updatedNotification.Title);
            Assert.Equal("Updated Description", updatedNotification.Description);
        }

        [Fact]
        public async Task DeleteNotificationAsync_Should_RemoveNotification_WhenExists()
        {
            var notification = await CreateTestNotificationAsync();
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            var result = await _service.DeleteNotificationAsync(notification.Id, CancellationToken.None);

            Assert.True(result);
            var deletedNotification = await _context.Notifications.FindAsync(notification.Id);
            Assert.Null(deletedNotification);
        }

        [Fact]
        public async Task DeleteNotificationAsync_Should_ReturnFalse_WhenNotExists()
        {
            var result = await _service.DeleteNotificationAsync(999, CancellationToken.None);
            Assert.False(result);
        }

    }
}

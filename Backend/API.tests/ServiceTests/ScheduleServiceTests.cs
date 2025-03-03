using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.ScheduleServices;
using iText.Kernel.Geom;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Line = GPS.API.Data.Models.Line;

namespace API.Tests.ServiceTests
{
    public class ScheduleServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ScheduleService _scheduleService;
        private readonly Mock<ICurrentTenantService> _mockCurrentTenantService;

        public ScheduleServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

            _mockCurrentTenantService = new Mock<ICurrentTenantService>();
            _mockCurrentTenantService.Setup(t => t.TenantId).Returns("tenant1");

            _context = new ApplicationDbContext(options, _mockCurrentTenantService.Object);
            _scheduleService = new ScheduleService(_context);
        }

        private async Task<Schedule> CreateTestScheduleAsync()
        {
            var line = new Line { CompleteDistance = "10", IsActive = true, Name = "21: Bisce polje : Humi" };

            _context.Lines.Add(line);
            await _context.SaveChangesAsync();

            var schedule = new Schedule { Id = 1, LineId = line.Id, DepartureTime = new TimeOnly(0, 15) };
            return schedule;
        }



        [Fact]
        public async Task GetAllSchedulesAsync_ShouldReturnSchedules()
        {
            // Arrange
            var schedule = await CreateTestScheduleAsync();
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            // Act
            var result = await _scheduleService.GetAllSchedulesAsync(CancellationToken.None);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetScheduleByIdAsync_ShouldReturnSchedule()
        {
            // Arrange
            var schedule = await CreateTestScheduleAsync();
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            // Act
            var result = await _scheduleService.GetScheduleByIdAsync(1, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task CreateScheduleAsync_ShouldAddSchedule()
        {
            // Arrange
            var schedule = await CreateTestScheduleAsync();

            // Act
            var result = await _scheduleService.CreateScheduleAsync(schedule, CancellationToken.None);

            // Assert
            Assert.NotNull(await _context.Schedules.FindAsync(schedule.Id));
        }

        [Fact]
        public async Task UpdateScheduleAsync_ShouldModifySchedule()
        {
            // Arrange
            var schedule = await CreateTestScheduleAsync();
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            // Act
            schedule.DepartureTime = new TimeOnly(0, 25);
            var updatedSchedule = await _scheduleService.UpdateScheduleAsync(schedule, CancellationToken.None);

            // Assert
            Assert.Equal(schedule.DepartureTime, updatedSchedule.DepartureTime);
        }

        [Fact]
        public async Task DeleteScheduleAsync_ShouldRemoveSchedule()
        {
            var schedule = await CreateTestScheduleAsync();
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            // Act
            var result = await _scheduleService.DeleteScheduleAsync(schedule.Id, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Null(await _context.Schedules.FindAsync(schedule.Id));
        }

        [Fact]
        public async Task GetAllSchedulesByLineIdAsync_ShouldReturnSchedulesForLine()
        {
            var schedule = await CreateTestScheduleAsync();
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            // Act
            var result = await _scheduleService.GetAllSchedulesByLineIdAsync(1, CancellationToken.None);

            // Assert
            Assert.Single(result);
        }
    }
}

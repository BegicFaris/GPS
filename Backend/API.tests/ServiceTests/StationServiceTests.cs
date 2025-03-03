using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.StationServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.ServiceTests
{
    public class StationServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<ICurrentTenantService> _mockCurrentTenantService;
        private readonly StationService _service;

        public StationServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _mockCurrentTenantService = new Mock<ICurrentTenantService>();
            _mockCurrentTenantService.Setup(t => t.TenantId).Returns("tenant1");

            _context = new ApplicationDbContext(options, _mockCurrentTenantService.Object);
            _service = new StationService(_context);
        }

        private async Task<Station> CreateTestStationAsync(string name = "Test Station")
        {
            var zone = new Zone { Name = "Zone 1" };
            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();

            var station = new Station
            {
                Name = name,
                ZoneId = zone.Id,
                Location = "Test Location",
                GPSCode = "1.23,4.53"
            };

            _context.Stations.Add(station);
            await _context.SaveChangesAsync();

            return station;
        }

        [Fact]
        public async Task GetAllStationsAsync_Should_ReturnAllStations()
        {
            await CreateTestStationAsync("Station 1");
            await CreateTestStationAsync("Station 2");

            var result = await _service.GetAllStationsAsync(CancellationToken.None);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetStationByIdAsync_Should_ReturnStation_WhenExists()
        {
            var station = await CreateTestStationAsync();

            var result = await _service.GetStationByIdAsync(station.Id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(station.Name, result.Name);
        }

        [Fact]
        public async Task GetStationByIdAsync_Should_ReturnNull_WhenNotExists()
        {
            var result = await _service.GetStationByIdAsync(999, CancellationToken.None);
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateStationAsync_Should_AddStationToDatabase()
        {
            var station = new Station { Name = "New Station", ZoneId = 1,
                Location = "Test Location",
                GPSCode = "1.23,4.53"
            };

            var result = await _service.CreateStationAsync(station, CancellationToken.None);

            var savedStation = await _context.Stations.FindAsync(result.Id);
            Assert.NotNull(savedStation);
            Assert.Equal("New Station", savedStation.Name);
        }

        [Fact]
        public async Task UpdateStationAsync_Should_UpdateStation_WhenExists()
        {
            var station = await CreateTestStationAsync();
            station.Name = "Updated Station";

            var updatedStation = await _service.UpdateStationAsync(station, CancellationToken.None);

            Assert.NotNull(updatedStation);
            Assert.Equal("Updated Station", updatedStation.Name);
        }

        [Fact]
        public async Task DeleteStationAsync_Should_RemoveStation_WhenExists()
        {
            var station = await CreateTestStationAsync();

            var result = await _service.DeleteStationAsync(station.Id, CancellationToken.None);

            Assert.True(result);
            var deletedStation = await _context.Stations.FindAsync(station.Id);
            Assert.Null(deletedStation);
        }

        [Fact]
        public async Task DeleteStationAsync_Should_ThrowException_WhenNotExists()
        {
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _service.DeleteStationAsync(999, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteStationAsync_Should_ThrowException_WhenStationIsInUse()
        {
            var line1 = new Line { Name = "10: Station X - Station Y", CompleteDistance = "15", IsActive = true };
            var station = await CreateTestStationAsync();
            _context.Routes.Add(new Route { LineId = line1.Id, StationId = station.Id, DistanceFromTheNextStation = new TimeOnly(0, 15), Order = 1 });
            await _context.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _service.DeleteStationAsync(station.Id, CancellationToken.None));
        }
    }
}

using FluentValidation.TestHelper;
using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.LineServices;
using GPS.API.Validators.LineValidators;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.ServiceTests
{
    public class LineServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly LineService _service;
        private readonly LineValidator _validator;

        public LineServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
                .Options;

            _context = new ApplicationDbContext(options, Mock.Of<ICurrentTenantService>());
            _service = new LineService(_context);
            _validator = new LineValidator();
        }

        [Fact]
        public async Task CreateLineAsync_Should_AddValidLine()
        {
            var line = new Line { Name = "21: Bisce polje - Humi", CompleteDistance = "15", IsActive = true };

            var validationResult = _validator.TestValidate(line);
            validationResult.ShouldNotHaveAnyValidationErrors();

            var createdLine = await _service.CreateLineAsync(line, CancellationToken.None);

            var savedLine = await _context.Lines.FindAsync(createdLine.Id);
            Assert.NotNull(savedLine);
            Assert.Equal("21: Bisce polje - Humi", savedLine.Name);
        }

        [Fact]
        public void CreateLineAsync_Should_FailValidation_WhenInvalid()
        {
            var invalidLine = new Line { Name = "InvalidName", CompleteDistance = "-5" , IsActive=true};

            var validationResult = _validator.TestValidate(invalidLine);
            validationResult.ShouldHaveValidationErrorFor(x => x.Name);
            validationResult.ShouldHaveValidationErrorFor(x => x.CompleteDistance);
        }

        [Fact]
        public async Task UpdateLineAsync_Should_UpdateExistingLine()
        {
            var line = new Line { Name = "30: Station A - Station B", CompleteDistance = "20", IsActive = true };
            await _service.CreateLineAsync(line, CancellationToken.None);

            line.Name = "30: Updated Station - New Destination";
            var updatedLine = await _service.UpdateLineAsync(line, CancellationToken.None);

            var savedLine = await _context.Lines.FindAsync(updatedLine.Id);

            Assert.NotNull(savedLine);
            Assert.Equal("30: Updated Station - New Destination", savedLine.Name);
        }

        [Fact]
        public async Task GetLineByIdAsync_Should_ReturnCorrectLine()
        {
            var line = new Line { Name = "50: Example - Another", CompleteDistance = "30", IsActive = true };
            await _service.CreateLineAsync(line, CancellationToken.None);

            var retrievedLine = await _service.GetLineByIdAsync(line.Id, CancellationToken.None);
            Assert.NotNull(retrievedLine);
            Assert.Equal("50: Example - Another", retrievedLine.Name);
        }

        [Fact]
        public async Task GetAllLinesAsync_Should_ReturnAllLines()
        {
            var lines = new List<Line>
        {
            new Line { Name = "1: First - Second", CompleteDistance = "10", IsActive = true },
            new Line { Name = "2: Another - End", CompleteDistance = "20", IsActive = true }
        };

            foreach (var line in lines)
            {
                await _service.CreateLineAsync(line, CancellationToken.None);
            }

            var retrievedLines = await _service.GetAllLinesAsync();
            Assert.Equal(2, retrievedLines.Count());
        }

        [Fact]
        public async Task DeleteLineAsync_Should_DeleteLine()
        {
            var line = new Line { Name = "100: DeleteMe - Gone", CompleteDistance = "10", IsActive = true };
            await _service.CreateLineAsync(line, CancellationToken.None);

            var result = await _service.DeleteLineAsync(line.Id, CancellationToken.None);
            var deletedLine = await _service.GetLineByIdAsync(line.Id, CancellationToken.None);

            Assert.True(result);
            Assert.Null(deletedLine);
        }
        [Fact]
        public async Task GetAllLinesByStationIdAsync_Should_ReturnCorrectLines()
        {
            var zone = new Zone { Id = 1, Name = "Zone 1" };
            var station = new Station { Id = 1, Name = "Main Station", Location="Mostar", GPSCode="1.27,6.66", ZoneId=1};
            _context.Stations.Add(station);
            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();

            var line1 = new Line { Name = "10: Station X - Station Y", CompleteDistance = "15", IsActive = true };
            var line2 = new Line { Name = "20: Another Station - Stop", CompleteDistance = "25", IsActive = true };

            await _service.CreateLineAsync(line1, CancellationToken.None);
            await _service.CreateLineAsync(line2, CancellationToken.None);

            _context.Routes.Add(new Route { LineId = line1.Id, StationId = station.Id , DistanceFromTheNextStation=new TimeOnly(0,15), Order=1});
            _context.Routes.Add(new Route { LineId = line2.Id, StationId = station.Id , DistanceFromTheNextStation = new TimeOnly(0, 15), Order = 1 });
            await _context.SaveChangesAsync();

            var result = await _service.GetAllLinesByStationIdAsync(station.Id, CancellationToken.None);
            Assert.Equal(2, result.Count());
        }


    }
}

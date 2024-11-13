using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Helper.Api;
using RS1_2024_25.API.Interfaces;

namespace RS1_2024_25.API.Controllers
{
    public class DriversController: MyEndpointBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers() =>
            Ok(await _driverService.GetAllDriversAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver(int id)
        {
            var driver = await _driverService.GetDriverByIdAsync(id);
            if (driver == null) return NotFound();
            return Ok(driver);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver(Driver driver)
        {
            var createdDriver = await _driverService.CreateDriverAsync(driver);
            return CreatedAtAction(nameof(GetDriver), new { id = createdDriver.Id }, createdDriver);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver(int id, Driver driver)
        {
            if (id != driver.Id) return BadRequest();
            var updatedDriver = await _driverService.UpdateDriverAsync(driver);
            return Ok(updatedDriver);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var success = await _driverService.DeleteDriverAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

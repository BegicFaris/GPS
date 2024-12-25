using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GPS.API.Services.ManagerServices;

namespace GPS.API.Controllers
{
    public class DriversController: MyControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers() =>
            Ok(await _driverService.GetAllDriversAsync());

        [Authorize]
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
            var existingDriver = await _driverService.GetDriverByIdAsync(id);

            if (driver.FirstName != null)
                existingDriver.FirstName = driver.FirstName;

            if (driver.LastName != null)
                existingDriver.LastName = driver.LastName;

            if (driver.Email != null)
                existingDriver.Email = driver.Email;

            if (driver.BirthDate.HasValue)
                existingDriver.BirthDate = driver.BirthDate;

            if (driver.RegistrationDate.HasValue)
                existingDriver.RegistrationDate = driver.RegistrationDate;

            if (driver.Image != null)
                existingDriver.Image = driver.Image;

            if (driver.Address != null)
                existingDriver.Address = driver.Address;

            if (driver.Status.HasValue)
                existingDriver.Status = driver.Status;

            if (driver.License != null)
                existingDriver.License = driver.License;

            if (driver.DriversLicenseNumber != null)
                existingDriver.DriversLicenseNumber = driver.DriversLicenseNumber;

            if (driver.HireDate != default)
                existingDriver.HireDate = driver.HireDate;

            if (driver.WorkingHoursInAWeek.HasValue)
                existingDriver.WorkingHoursInAWeek = driver.WorkingHoursInAWeek;

            existingDriver.TwoFactorEnabled = driver.TwoFactorEnabled;

            var updatedDriver = await _driverService.UpdateDriverAsync(existingDriver);
            if (updatedDriver == null)
                return NotFound();
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

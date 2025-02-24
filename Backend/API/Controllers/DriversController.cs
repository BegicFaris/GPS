using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GPS.API.Services.ManagerServices;

namespace GPS.API.Controllers
{
    public class DriversController : MyControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers(CancellationToken cancellationToken) =>
            Ok(await _driverService.GetAllDriversAsync(cancellationToken));

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver(int id, CancellationToken cancellationToken)
        {
            var driver = await _driverService.GetDriverByIdAsync(id, cancellationToken);
            if (driver == null) return NotFound();
            return Ok(driver);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<IActionResult> CreateDriver(Driver driver, CancellationToken cancellationToken)
        {
            var createdDriver = await _driverService.CreateDriverAsync(driver, cancellationToken);
            return CreatedAtAction(nameof(GetDriver), new { id = createdDriver.Id }, createdDriver);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver(int id, Driver driver, CancellationToken cancellationToken)
        {
            if (id != driver.Id) return BadRequest();
            var existingDriver = await _driverService.GetDriverByIdAsync(id, cancellationToken);

            if (existingDriver == null) return NotFound();

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

            var updatedDriver = await _driverService.UpdateDriverAsync(existingDriver, cancellationToken);
            if (updatedDriver == null)
                return NotFound();
            return Ok(updatedDriver);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _driverService.DeleteDriverAsync(id, cancellationToken);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the driver", details = ex.Message });
            }
        }
    }
}

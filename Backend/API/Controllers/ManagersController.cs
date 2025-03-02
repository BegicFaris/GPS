using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace GPS.API.Controllers
{
    public class ManagersController(IManagerService managerService) : MyControllerBase
    {
        private readonly IManagerService _managerService = managerService;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manager>>> GetAllManagers(CancellationToken cancellationToken)
        {
            var managers = await _managerService.GetAllManagersAsync(cancellationToken);
            return Ok(managers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(int id, CancellationToken cancellationToken)
        {
            var manager = await _managerService.GetManagerByIdAsync(id,cancellationToken);
            if (manager == null)
                return NotFound();
            return Ok(manager);
        }

        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPost]
        public async Task<ActionResult<Manager>> CreateManager(Manager manager, CancellationToken cancellationToken)
        {
            var createdManager = await _managerService.CreateManagerAsync(manager, cancellationToken);
            return CreatedAtAction(nameof(GetManager), new { id = createdManager.Id }, createdManager);
        }


        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(int id, Manager manager, CancellationToken cancellationToken)
        {
            if (id != manager.Id) return BadRequest();
            var existingManager = await _managerService.GetManagerByIdAsync(id, cancellationToken);
            if (existingManager == null) return NotFound($"Manager with Id:{id} not found!");


            if (manager.FirstName != null)
                existingManager.FirstName = manager.FirstName;

            if (manager.LastName != null)
                existingManager.LastName = manager.LastName;

            if (manager.Email != null)
                existingManager.Email = manager.Email;

            if (manager.BirthDate.HasValue)
                existingManager.BirthDate = manager.BirthDate;

            if (manager.RegistrationDate.HasValue)
                existingManager.RegistrationDate = manager.RegistrationDate;

            if (manager.Image != null)
                existingManager.Image = manager.Image;

            if (manager.Address != null)
                existingManager.Address = manager.Address;

            if (manager.Status.HasValue)
                existingManager.Status = manager.Status;

            if (manager.HireDate != default)
                existingManager.HireDate = manager.HireDate;

            if (manager.Department != null)
                existingManager.Department = manager.Department;

            if (manager.ManagerLevel != null)
                existingManager.ManagerLevel = manager.ManagerLevel;

            existingManager.TwoFactorEnabled = manager.TwoFactorEnabled;
            var updatedManager = await _managerService.UpdateManagerAsync(existingManager, cancellationToken);
            if (updatedManager == null)
                return NotFound();

            return Ok(updatedManager);
        }



        [Authorize(Roles = nameof(UserRole.Manager))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id, CancellationToken cancellationToken)
        {
            var result = await _managerService.DeleteManagerAsync(id, cancellationToken);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("check-email")]
        public async Task<IActionResult> ManagerCheckEmailExists([FromQuery] string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required.");
            }
            bool exists = await _managerService.ManagerEmailExistsAsync(email, cancellationToken);
            return Ok(new { exists });
        }
    }
}

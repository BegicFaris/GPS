using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace GPS.API.Controllers
{
    public class ManagersController: MyControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manager>>> GetAllManagers()
        {
            var managers = await _managerService.GetAllManagersAsync();
            return Ok(managers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(int id)
        {
            var manager = await _managerService.GetManagerByIdAsync(id);
            if (manager == null)
                return NotFound();
            return Ok(manager);
        }

        [HttpPost]
        public async Task<ActionResult<Manager>> CreateManager(Manager manager)
        {
            var createdManager = await _managerService.CreateManagerAsync(manager);
            return CreatedAtAction(nameof(GetManager), new { id = createdManager.Id }, createdManager);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(int id, Manager manager)
        {
            if (id != manager.Id) return BadRequest();
            var existingManager = await _managerService.GetManagerByIdAsync(id);
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
            var updatedManager = await _managerService.UpdateManagerAsync(existingManager);
            if (updatedManager == null)
                return NotFound();

            return Ok(updatedManager);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var result = await _managerService.DeleteManagerAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

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
            if (id != manager.Id)
                return BadRequest();

            var updatedManager = await _managerService.UpdateManagerAsync(manager);
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

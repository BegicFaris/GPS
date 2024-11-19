using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;

namespace GPS.API.Controllers
{
    public class MyAppUsersController: MyControllerBase
    {
        private readonly IMyAppUserService _userService;

        public MyAppUsersController(IMyAppUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() =>
            Ok(await _userService.GetAllUsersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(MyAppUser user)
        {
            MyAppUser createdUser;
            if (user is Driver driver)
            {
                createdUser = await _userService.CreateUserAsync(driver);
            }
            else if (user is Passenger passenger)
            {
                createdUser = await _userService.CreateUserAsync(passenger);
            }
            else if (user is Manager manager)
            {
                createdUser = await _userService.CreateUserAsync(manager);
            }
            else
            {
                return BadRequest("Invalid user type.");
            }

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, MyAppUser user)
        {
            if (id != user.Id) return BadRequest();

            var updatedUser = await _userService.UpdateUserAsync(user);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

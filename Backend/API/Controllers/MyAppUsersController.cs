using Microsoft.AspNetCore.Mvc;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GPS.API.Dtos.UserDtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using GPS.API.Dtos.TicketDtos;

namespace GPS.API.Controllers
{
    public class MyAppUsersController: MyControllerBase
    {
        private readonly IMyAppUserService _userService;

        public MyAppUsersController(IMyAppUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers() =>
            Ok(await _userService.GetAllUsersAsync());

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [Authorize]
        [HttpGet("get/{email}")]
        public async Task<IActionResult> GetUserData(string email)
        {
            var userData = await _userService.GetUserByEmailAsync(email);

            if (userData == null)
            {
                // Return a 404 if no user is found
                return NotFound(new { message = "User not found" });
            }

            // Return the user data (200 OK)
            return Ok(userData);
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

        [HttpPut("profile/{id}")]
        public async Task<IActionResult> UpdateUser(int id, MyAppUser user)
        {
            if (id != user.Id) return BadRequest();

            var updatedUser = await _userService.UpdateUserAsync(user);
            return Ok(updatedUser);
        }

        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var exists = await _userService.EmailExistsAsync(email);
            return Ok(new { exists });
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

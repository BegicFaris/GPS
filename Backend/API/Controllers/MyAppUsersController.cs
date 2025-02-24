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
    public class MyAppUsersController : MyControllerBase
    {
        private readonly IMyAppUserService _userService;

        public MyAppUsersController(IMyAppUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken) =>
            Ok(await _userService.GetAllUsersAsync(cancellationToken));


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(id, cancellationToken);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [Authorize]
        [HttpGet("get/{email}")]
        public async Task<IActionResult> GetUserData(string email, CancellationToken cancellationToken)
        {
            var userData = await _userService.GetUserByEmailAsync(email, cancellationToken);

            if (userData == null)
            {
                // Return a 404 if no user is found
                return NotFound(new { message = "User not found" });
            }

            // Return the user data (200 OK)
            return Ok(userData);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser(MyAppUser user, CancellationToken cancellationToken)
        {
            MyAppUser createdUser;
            if (user is Driver driver)
            {
                createdUser = await _userService.CreateUserAsync(driver, cancellationToken);
            }
            else if (user is Passenger passenger)
            {
                createdUser = await _userService.CreateUserAsync(passenger, cancellationToken);
            }
            else if (user is Manager manager)
            {
                createdUser = await _userService.CreateUserAsync(manager, cancellationToken);
            }
            else
            {
                return BadRequest("Invalid user type.");
            }

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        [Authorize]
        [HttpPut("profile/{id}")]
        public async Task<IActionResult> UpdateUser(int id, MyAppUser user, CancellationToken cancellationToken)
        {
            if (id != user.Id) return BadRequest();

            var updatedUser = await _userService.UpdateUserAsync(user, cancellationToken);
            return Ok(updatedUser);
        }

        [Authorize]
        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var exists = await _userService.EmailExistsAsync(email, cancellationToken);
            return Ok(new { exists });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var success = await _userService.DeleteUserAsync(id, cancellationToken);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

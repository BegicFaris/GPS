using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Dtos;
using GPS.API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GPS.API.Controllers
{
    public class AccountController(ApplicationDbContext _context, ITokenService tokenService): MyControllerBase
    {


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            if (_context.MyAppUsers.Any(u => u.Email == dto.Email))
                return BadRequest("Email is already in use.");
            using var hmac = new HMACSHA512();
            MyAppUser user;
            switch (dto.UserType.ToLower())
            {
                case "driver":
                    user = new Driver
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                        PasswordSalt = hmac.Key,
                        RegistrationDate = DateTime.Now,
                        License = "DefaultLicense", // Customize as needed
                        DriversLicenseNumber = "DefaultNumber", // Customize as needed
                        HireDate = DateTime.Now
                    };
                    break;

                case "manager":
                    user = new Manager
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                        PasswordSalt = hmac.Key,
                        RegistrationDate = DateTime.Now,
                        HireDate = DateTime.Now,
                        Department = "DefaultDepartment", // Customize as needed
                        ManagerLevel = "DefaultLevel" // Customize as needed
                    };
                    break;

                case "passenger":
                    user = new Passenger
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                        PasswordSalt = hmac.Key,
                        RegistrationDate = DateTime.Now,
                        DiscountID = 0 // Default discount, customize as needed
                    };
                    break;

                default:
                    return BadRequest("Invalid user type.");
            }

            _context.MyAppUsers.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email == dto.Email);
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            if (user == null)
                return Unauthorized("Invalid email or password");

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }
            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };
        }
    }
}

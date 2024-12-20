using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Dtos;
using GPS.API.Dtos.RegisterDtos;
using GPS.API.Interfaces;
using GPS.API.Services;
using GPS.API.Services.TokenServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace GPS.API.Controllers
{
    public class AccountController(ApplicationDbContext _context, ITokenService tokenService, ICurrentTenantService currentTenantService, IHttpClientFactory _httpClientFactory) : MyControllerBase
    {

        [HttpPost("register/driver")]
        public async Task<ActionResult<UserDto>> RegisterDriver(RegisterDriverDto dto)
        {
            if (await _context.MyAppUsers.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email is already in use.");
            using var hmac = new HMACSHA512();
            var user = new Driver
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key,
                BirthDate = dto.BirthDate,
                RegistrationDate = DateTime.Now,
                Image = dto.Image,
                Address = dto.Address,
                License = dto.License,
                DriversLicenseNumber = dto.DriversLicenseNumber,
                HireDate = dto.HireDate,
                WorkingHoursInAWeek = dto.WorkingHoursInAWeek,

            };

            _context.MyAppUsers.Add(user);
            await _context.SaveChangesAsync();
            string role = "Driver";
            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                Role = role
            };
        }

        [HttpPost("register/manager")]
        public async Task<ActionResult<UserDto>> RegisterManager(RegisterManagerDto dto)
        {
            if (await _context.MyAppUsers.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email is already in use.");
            using var hmac = new HMACSHA512();
            var user = new Manager
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key,
                BirthDate = dto.BirthDate,
                RegistrationDate = DateTime.Now,
                Image = dto.Image,
                Address = dto.Address,
                HireDate = dto.HireDate,
                Department = dto.Department,
                ManagerLevel = dto.ManagerLevel,
            };

            _context.MyAppUsers.Add(user);
            await _context.SaveChangesAsync();
            string role = "Manager";
            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                Role = role
            };
        }

        [HttpPost("register/passenger")]
        public async Task<ActionResult<UserDto>> RegisterPassenger(RegisterPassengerDto dto)
        {
            if (!await VerifyCaptcha(dto.CaptchaResponse))
            {
                return BadRequest("CAPTCHA verification failed");
            }
            if (await _context.MyAppUsers.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email is already in use.");
            var tenant= await _context.Tenants.Where(t=>t.Id==dto.TenantId).FirstOrDefaultAsync();
            currentTenantService.SetTenant(tenant.Id);
            using var hmac = new HMACSHA512();
            var user = new Passenger
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key,
                BirthDate = dto.BirthDate,
                RegistrationDate = DateTime.Now,
                Image = dto.Image,
                Address = dto.Address,
                DiscountID = dto.DiscountId,
                TenantId = dto.TenantId
            };

            _context.MyAppUsers.Add(user);
            await _context.SaveChangesAsync();
            string role = "Passenger";
            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                Role = role
            };
        }
        private async Task<bool> VerifyCaptcha(string captchaResponse)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(
                "https://www.google.com/recaptcha/api/siteverify",
                new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", "6LfSlaEqAAAAAJzr4xo8VNya1a7-JGP7PXqqgWp6"),
                    new KeyValuePair<string, string>("response", captchaResponse)
                })
            );

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonData = JObject.Parse(jsonString);
                return jsonData.Value<bool>("success");
            }

            return false;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            try
            {
            var user = await _context.MyAppUsers.FirstOrDefaultAsync(u => u.Email.Equals(dto.Email)); //dodati provjeru
                if (user == null)
                    return Unauthorized("Invalid email or password");
                using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid email or password");
                }
            }
            string role = user switch
            {
                Driver => "Driver",
                Manager => "Manager",
                Passenger => "Passenger",
                _ => "Unknown"
            };

            currentTenantService.SetTenant(user.TenantId);
            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                Role = role
            };
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred"+ex);
            }
        }
    }
}

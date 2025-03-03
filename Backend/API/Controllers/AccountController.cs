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
using System.Threading;

namespace GPS.API.Controllers
{
    public class AccountController(IDriverService driverService, IPassengerService passengerService, IManagerService managerService, IMyAppUserService myAppUserService, ITokenService tokenService, IHttpClientFactory _httpClientFactory) : MyControllerBase
    {

        [HttpPost("register/driver")]
        public async Task<ActionResult<UserDto>> RegisterDriver(RegisterDriverDto dto, CancellationToken cancellationToken)
        {

            if (await myAppUserService.EmailExistsAsync(dto.Email, cancellationToken))
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
            await driverService.CreateDriverAsync(user, cancellationToken);
            string role = "Driver";
            var token = await tokenService.CreateToken(user, cancellationToken);
            return new UserDto
            {
                Email = user.Email,
                Token = token,
                Role = role
            };
        }

        [HttpPost("register/manager")]
        public async Task<ActionResult<UserDto>> RegisterManager(RegisterManagerDto dto, CancellationToken cancellationToken)
        {
            if (await myAppUserService.EmailExistsAsync(dto.Email, cancellationToken))
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

            await managerService.CreateManagerAsync(user, cancellationToken);
            string role = "Manager";
            var token = await tokenService.CreateToken(user, cancellationToken);
            return new UserDto
            {
                Email = user.Email,
                Token = token,
                Role = role
            };
        }

        [HttpPost("register/passenger")]
        public async Task<ActionResult<UserDto>> RegisterPassenger(RegisterPassengerDto dto, CancellationToken cancellationToken)
        {
            //if (!await VerifyCaptcha(dto.CaptchaResponse))
            //{
            //    return BadRequest("CAPTCHA verification failed");
            //}


            if (await myAppUserService.EmailExistsAsync(dto.Email, cancellationToken))
                return BadRequest("Email is already in use.");
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
            };

            await passengerService.CreatePassengerAsync(user, cancellationToken);
            string role = "Passenger";
            var token = await tokenService.CreateToken(user, cancellationToken);
            return new UserDto
            {
                Email = user.Email,
                Token = token,
                Role = role
            };
        }
        private async Task<bool> VerifyCaptcha(string captchaResponse, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(
                "https://www.google.com/recaptcha/api/siteverify",
                new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", "6LfSlaEqAAAAAJzr4xo8VNya1a7-JGP7PXqqgWp6"),
                    new KeyValuePair<string, string>("response", captchaResponse)
                }),
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
                var jsonData = JObject.Parse(jsonString);
                return jsonData.Value<bool>("success");
            }

            return false;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await myAppUserService.GetUserByEmailAsync(dto.Email, cancellationToken);
                if (user == null)
                    return BadRequest("Invalid email or password");

                if (user.PasswordSalt == null || user.PasswordHash==null)
                {
                    return BadRequest("Invalid email or password");
                }

                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {            
                    if (computedHash[i] != user.PasswordHash[i])
                    {
                        return BadRequest("Invalid email or password");
                    }
                }
                string role = user switch
                {
                    Driver => "Driver",
                    Manager => "Manager",
                    Passenger => "Passenger",
                    _ => "Unknown"
                };

                var token = await tokenService.CreateToken(user, cancellationToken);
                return new UserDto
                {
                    Email = user.Email,
                    Token = token,
                    Role = role,
                    Id = user.Id
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred" + ex);
            }
        }
    }
}

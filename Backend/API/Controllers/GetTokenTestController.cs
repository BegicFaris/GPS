using GPS.API.Data.DbContexts;
using GPS.API.Dtos;
using GPS.API.Interfaces;
using GPS.API.Services.TokenServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GPS.API.Controllers
{
    public class AAGetTokenTestController(ApplicationDbContext _context, ITokenService tokenService) : MyControllerBase
    {
        [HttpPost("GetToken")]
        public async Task<ActionResult<UserDto>> Login(string? tenantId, ICurrentTenantService currentTenantService)
        {
            var user = await _context.MyAppUsers.FirstOrDefaultAsync();

            if (String.IsNullOrEmpty(tenantId))
                tenantId = "mostar";

            user.TenantId = tenantId;

            currentTenantService.SetTenant(user.TenantId);
            return new UserDto
            {
                Email = user.Email,
                Token = "Bearer " + tokenService.CreateToken(user)
            };


        }
    }
}

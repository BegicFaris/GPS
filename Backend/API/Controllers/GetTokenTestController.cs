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
        public async Task<ActionResult<UserDto>> Login(string? tenantId, ICurrentTenantService currentTenantService, CancellationToken cancellationToken)
        {
          await  currentTenantService.SetTenant(tenantId==null?"mostar":tenantId,cancellationToken);
            var user = await _context.MyAppUsers.FirstOrDefaultAsync();
            if (user == null) { return NotFound(); }
            var token = "Bearer " + await tokenService.CreateToken(user,cancellationToken);
            return new UserDto
            {
                Email = user.Email,
                Token = token,
                Role = "Manager"
            };


        }
    }
}

using GPS.API.Dtos;
using GPS.API.Interfaces;
using GPS.API.Services.TokenServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Controllers
{
    public class TokenController(ITokenService tokenService) : MyControllerBase
    {

        [HttpGet("{tenantId}")]
        public async Task<ActionResult<string>> GetToken(string tenantId)
        {
            var token = tokenService.CreateTenantOnlyToket(tenantId);
            return Ok(new { token }); 
        }
    }
}
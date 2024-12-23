using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.TenantServices;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GPS.API.Services.TokenServices
{
    public class TokenService (IConfiguration config, ICurrentTenantService currentTenantService) : ITokenService
    {
        public string CreateToken(MyAppUser user)
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot acces tokenKey from appsettings");
            if (tokenKey.Length < 64) throw new Exception("Your tokeKey need to be longer");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            if (currentTenantService.TenantId == null)
            {
                currentTenantService.SetTenant(user.TenantId);
            }

            var claims = new List<Claim> 
            { 
                new(ClaimTypes.NameIdentifier, user.Email),
                new Claim("TenantId", currentTenantService.TenantId)
            };

            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateTenantOnlyToket(string tenantId)
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot acces tokenKey from appsettings");
            if (tokenKey.Length < 64) throw new Exception("Your tokeKey need to be longer");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim>
            {
                new Claim("TenantId",tenantId)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);



        }
    }
}

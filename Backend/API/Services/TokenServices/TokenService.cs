using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.TenantServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GPS.API.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly ICurrentTenantService _currentTenantService;

        public TokenService(IConfiguration config, ICurrentTenantService currentTenantService)
        {
            _config = config;
            _currentTenantService = currentTenantService;
        }

        public async Task<string> CreateToken(MyAppUser user, CancellationToken cancellationToken)
        {

            cancellationToken.ThrowIfCancellationRequested();

            var tokenKey = _config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
            if (tokenKey.Length < 64) throw new Exception("Your tokenKey needs to be longer");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            if (_currentTenantService.TenantId == null)
            {
                if (user == null || user.TenantId==null)
                {
                    throw new Exception("Cannot set tenant from user");
                }
               await  _currentTenantService.SetTenant(user.TenantId, cancellationToken);
            }

            var role = GetRoleFromUser(user);

            if (_currentTenantService.TenantId == null) {
                throw new Exception("Cannot get tenant id from service");
            }
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Email),
                new Claim("TenantId", _currentTenantService.TenantId),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
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

        private UserRole GetRoleFromUser(MyAppUser user)
        {
            if (user is Manager)
                return UserRole.Manager;
            else if (user is Driver)
                return UserRole.Driver;
            else if (user is Passenger)
                return UserRole.Passenger;
            else
                return UserRole.None;
        }
    }

}

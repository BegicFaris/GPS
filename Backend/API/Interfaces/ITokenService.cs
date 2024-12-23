using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(MyAppUser user);
        public string CreateTenantOnlyToket(string tenantId);
    }
}

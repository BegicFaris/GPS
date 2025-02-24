using GPS.API.Data.Models;

namespace GPS.API.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(MyAppUser user, CancellationToken cancellationToken);
    }
}

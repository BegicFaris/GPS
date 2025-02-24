namespace GPS.API.Interfaces
{
    public interface ITwoFactorAuthService
    {
        Task<bool> IsUsingTwoFactorAsync(string email, CancellationToken cancellationToken);
        Task GenerateTwoFactorCodeAsync(string email, CancellationToken cancellationToken);
        Task<bool> VerifyTwoFactorCodeAsync(string email, string code, CancellationToken cancellationToken);
    }
}

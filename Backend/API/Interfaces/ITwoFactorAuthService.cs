namespace GPS.API.Interfaces
{
    public interface ITwoFactorAuthService
    {
        Task GenerateTwoFactorCode(string email);
        Task<bool> VerifyTwoFactorCode(string email, string code);
        Task<bool> IsUsingTwoFactor(string email);
    }
}

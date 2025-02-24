namespace GPS.API.Interfaces
{
    public interface IPasswordResetService
    {
        Task GenerateResetCode(string email, CancellationToken cancellationToken);
        Task<bool> VerifyResetCode(string email, string code, CancellationToken cancellationToken);
        Task<bool> ResetPassword(string email, string code, string newPassword, CancellationToken cancellationToken);
    }
}

namespace GPS.API.Interfaces
{
    public interface IPasswordResetService
    {
            Task GenerateResetCode(string email);
            Task<bool> VerifyResetCode(string email, string code);
            Task<bool> ResetPassword(string email, string code, string newPassword);
    }
}

namespace GPS.API.Dtos.PasswordResetDtos
{
    public class ResetPasswordRequestDto
    {
        public required string Email { get; set; }
        public required string Code { get; set; }
        public required string NewPassword { get; set; }
    }
}

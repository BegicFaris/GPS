namespace GPS.API.Dtos.PasswordResetDtos
{
    public class VerifyCodeRequestDto
    {
        public required string Email { get; set; }
        public required string Code { get; set; }
    }
}

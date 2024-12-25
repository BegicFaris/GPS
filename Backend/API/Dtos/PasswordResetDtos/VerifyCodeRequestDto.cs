namespace GPS.API.Dtos.PasswordResetDtos
{
    public class VerifyCodeRequestDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}

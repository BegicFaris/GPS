namespace GPS.API.Dtos.TwoFactorDtos
{
    public class VerifyTwoFactorCodeRequestDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}

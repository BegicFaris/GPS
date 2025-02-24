namespace GPS.API.Dtos.RegisterDtos
{
    public class RegisterPassengerDto: RegisterDto
    {
        public int? DiscountId { get; set; }
        public required string CaptchaResponse { get; set; }

    }
}

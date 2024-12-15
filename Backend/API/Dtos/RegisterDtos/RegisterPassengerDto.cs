namespace GPS.API.Dtos.RegisterDtos
{
    public class RegisterPassengerDto: RegisterDto
    {
        public int? DiscountId { get; set; }
        public string TenantId { get; set; }
    }
}

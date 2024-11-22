namespace GPS.API.Dtos.RegisterDtos
{
    public class RegisterDriverDto: RegisterDto
    {
        public required string License { get; set; }
        public required string DriversLicenseNumber { get; set; }
        public required DateTime HireDate { get; set; }
        public float? WorkingHoursInAWeek { get; set; }
    }
}

using GPS.API.Dtos.TicketDtos;

namespace GPS.API.Dtos.UserDtos
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? Address { get; set; }
        public bool? Status { get; set; }
        public string UserType { get; set; }
        public bool TwoFactorEnabled { get; set; }
        // Additional properties based on user type
        public DateOnly? HireDate { get; set; }
        public string? Department { get; set; }
        public string? ManagerLevel { get; set; }
        public string? License { get; set; }
        public string? DriversLicenseNumber { get; set; }
        public float? WorkingHoursInAWeek { get; set; }
        public int? DiscountID { get; set; }
    }
}

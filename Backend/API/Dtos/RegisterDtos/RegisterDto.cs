using GPS.API.Interfaces;

namespace GPS.API.Dtos.RegisterDtos
{
    public abstract class RegisterDto 
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string  Email { get; set; }
        public required string Password { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public byte[]? Image { get; set; }
        public string? Address { get; set; }
    }
}

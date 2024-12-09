namespace GPS.API.Dtos
{
    public class UserDto
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string Role { get; set; }
    }
}

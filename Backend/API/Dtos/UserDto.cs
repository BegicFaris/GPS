namespace GPS.API.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public required string Role { get; set; }
    }
}

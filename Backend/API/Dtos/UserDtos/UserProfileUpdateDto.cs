namespace GPS.API.Dtos.UserDtos
{
    public class UserProfileUpdateDto
    {
        
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public DateOnly? BirthDate { get; set; }
            public string? Address { get; set; }
            public bool TwoFactorEnabled { get; set; }
            // Add other editable fields here

    }
}

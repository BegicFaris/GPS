using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    [Table("MyAppUsers")]
    public abstract class MyAppUser  : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public byte[]? Image { get; set; }
        public string? Address { get; set; }
        public bool? Status { get; set; }
        public string? TenantId { get; set; }
    }

}

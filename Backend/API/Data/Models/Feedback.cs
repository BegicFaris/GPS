using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;
namespace GPS.API.Data.Models
{
    public class Feedback : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public required int UserId { get; set; }
        public MyAppUser? User { get; set; }
        public string? Comment { get; set; }
        public required float Rating { get; set; }
        public required DateTime Date { get; set; }
        public byte[]? Picture { get; set; }
        public string? TenantId { get; set; }
    }
}

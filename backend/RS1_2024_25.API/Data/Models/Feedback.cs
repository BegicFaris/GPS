using RS1_2024_25.API.Data.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Feedback
    {
        [Key]
        public required int Id { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public required int UserId { get; set; }
        public MyAppUser User { get; set; }
        public string? Comment { get; set; }
        public required float Rating { get; set; }
        public required DateTime Date { get; set; }
        public byte[]? Picture { get; set; }
    }
}

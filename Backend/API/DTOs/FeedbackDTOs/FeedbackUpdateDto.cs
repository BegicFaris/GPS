using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.FeedbackDTOs
{
    public class FeedbackUpdateDto
    {
        public required int Id { get; set; }
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public float? Rating { get; set; }
        public DateTime? Date { get; set; }
        public byte[]? Picture { get; set; }
    }
}

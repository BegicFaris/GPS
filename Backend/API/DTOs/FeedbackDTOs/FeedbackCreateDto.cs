using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.FeedbackDTOs
{
    public class FeedbackCreateDto
    {
        public required int UserId { get; set; }
        public string? Comment { get; set; }
        public required float Rating { get; set; }
        public required DateTime Date { get; set; }
        public byte[]? Picture { get; set; }
    }
}

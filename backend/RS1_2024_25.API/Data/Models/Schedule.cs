using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Line))]
        public int LineId {  get; set; }
        public Line Line { get; set; }
        public TimeOnly DepartureTime { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Bus))]
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        [ForeignKey(nameof(Driver))]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly ShiftStartingTime { get; set; }
        public TimeOnly ShiftEndingTime { get; set; }
    }
}

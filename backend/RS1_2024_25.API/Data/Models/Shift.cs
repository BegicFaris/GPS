using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Shift
    {
        [Key]
        public required int Id { get; set; }
        [ForeignKey(nameof(Bus))]
        public required int BusId { get; set; }
        public Bus? Bus { get; set; }
        [ForeignKey(nameof(Driver))]
        public required int DriverId { get; set; }
        public Driver? Driver { get; set; }
        public required DateOnly ShiftDate { get; set; }
        public required TimeOnly ShiftStartingTime { get; set; }
        public required TimeOnly ShiftEndingTime { get; set; }
    }
}

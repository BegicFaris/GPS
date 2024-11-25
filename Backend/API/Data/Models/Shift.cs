using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class Shift:IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Bus))]
        public required int BusId { get; set; }
        public Bus? Bus { get; set; }
        [ForeignKey(nameof(Driver))]
        public required int DriverId { get; set; }
        public Driver? Driver { get; set; }
        public required DateOnly ShiftDate { get; set; }
        public required TimeOnly ShiftStartingTime { get; set; }
        public required TimeOnly ShiftEndingTime { get; set; }
        public string? TenantId { get; set; }
    }
}

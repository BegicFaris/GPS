using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class ShiftDetail : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Shift))]
        public required int ShiftId { get; set; }
        public Shift? Shift { get; set; }

        [ForeignKey(nameof(Line))]
        public required int LineId { get; set; }
        public Line? Line { get; set; }
        public required TimeOnly ShiftDetailStartingTime { get; set; }
        public required TimeOnly ShiftDetailEndingTime { get; set; }
        public string? TenantId { get; set; }
    }
}

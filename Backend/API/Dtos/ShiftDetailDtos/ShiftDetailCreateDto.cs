using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Dtos.ShiftDetailDtos
{
    public class ShiftDetailCreateDto
    {
        public required int ShiftId { get; set; }
        public required int LineId { get; set; }
        public required string ShiftDetailStartingTime { get; set; }
        public required string ShiftDetailEndingTime { get; set; }
    }
}

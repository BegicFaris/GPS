using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.LineDtos
{
    public class LineCreateDto
    {
        public required string Name { get; set; }
        public required int StartingStationID { get; set; }
        public required int EndingStationID { get; set; }
        public required string CompleteDistance { get; set; }
        public required bool IsActive { get; set; }
    }
}

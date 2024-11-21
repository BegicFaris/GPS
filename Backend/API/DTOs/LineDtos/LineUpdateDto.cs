using GPS.API.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Dtos.LineDtos
{
    public class LineUpdateDto
    {
        public  required int Id { get; set; }
        public  string? Name { get; set; }
        public  int? StartingStationID { get; set; }
        public  int? EndingStationID { get; set; }
        public  string? CompleteDistance { get; set; }
        public  bool? IsActive { get; set; }
    }
}

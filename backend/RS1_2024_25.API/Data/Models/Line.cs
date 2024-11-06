using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Line
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(Station))]
        public int StartingStationID { get; set; }
        public Station StartingStation { get; set; }
        [ForeignKey(nameof(Station))]
        public int EndingStationID { get; set; }
        public Station EndingStation { get; set; }
        public string CompleteDistance { get; set; }
        public bool IsActive { get; set; }
    }
}

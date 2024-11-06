using System.ComponentModel.DataAnnotations;

namespace RS1_2024_25.API.Data.Models
{
    public class Line
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartingStation { get; set; }
        public string EndingStation { get; set; }
        public string CompleteDistance { get; set; }
        public bool IsCompleted { get; set; }
    }
}

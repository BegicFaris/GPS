using GPS.API.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Data.Models
{
    public class FavoriteLine : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey(nameof(MyAppUser))]
        public required int UserId { get; set; }
        public MyAppUser? User { get; set; }


        [ForeignKey(nameof(Line))]
        public required int LineId { get; set; }
        public Line? Line { get; set; }

        public string TenantId { get; set; }
    }
}

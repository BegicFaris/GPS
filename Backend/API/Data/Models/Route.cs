﻿using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class Route : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Line))]
        public required int LineId { get; set; }
        public Line? Line { get; set; }
        [ForeignKey(nameof(Station))]
        public required int StationId { get; set; }
        public Station? Station { get; set; }
        public required TimeOnly DistanceFromTheNextStation { get; set; }
        public required int Order { get; set; }
        public string? TenantId { get; set; }
    }
}

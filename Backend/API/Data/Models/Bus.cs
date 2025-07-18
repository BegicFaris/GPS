﻿using GPS.API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class Bus : IMustHaveTenant,ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        public required string RegistrationNumber { get; set; }
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public required string Capacity { get; set; }
        public required string ManufactureYear { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? TenantId { get; set; }


    }
}

﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GPS.API.Data.Models
{
    public class SystemActionLog
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public int UserId { get; set; }
        public MyAppUser? User { get; set; }
        public string? QueryPath { get; set; }
        public string? PostData { get; set; }
        public DateTime TimeOfAction { get; set; }
        public string? IpAdress { get; set; }
        public string? ExceptionMessage { get; set; }
        public bool IsException { get; set; }
    }
}

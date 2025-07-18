﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GPS.API.Data.Models
{
    [Table("Managers")]
    public class Manager : MyAppUser
    {
        public required DateOnly HireDate { get; set; }
        public required string Department { get; set; }
        public required string ManagerLevel { get; set; }
    }
}

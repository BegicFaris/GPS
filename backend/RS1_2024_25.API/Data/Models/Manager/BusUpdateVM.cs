namespace RS1_2024_25.API.Models.Manager
{
    public class BusUpdateVM
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Capacity { get; set; }
        public string ManufactureYear { get; set; }
    }
}
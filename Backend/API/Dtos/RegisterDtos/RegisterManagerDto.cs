namespace GPS.API.Dtos.RegisterDtos
{
    public class RegisterManagerDto: RegisterDto
    {
        public required DateOnly HireDate { get; set; }
        public required string Department {  get; set; }
        public required string ManagerLevel { get; set; }
    }
}

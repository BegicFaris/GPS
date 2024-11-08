namespace RS1_2024_25.API.Services.TenantServices
{
    public interface ICurrentTenantService
    {
        string? TenantId { get; set; }
        public Task<bool> SetTenant(string tenant);

    }
}
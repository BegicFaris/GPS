namespace GPS.API.Interfaces
{

    //The interface used to implement the tenant service
    public interface ICurrentTenantService
    {
        public string? TenantId { get; set; }
        public Task<bool> SetTenant(string tenant);

    }
}
namespace GPS.API.Interfaces
{

    //The interface used to implement the tenant service
    public interface ICurrentTenantService
    {
        string? TenantId { get; set; }
        Task<bool> SetTenant(string tenantId, CancellationToken cancellationToken);

    }
}
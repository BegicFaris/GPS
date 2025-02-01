namespace GPS.API.Interfaces
{

    //The interface used to implement the tenant service
    public interface ICurrentTenantService
    {
        public string? TenantId { get; set; }


        /// <summary>
        /// This method sets the value of the current tenant
        /// </summary>
        /// <param name="tenant">The id of the tenant it needs to set.</param>
        /// <returns>True if it sets, false if it does bot.</returns>
        public Task<bool> SetTenant(string tenant);

    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Helper.Api;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static RS1_2024_25.API.Endpoints.TenantEndpoint.GetZonesEndpoint;

//Test code to see if multitenancy works, I used the class with the least ammount of attributes to make it easy on myself



namespace RS1_2024_25.API.Endpoints.TenantEndpoint
{
    public class GetZonesEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
        .WithRequest<GetZoneRequest>
        .WithResult<List<Zone>> // Return type is now List<ZONE>
    {
        [HttpPost]
        public override async Task<List<Zone>> HandleAsync([FromBody] GetZoneRequest request, CancellationToken cancellationToken = default)
        {
            // Step 1: Get tenantID from the header
            var tenantID = Request.Headers["tenantID"].ToString();
            if (string.IsNullOrEmpty(tenantID))
            {
                // Handle the case where tenantID is not provided
                throw new ArgumentException("Tenant ID header is missing.");
            }

            // Step 2: Logic to fetch multiple zones based on tenantID
            var zones = db.Zones.ToList();

            if (zones == null || zones.Count == 0)
            {
                // Handle case where no zones are found for the tenantID
                throw new KeyNotFoundException("No zones found for the provided tenant ID.");
            }

            // Step 3: Return the list of zones
            return zones;
        }

        public class GetZoneRequest
        {
            // Any additional parameters could go here, but tenantID is passed via header
        }
    }
}
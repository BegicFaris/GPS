using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data.Models;


namespace RS1_2024_25.API.Data
{

    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {



        }
        public DbSet<Tenant> Tenants { get; set; }
    }
}
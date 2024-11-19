using GPS.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GPS.API.Data.DbContexts
{
    //A Dbcontext that is needed for multitenantcy to avoide circular reference, it only needs to have the tenant DBset and nothing else

    public class TenantDbContext : DbContext
    {
        //Since we have 2 Db contexts we need to specify which one we are using with the options builder
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {



        }
        public DbSet<Tenant> Tenants { get; set; }
    }
}

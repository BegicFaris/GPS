using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Services.TenantServices;
using Route = RS1_2024_25.API.Data.Models.Route;

namespace RS1_2024_25.API.Data
{

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService) : DbContext(options)
    {
        //Implemenitg Multitenatcy into the AppDbContext
        public string CurrentTenantID = currentTenantService.TenantId;
        private readonly DbContextOptions<ApplicationDbContext> options = options;
        private readonly ICurrentTenantService currentTenantService = currentTenantService;
        public DbSet<MyAppUser> MyAppUsers { get; set; }
        public DbSet<MyAuthenticationToken> MyAuthenticationTokens { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<PassengerCreditCard> PassengerCreditCards { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Ovo treba implementirati za svaku klasu koja koristi tenant
            //modelBuilder.Entity<NekaKlasa>().HasQueryFilter(a => a.TenantId == CurrentTenantID);
            modelBuilder.Entity<Zone>().HasQueryFilter(a => a.TenantId == CurrentTenantID);

            //Adding test data to the DB
            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { Id = "tenant1", Name = "Tenant1" },
                new Tenant { Id = "tenant2", Name = "Tenant2" }
                );

            modelBuilder.Entity<Zone>().HasData(

                  new Zone { Id = 1, Name = "Zone one", Price = 1.5M, TenantId = "tenant1" },
                  new Zone { Id = 2, Name = "Zone two", Price = 2.1M, TenantId = "tenant1" },
                  new Zone { Id = 3, Name = "Zone three", Price = 2.7M, TenantId = "tenant2" }
                );

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }




            // opcija kod nasljeđivanja
            // modelBuilder.Entity<NekaBaznaKlasa>().UseTpcMappingStrategy();
        }





        //every time we save changes we also save the Id of the current tenant for every entity that implements IMustHaveTenant
        public override int SaveChanges()
        {

            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantID;
                        break;

                }

            }



            var result = base.SaveChanges();
            return result;
        }

    }
}

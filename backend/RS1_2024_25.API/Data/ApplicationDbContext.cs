using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using Route = RS1_2024_25.API.Data.Models.Route;

namespace RS1_2024_25.API.Data
{
    public class ApplicationDbContext(
        DbContextOptions options) : DbContext(options)
    {
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            // opcija kod nasljeđivanja
            // modelBuilder.Entity<NekaBaznaKlasa>().UseTpcMappingStrategy();
        }
    }
}

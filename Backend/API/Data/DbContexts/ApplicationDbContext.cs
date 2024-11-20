using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using System.Text;

using Microsoft.EntityFrameworkCore.Diagnostics;


namespace GPS.API.Data.DbContexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService) : DbContext(options)
    {
        //Implemenitg Multitenatcy into the AppDbContext
        public string CurrentTenantID = currentTenantService.TenantId;
        private readonly DbContextOptions<ApplicationDbContext> options = options;
        private readonly ICurrentTenantService currentTenantService = currentTenantService;


        public DbSet<MyAppUser> MyAppUsers { get; set; }
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
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<SystemActionLog> SystemActionsLog { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.ConfigureWarnings(warnings =>
        //     warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyAppUser>().ToTable("MyAppUsers");
            modelBuilder.Entity<Passenger>().ToTable("Passengers");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<Manager>().ToTable("Managers");
            //Ovo treba implementirati za svaku klasu koja koristi tenant
            //modelBuilder.Entity<NekaKlasa>().HasQueryFilter(a => a.TenantId == CurrentTenantID);


            //Adding test data to the DB
            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { Id = "tenant1", Name = "Tenant1" },
                new Tenant { Id = "tenant2", Name = "Tenant2" }
                );

            modelBuilder.Entity<Zone>().HasData(

                new Zone { Id = 1, Name = "Zone one", Price = 1.5M },
                new Zone { Id = 2, Name = "Zone two", Price = 2.1M },
                new Zone { Id = 3, Name = "Zone three", Price = 2.7M }
                );
            modelBuilder.Entity<Bus>().HasData(
                new Bus { Id = 1, Capacity = "20", Manufacturer = "MAN", ManufactureYear = "2002", Model = "MK2", RegistrationNumber = "12345678" },
                new Bus { Id = 2, Capacity = "21", Manufacturer = "MAN", ManufactureYear = "2003", Model = "MK3", RegistrationNumber = "asd5678" }
                );
            modelBuilder.Entity<CreditCard>().HasData(
                new CreditCard { Id = 1, CardName = "Faris", CardNumber = "1234 5679 8791", CCV = 123, ExpirationDate = "7/28" },
                new CreditCard { Id = 2, CardName = "Nedim", CardNumber = "2432 4454 4545", CCV = 254, ExpirationDate = "7/28" }
                );
            modelBuilder.Entity<Discount>().HasData(
                new Discount { Id = 1, DiscountName = "Student", DiscountValue = 0.15f },
                new Discount { Id = 2, DiscountName = "Penzioner", DiscountValue = 0.17f }
                );
            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, DriversLicenseNumber = "a1435affaa", PasswordHash= Encoding.UTF8.GetBytes("e1f2142aec055d334a048a52f51c204d31889a2b7305f5997e37d7e5395194fec9bb2383e4f66efa67bdefd3e0384ecc699761c05b19e965b151af8a4dd4f5fd"), Email = "mail@mail.com", FirstName = "Adi", HireDate = DateTime.Now, LastName = "Gosto", License = "1123123" },
                new Driver { Id = 2, DriversLicenseNumber = "adasd43aa", PasswordHash = Encoding.UTF8.GetBytes("93c8bbc4b96d326cd19288318286b07fa6933be6b74d4ad6f1e861f3b580fb909f0d9001dd0a3e790116b6f885372b1ba005f50e0bf5a9051647a6104518caa4"),Email = "mail@mail2.com", FirstName = "Nedim", HireDate = DateTime.Now, LastName = "Jugo", License = "11jdfghsdjg23" }
                );
            modelBuilder.Entity<Manager>().HasData(
                new Manager { Id = 3, Email = "mail@mail.com", PasswordHash = Encoding.UTF8.GetBytes("e1f2142aec055d334a048a52f51c204d31889a2b7305f5997e37d7e5395194fec9bb2383e4f66efa67bdefd3e0384ecc699761c05b19e965b151af8a4dd4f5fd"),FirstName = "Adil", HireDate = DateTime.Now, LastName = "Joldic", Department = "HR", ManagerLevel = "1" },
                new Manager { Id = 4, Email = "mail@mail2.com", PasswordHash = Encoding.UTF8.GetBytes("93c8bbc4b96d326cd19288318286b07fa6933be6b74d4ad6f1e861f3b580fb909f0d9001dd0a3e790116b6f885372b1ba005f50e0bf5a9051647a6104518caa4"),FirstName = "Denis", HireDate = DateTime.Now, LastName = "Music", Department = "IT", ManagerLevel = "2" }

                );
            modelBuilder.Entity<Passenger>().HasData(
                new Passenger { Id = 5, Email = "mail@mail.com", FirstName = "Adil", PasswordHash = Encoding.UTF8.GetBytes("e1f2142aec055d334a048a52f51c204d31889a2b7305f5997e37d7e5395194fec9bb2383e4f66efa67bdefd3e0384ecc699761c05b19e965b151af8a4dd4f5fd"), LastName = "Joldic" },
                new Passenger { Id = 6, Email = "mail@mail2.com", FirstName = "Denis", PasswordHash = Encoding.UTF8.GetBytes("93c8bbc4b96d326cd19288318286b07fa6933be6b74d4ad6f1e861f3b580fb909f0d9001dd0a3e790116b6f885372b1ba005f50e0bf5a9051647a6104518caa4"), LastName = "Music" }
                );
            modelBuilder.Entity<Feedback>().HasData(
               new Feedback { Id = 1, Date = new DateTime(2024, 1, 1), UserId = 5, Rating = 5 },
                new Feedback { Id = 2, Date = new DateTime(2024, 1, 1), UserId = 6, Rating = 3 }
                );
            modelBuilder.Entity<Station>().HasData(
                new Station { Id = 1, GPSCode = "6.6.6", Location = "Bafo", Name = "Bafo", ZoneId = 1 },
                new Station { Id = 2, GPSCode = "13123", Location = "Sutina", Name = "Sutina1", ZoneId = 2 }
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

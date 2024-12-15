using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using System.Text;

using Microsoft.EntityFrameworkCore.Diagnostics;
using GPS.API.Services.TenantServices;
using System.Security.Cryptography;


namespace GPS.API.Data.DbContexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService) : DbContext(options)
    {
        //Implemenitg Multitenatcy into the AppDbContext
        private readonly DbContextOptions<ApplicationDbContext> options = options;
        public string CurrentTenantID => currentTenantService.TenantId ?? throw new Exception("TenantId not found.");
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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings =>
             warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyAppUser>().ToTable("MyAppUsers");
            modelBuilder.Entity<Passenger>().ToTable("Passengers");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<Manager>().ToTable("Managers");
            //Ovo treba implementirati za svaku klasu koja koristi tenant
            //modelBuilder.Entity<NekaKlasa>().HasQueryFilter(a => a.TenantId == CurrentTenantID);
            //modelBuilder.Entity<MyAppUser>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Bus>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<CreditCard>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Discount>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Feedback>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Line>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Notification>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<NotificationType>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<PassengerCreditCard>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Models.Route>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Schedule>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Shift>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Station>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Ticket>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<TicketType>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);
            modelBuilder.Entity<Zone>().HasQueryFilter(x => x.TenantId == currentTenantService.TenantId);


            //Adding test data to the DB
            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { Id = "mostar", Name = "Mostar" },
                new Tenant { Id = "sarajevo", Name = "Sarajevo" },
                new Tenant { Id = "bugojno", Name = "Bugojno" }
                );
            modelBuilder.Entity<Zone>().HasData(
                new Zone { Id = 1, Name = "Zone one", Price = 1.5M },
                new Zone { Id = 2, Name = "Zone two", Price = 2.1M },
                new Zone { Id = 3, Name = "Zone three", Price = 2.7M }
                );
            modelBuilder.Entity<Bus>().HasData(
                new Bus { Id = 1, Capacity = "20", Manufacturer = "MAN", ManufactureYear = "2002", Model = "MK2", RegistrationNumber = "12345678", TenantId = "mostar" },
                new Bus { Id = 2, Capacity = "21", Manufacturer = "MAN", ManufactureYear = "2003", Model = "MK3", RegistrationNumber = "asd5678", TenantId = "mostar" }
                );
            modelBuilder.Entity<CreditCard>().HasData(
                new CreditCard { Id = 1, CardName = "Faris", CardNumber = "1234 5679 8791", CCV = 123, ExpirationDate = "7/28", TenantId = "mostar" },
                new CreditCard { Id = 2, CardName = "Nedim", CardNumber = "2432 4454 4545", CCV = 254, ExpirationDate = "7/28", TenantId = "mostar" }
                );
            modelBuilder.Entity<Discount>().HasData(
                new Discount { Id = 1, DiscountName = "Student", DiscountValue = 0.15f, TenantId = "mostar" },
                new Discount { Id = 2, DiscountName = "Penzioner", DiscountValue = 0.17f, TenantId = "mostar" }
                );

            using var hmac = new HMACSHA512();
            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, DriversLicenseNumber = "a1435affaa", PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123")), PasswordSalt = hmac.Key, Email = "1", FirstName = "Adi", HireDate = new DateOnly(2024, 12, 1), LastName = "Gosto", License = "1123123", TenantId = "mostar" },
                new Driver { Id = 2, DriversLicenseNumber = "adasd43aa", PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123")), PasswordSalt = hmac.Key, Email = "2", FirstName = "Nedim", HireDate = new DateOnly(2024, 12, 1), LastName = "Jugo", License = "11jdfghsdjg23", TenantId = "mostar" }
                );
            modelBuilder.Entity<Manager>().HasData(
                new Manager { Id = 3, Email = "3", PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123")), PasswordSalt = hmac.Key, FirstName = "Adil", HireDate = new DateOnly(2024, 12, 1), LastName = "Joldic", Department = "HR", ManagerLevel = "1", TenantId = "mostar" },
                new Manager { Id = 4, Email = "4", PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123")), PasswordSalt = hmac.Key, FirstName = "Denis", HireDate = new DateOnly(2024, 12, 1), LastName = "Music", Department = "IT", ManagerLevel = "2", TenantId = "mostar" }

                );
            modelBuilder.Entity<Passenger>().HasData(
                new Passenger { Id = 5, Email = "5", FirstName = "Adil", PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123")), PasswordSalt = hmac.Key, LastName = "Joldic", TenantId = "mostar" },
                new Passenger { Id = 6, Email = "6", FirstName = "Denis", PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123")), PasswordSalt = hmac.Key, LastName = "Music", TenantId = "mostar" }
                );
            modelBuilder.Entity<Feedback>().HasData(
               new Feedback { Id = 1, Date = new DateTime(2024, 1, 1), UserId = 5, Rating = 5, TenantId = "mostar" },
                new Feedback { Id = 2, Date = new DateTime(2024, 1, 1), UserId = 6, Rating = 3, TenantId = "mostar" }
                );
            modelBuilder.Entity<Station>().HasData(
                new Station { Id = 1, GPSCode = "6.6.6", Location = "Bafo", Name = "Bafo", ZoneId = 1, TenantId = "mostar" },
                new Station { Id = 2, GPSCode = "13123", Location = "Sutina", Name = "Sutina1", ZoneId = 2, TenantId = "mostar" }
                );
            modelBuilder.Entity<Line>().HasData(
                new Line { Id = 1, CompleteDistance = "10", IsActive = true, EndingStationId = 1, StartingStationId = 2, Name = "21", TenantId = "mostar" },
                 new Line { Id = 2, CompleteDistance = "10", IsActive = true, EndingStationId = 1, StartingStationId = 2, Name = "21", TenantId = "mostar" }
                );
            modelBuilder.Entity<NotificationType>().HasData(
                new NotificationType { Id = 1, Description = "A warning notif", Name = "Warning", TenantId = "mostar" },
                new NotificationType { Id = 2, Description = "A error notif", Name = "Error", TenantId = "mostar" }
                );
            modelBuilder.Entity<Notification>().HasData(
                new Notification { Id = 1,Description="New notif", Date = new DateOnly(2024, 1, 1), Duration = new TimeOnly(1, 1, 1), IsActive = true, LineId = 1, NotificationTypeId = 1, TenantId = "mostar" },
                new Notification { Id = 2,Description = "New notif", Date = new DateOnly(2024, 1, 1), Duration = new TimeOnly(1, 1, 1), IsActive = true, LineId = 2, NotificationTypeId = 2, TenantId = "mostar" }
                );
            modelBuilder.Entity<PassengerCreditCard>().HasData(
                new PassengerCreditCard { Id = 1, CreditCardId = 1, PassengerId = 5, SavingDate = new DateTime(2024, 1, 1), TenantId = "mostar" },
                new PassengerCreditCard { Id = 2, CreditCardId = 2, PassengerId = 6, SavingDate = new DateTime(2024, 1, 1), TenantId = "mostar" }
                );
            modelBuilder.Entity<Models.Route>().HasData(
               new Models.Route { Id = 1, LineId = 1, StationId = 1, DistanceFromTheNextStation = 15.6f, TenantId = "mostar" },
               new Models.Route { Id = 2, LineId = 2, StationId = 2, DistanceFromTheNextStation = 15.6f, TenantId = "mostar" }
               );
            modelBuilder.Entity<Shift>().HasData(
                new Shift { Id = 1, BusId = 1, DriverId = 1, ShiftDate = new DateOnly(2024, 1, 1), ShiftEndingTime = new TimeOnly(16, 0, 0), ShiftStartingTime = new TimeOnly(8, 0, 0), TenantId = "mostar" },
                new Shift { Id = 2, BusId = 2, DriverId = 2, ShiftDate = new DateOnly(2024, 1, 1), ShiftEndingTime = new TimeOnly(16, 0, 0), ShiftStartingTime = new TimeOnly(8, 0, 0), TenantId = "mostar" }
                );
            modelBuilder.Entity<TicketType>().HasData(
                new TicketType { Id = 1, Name = "Basic", TenantId = "mostar" },
                new TicketType { Id = 2, Name = "Advanced", TenantId = "mostar" }
                );
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id = 1, CreatedDate = new DateTime(2024, 1, 1), ExpirationDate = new DateTime(2024, 2, 2), LineId = 1, QrCode = new byte[] { }, TicketTypeId = 1, UserId = 2, ZoneId = 1, TenantId = "mostar" },
                new Ticket { Id = 2, CreatedDate = new DateTime(2024, 2, 1), ExpirationDate = new DateTime(2024, 2, 2), LineId = 2, QrCode = new byte[] { }, TicketTypeId = 2, UserId = 1, ZoneId = 1, TenantId = "mostar" }
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
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }


    }
}

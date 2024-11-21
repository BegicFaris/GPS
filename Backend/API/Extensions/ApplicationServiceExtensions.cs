using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.BusServices;
using GPS.API.Services.DriverServices;
using GPS.API.Services.LineServices;
using GPS.API.Services.ManagerServices;
using GPS.API.Services.PassengerServices;
using GPS.API.Services.ScheduleServices;
using GPS.API.Services.TenantServices;
using GPS.API.Services.TokenServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Services.UserServices;

namespace GPS.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
            IConfiguration config
            ) {
            
            // Add services to the container.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("db1")));

            services.AddDbContext<TenantDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("db1")));


            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();


            //dodajte vaše servise
            //builder.Services.AddTransient<MyAuthService>();
            // Adding the scpoped service MyAppUserService, DriverService, PassengerService and ManagerService
            services.AddScoped<IMyAppUserService, MyAppUserService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IPassengerService, PassengerService>();
            services.AddScoped<IManagerService, ManagerService>();
            // Adding the scpoped service LineService and ScheduleService
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ILineService, LineService>();
            services.AddScoped<IBusService, BusService>();
            // Adding the scpoped service CurrentTenantService
            services.AddScoped<ICurrentTenantService, CurrentTenantService>();
            services.AddScoped<IPasswordHasher<MyAppUser>, PasswordHasher<MyAppUser>>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}

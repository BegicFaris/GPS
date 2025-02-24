using GPS.API.Data.DbContexts;
using GPS.API.Data.Models;
using GPS.API.Interfaces;
using GPS.API.Services.BusServices;
using GPS.API.Services.DriverServices;
using GPS.API.Services.DiscountServices;
using GPS.API.Services.FeedbackServices;
using GPS.API.Services.LineServices;
using GPS.API.Services.ManagerServices;
using GPS.API.Services.NotificationServices;
using GPS.API.Services.NotificationTypeService;
using GPS.API.Services.PassengerServices;
using GPS.API.Services.RouteServices;
using GPS.API.Services.ScheduleServices;
using GPS.API.Services.ShiftServices;
using GPS.API.Services.StationServices;
using GPS.API.Services.TenantServices;
using GPS.API.Services.TicketServices;
using GPS.API.Services.TokenServices;
using GPS.API.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using GPS.API.Services.ZoneServices;
using GPS.API.Services.EmailServices;
using GPS.API.Services.PasswordresetServices;
using GPS.API.Services;
using GPS.API.Services.TwoFactorAuthServices;
using GPS.API.Services.FavoriteLineService;
using GPS.API.Services.TicketInfoServices;
using GPS.API.Services.TicketTypeServices;
using GPS.API.Services.StripeServices;
using GPS.API.Services.ShiftDetailServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using GPS.API.Validators.RegisterValidators;

namespace GPS.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            // Add services to the container.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("db1")
                ));

            services.AddDbContext<TenantDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("db1")));
            services.AddHttpClient();




            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Please enter your token with this format: ''Bearer YOUR_TOKEN''",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                     }
                });
            });

            services.AddHttpContextAccessor();
            //dodajte vaše servise
            //builder.Services.AddTransient<MyAuthService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationTypeService, NotificationTypeService>();
            services.AddScoped<IShiftService, ShiftService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IStationService, StationService>();
            services.AddScoped<IMyAppUserService, MyAppUserService>();
            services.AddScoped<IShiftDetailService, ShiftDetailService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IPassengerService, PassengerService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ILineService, LineService>();
            services.AddScoped<IBusService, BusService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IZoneService, ZoneService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IFavoriteLineService, FavoriteLineService>();
            // Adding the scpoped service CurrentTenantService
            services.AddScoped<ICurrentTenantService, CurrentTenantService>();
            services.AddScoped<IPasswordHasher<MyAppUser>, PasswordHasher<MyAppUser>>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITicketInfoService, TicketInfoService>();
            services.AddScoped<ITwoFactorAuthService, TwoFactorAuthService>();
            services.AddScoped<ITicketTypeService, TicketTypeService>();
            services.AddScoped<IStripeService, StripeService>();

            services.AddHostedService<ShiftDetailBackgroundService>();


            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

            return services;
        }
    }
}

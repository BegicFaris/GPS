
using GPS.API.Middleware;
using GPS.API.Data.DbContexts;
using GPS.API.Interfaces;
using GPS.API.Services.BusServices;
using GPS.API.Services.DriverServices;
using GPS.API.Services.LineServices;
using GPS.API.Services.ManagerServices;
using GPS.API.Services.PassengerServices;
using GPS.API.Services.ScheduleServices; 
using GPS.API.Services.TenantServices;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Services.UserServices;

var config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", false)
.Build();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db1")));

builder.Services.AddDbContext<TenantDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("db1")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();


//dodajte vaše servise
//builder.Services.AddTransient<MyAuthService>();
// Adding the scpoped service MyAppUserService, DriverService, PassengerService and ManagerService
builder.Services.AddScoped<IMyAppUserService, MyAppUserService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
// Adding the scpoped service LineService and ScheduleService
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<ILineService, LineService>();
builder.Services.AddScoped<IBusService, BusService>();
// Adding the scpoped service CurrentTenantService
builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(
    options => options
        .SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
); //This needs to set everything allowed


app.UseAuthorization();

app.UseMiddleware<TenantResolver>();

app.MapControllers();

app.Run();

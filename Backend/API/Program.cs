
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
using GPS.API.Data.Models;
using Microsoft.AspNetCore.Identity;
using GPS.API.Services.TokenServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GPS.API.Extensions;


var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
//Servisi
builder.Services.AddApplicationServices(config);
//Identiteti
builder.Services.AddIdentityServices(config);

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


//app.UseCors(
//    options => options
//        .SetIsOriginAllowed(x => _ = true)
//        .AllowAnyMethod()
//        .AllowAnyHeader()
//        .AllowCredentials()
//); //This needs to set everything allowed //BEGINO

//KURS
app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod()
            .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TenantResolver>();

app.MapControllers();

app.Run();

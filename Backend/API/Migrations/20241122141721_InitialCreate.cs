using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GPS.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufactureYear = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyAppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyAppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriversLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkingHoursInAWeek = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_MyAppUsers_Id",
                        column: x => x.Id,
                        principalTable: "MyAppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_MyAppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MyAppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerLevel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_MyAppUsers_Id",
                        column: x => x.Id,
                        principalTable: "MyAppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DiscountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passengers_Discounts_DiscountID",
                        column: x => x.DiscountID,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Passengers_MyAppUsers_Id",
                        column: x => x.Id,
                        principalTable: "MyAppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemActionsLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QueryPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeOfAction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsException = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemActionsLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemActionsLog_MyAppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MyAppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPSCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    ShiftDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ShiftStartingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ShiftEndingTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shifts_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PassengerCreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    CreditCardId = table.Column<int>(type: "int", nullable: false),
                    SavingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerCreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassengerCreditCards_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PassengerCreditCards_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingStationId = table.Column<int>(type: "int", nullable: false),
                    EndingStationId = table.Column<int>(type: "int", nullable: false),
                    CompleteDistance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lines_Stations_EndingStationId",
                        column: x => x.EndingStationId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lines_Stations_StartingStationId",
                        column: x => x.StartingStationId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<TimeOnly>(type: "time", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    DistanceFromTheNextStation = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Routes_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: false),
                    TicketTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QrCode = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_MyAppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MyAppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "Id", "Capacity", "ManufactureYear", "Manufacturer", "Model", "RegistrationNumber" },
                values: new object[,]
                {
                    { 1, "20", "2002", "MAN", "MK2", "12345678" },
                    { 2, "21", "2003", "MAN", "MK3", "asd5678" }
                });

            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "Id", "CCV", "CardName", "CardNumber", "ExpirationDate" },
                values: new object[,]
                {
                    { 1, 123, "Faris", "1234 5679 8791", "7/28" },
                    { 2, 254, "Nedim", "2432 4454 4545", "7/28" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountName", "DiscountValue" },
                values: new object[,]
                {
                    { 1, "Student", 0.15f },
                    { 2, "Penzioner", 0.17f }
                });

            migrationBuilder.InsertData(
                table: "MyAppUsers",
                columns: new[] { "Id", "Address", "BirthDate", "Email", "FirstName", "Image", "LastName", "PasswordHash", "PasswordSalt", "RegistrationDate", "Status" },
                values: new object[,]
                {
                    { 1, null, null, "mail@mail.com", "Adi", null, "Gosto", new byte[] { 101, 49, 102, 50, 49, 52, 50, 97, 101, 99, 48, 53, 53, 100, 51, 51, 52, 97, 48, 52, 56, 97, 53, 50, 102, 53, 49, 99, 50, 48, 52, 100, 51, 49, 56, 56, 57, 97, 50, 98, 55, 51, 48, 53, 102, 53, 57, 57, 55, 101, 51, 55, 100, 55, 101, 53, 51, 57, 53, 49, 57, 52, 102, 101, 99, 57, 98, 98, 50, 51, 56, 51, 101, 52, 102, 54, 54, 101, 102, 97, 54, 55, 98, 100, 101, 102, 100, 51, 101, 48, 51, 56, 52, 101, 99, 99, 54, 57, 57, 55, 54, 49, 99, 48, 53, 98, 49, 57, 101, 57, 54, 53, 98, 49, 53, 49, 97, 102, 56, 97, 52, 100, 100, 52, 102, 53, 102, 100 }, null, null, null },
                    { 2, null, null, "mail@mail2.com", "Nedim", null, "Jugo", new byte[] { 57, 51, 99, 56, 98, 98, 99, 52, 98, 57, 54, 100, 51, 50, 54, 99, 100, 49, 57, 50, 56, 56, 51, 49, 56, 50, 56, 54, 98, 48, 55, 102, 97, 54, 57, 51, 51, 98, 101, 54, 98, 55, 52, 100, 52, 97, 100, 54, 102, 49, 101, 56, 54, 49, 102, 51, 98, 53, 56, 48, 102, 98, 57, 48, 57, 102, 48, 100, 57, 48, 48, 49, 100, 100, 48, 97, 51, 101, 55, 57, 48, 49, 49, 54, 98, 54, 102, 56, 56, 53, 51, 55, 50, 98, 49, 98, 97, 48, 48, 53, 102, 53, 48, 101, 48, 98, 102, 53, 97, 57, 48, 53, 49, 54, 52, 55, 97, 54, 49, 48, 52, 53, 49, 56, 99, 97, 97, 52 }, null, null, null },
                    { 3, null, null, "mail@mail.com", "Adil", null, "Joldic", new byte[] { 101, 49, 102, 50, 49, 52, 50, 97, 101, 99, 48, 53, 53, 100, 51, 51, 52, 97, 48, 52, 56, 97, 53, 50, 102, 53, 49, 99, 50, 48, 52, 100, 51, 49, 56, 56, 57, 97, 50, 98, 55, 51, 48, 53, 102, 53, 57, 57, 55, 101, 51, 55, 100, 55, 101, 53, 51, 57, 53, 49, 57, 52, 102, 101, 99, 57, 98, 98, 50, 51, 56, 51, 101, 52, 102, 54, 54, 101, 102, 97, 54, 55, 98, 100, 101, 102, 100, 51, 101, 48, 51, 56, 52, 101, 99, 99, 54, 57, 57, 55, 54, 49, 99, 48, 53, 98, 49, 57, 101, 57, 54, 53, 98, 49, 53, 49, 97, 102, 56, 97, 52, 100, 100, 52, 102, 53, 102, 100 }, null, null, null },
                    { 4, null, null, "mail@mail2.com", "Denis", null, "Music", new byte[] { 57, 51, 99, 56, 98, 98, 99, 52, 98, 57, 54, 100, 51, 50, 54, 99, 100, 49, 57, 50, 56, 56, 51, 49, 56, 50, 56, 54, 98, 48, 55, 102, 97, 54, 57, 51, 51, 98, 101, 54, 98, 55, 52, 100, 52, 97, 100, 54, 102, 49, 101, 56, 54, 49, 102, 51, 98, 53, 56, 48, 102, 98, 57, 48, 57, 102, 48, 100, 57, 48, 48, 49, 100, 100, 48, 97, 51, 101, 55, 57, 48, 49, 49, 54, 98, 54, 102, 56, 56, 53, 51, 55, 50, 98, 49, 98, 97, 48, 48, 53, 102, 53, 48, 101, 48, 98, 102, 53, 97, 57, 48, 53, 49, 54, 52, 55, 97, 54, 49, 48, 52, 53, 49, 56, 99, 97, 97, 52 }, null, null, null },
                    { 5, null, null, "mail@mail.com", "Adil", null, "Joldic", new byte[] { 101, 49, 102, 50, 49, 52, 50, 97, 101, 99, 48, 53, 53, 100, 51, 51, 52, 97, 48, 52, 56, 97, 53, 50, 102, 53, 49, 99, 50, 48, 52, 100, 51, 49, 56, 56, 57, 97, 50, 98, 55, 51, 48, 53, 102, 53, 57, 57, 55, 101, 51, 55, 100, 55, 101, 53, 51, 57, 53, 49, 57, 52, 102, 101, 99, 57, 98, 98, 50, 51, 56, 51, 101, 52, 102, 54, 54, 101, 102, 97, 54, 55, 98, 100, 101, 102, 100, 51, 101, 48, 51, 56, 52, 101, 99, 99, 54, 57, 57, 55, 54, 49, 99, 48, 53, 98, 49, 57, 101, 57, 54, 53, 98, 49, 53, 49, 97, 102, 56, 97, 52, 100, 100, 52, 102, 53, 102, 100 }, null, null, null },
                    { 6, null, null, "mail@mail2.com", "Denis", null, "Music", new byte[] { 57, 51, 99, 56, 98, 98, 99, 52, 98, 57, 54, 100, 51, 50, 54, 99, 100, 49, 57, 50, 56, 56, 51, 49, 56, 50, 56, 54, 98, 48, 55, 102, 97, 54, 57, 51, 51, 98, 101, 54, 98, 55, 52, 100, 52, 97, 100, 54, 102, 49, 101, 56, 54, 49, 102, 51, 98, 53, 56, 48, 102, 98, 57, 48, 57, 102, 48, 100, 57, 48, 48, 49, 100, 100, 48, 97, 51, 101, 55, 57, 48, 49, 49, 54, 98, 54, 102, 56, 56, 53, 51, 55, 50, 98, 49, 98, 97, 48, 48, 53, 102, 53, 48, 101, 48, 98, 102, 53, 97, 57, 48, 53, 49, 54, 52, 55, 97, 54, 49, 48, 52, 53, 49, 56, 99, 97, 97, 52 }, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A warning notif", "Warning" },
                    { 2, "A error notif", "Error" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "tenant1", "Tenant1" },
                    { "tenant2", "Tenant2" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Basic" },
                    { 2, "Advanced" }
                });

            migrationBuilder.InsertData(
                table: "Zones",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Zone one", 1.5m },
                    { 2, "Zone two", 2.1m },
                    { 3, "Zone three", 2.7m }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "DriversLicenseNumber", "HireDate", "License", "WorkingHoursInAWeek" },
                values: new object[,]
                {
                    { 1, "a1435affaa", new DateTime(2024, 11, 22, 15, 17, 20, 384, DateTimeKind.Local).AddTicks(2940), "1123123", null },
                    { 2, "adasd43aa", new DateTime(2024, 11, 22, 15, 17, 20, 386, DateTimeKind.Local).AddTicks(2037), "11jdfghsdjg23", null }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "Comment", "Date", "Picture", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5f, 5 },
                    { 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3f, 6 }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Department", "HireDate", "ManagerLevel" },
                values: new object[,]
                {
                    { 3, "HR", new DateTime(2024, 11, 22, 15, 17, 20, 386, DateTimeKind.Local).AddTicks(2896), "1" },
                    { 4, "IT", new DateTime(2024, 11, 22, 15, 17, 20, 386, DateTimeKind.Local).AddTicks(3425), "2" }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "DiscountID" },
                values: new object[,]
                {
                    { 5, null },
                    { 6, null }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "GPSCode", "Location", "Name", "ZoneId" },
                values: new object[,]
                {
                    { 1, "6.6.6", "Bafo", "Bafo", 1 },
                    { 2, "13123", "Sutina", "Sutina1", 2 }
                });

            migrationBuilder.InsertData(
                table: "Lines",
                columns: new[] { "Id", "CompleteDistance", "EndingStationId", "IsActive", "Name", "StartingStationId" },
                values: new object[,]
                {
                    { 1, "10", 1, true, "21", 2 },
                    { 2, "10", 1, true, "21", 2 }
                });

            migrationBuilder.InsertData(
                table: "PassengerCreditCards",
                columns: new[] { "Id", "CreditCardId", "PassengerId", "SavingDate" },
                values: new object[,]
                {
                    { 1, 1, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "BusId", "DriverId", "ShiftDate", "ShiftEndingTime", "ShiftStartingTime" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateOnly(2024, 1, 1), new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0) },
                    { 2, 2, 2, new DateOnly(2024, 1, 1), new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "Date", "Duration", "IsActive", "LineId", "NotificationTypeId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 1, 1), new TimeOnly(1, 1, 1), true, 1, 1 },
                    { 2, new DateOnly(2024, 1, 1), new TimeOnly(1, 1, 1), true, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "DistanceFromTheNextStation", "LineId", "StationId" },
                values: new object[,]
                {
                    { 1, 15.6f, 1, 1 },
                    { 2, 15.6f, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedDate", "ExpirationDate", "LineId", "QrCode", "TicketTypeId", "UserId", "ZoneId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new byte[0], 1, 2, 1 },
                    { 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new byte[0], 2, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_EndingStationId",
                table: "Lines",
                column: "EndingStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_StartingStationId",
                table: "Lines",
                column: "StartingStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LineId",
                table: "Notifications",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerCreditCards_CreditCardId",
                table: "PassengerCreditCards",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerCreditCards_PassengerId",
                table: "PassengerCreditCards",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_DiscountID",
                table: "Passengers",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_LineId",
                table: "Routes",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StationId",
                table: "Routes",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_LineId",
                table: "Schedules",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_BusId",
                table: "Shifts",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DriverId",
                table: "Shifts",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_ZoneId",
                table: "Stations",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemActionsLog_UserId",
                table: "SystemActionsLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_LineId",
                table: "Tickets",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeId",
                table: "Tickets",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ZoneId",
                table: "Tickets",
                column: "ZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PassengerCreditCards");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "SystemActionsLog");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "MyAppUsers");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}

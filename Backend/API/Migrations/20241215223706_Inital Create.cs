using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GPS.API.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
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
                    ManufactureYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CCV = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    DiscountValue = table.Column<float>(type: "real", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false),
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
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false),
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
                    GPSCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    ShiftEndingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    SavingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<TimeOnly>(type: "time", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    DistanceFromTheNextStation = table.Column<float>(type: "real", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    DepartureTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    QrCode = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                columns: new[] { "Id", "Capacity", "ManufactureYear", "Manufacturer", "Model", "RegistrationNumber", "TenantId" },
                values: new object[,]
                {
                    { 1, "20", "2002", "MAN", "MK2", "12345678", "mostar" },
                    { 2, "21", "2003", "MAN", "MK3", "asd5678", "mostar" }
                });

            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "Id", "CCV", "CardName", "CardNumber", "ExpirationDate", "TenantId" },
                values: new object[,]
                {
                    { 1, 123, "Faris", "1234 5679 8791", "7/28", "mostar" },
                    { 2, 254, "Nedim", "2432 4454 4545", "7/28", "mostar" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountName", "DiscountValue", "TenantId" },
                values: new object[,]
                {
                    { 1, "Student", 0.15f, "mostar" },
                    { 2, "Penzioner", 0.17f, "mostar" }
                });

            migrationBuilder.InsertData(
                table: "MyAppUsers",
                columns: new[] { "Id", "Address", "BirthDate", "Email", "FirstName", "Image", "LastName", "PasswordHash", "PasswordSalt", "RegistrationDate", "Status", "TenantId" },
                values: new object[,]
                {
                    { 1, null, null, "1", "Adi", null, "Gosto", new byte[] { 109, 185, 101, 142, 43, 107, 53, 9, 203, 104, 234, 164, 188, 84, 197, 8, 251, 78, 99, 174, 53, 194, 99, 108, 204, 200, 80, 166, 221, 211, 75, 63, 152, 103, 211, 85, 2, 16, 102, 103, 17, 13, 34, 223, 97, 196, 88, 193, 67, 113, 139, 203, 176, 235, 213, 227, 249, 204, 144, 208, 125, 218, 250, 9 }, new byte[] { 218, 10, 47, 252, 253, 255, 70, 13, 8, 235, 212, 209, 208, 5, 61, 122, 215, 202, 152, 153, 243, 242, 23, 196, 15, 170, 170, 139, 0, 244, 220, 178, 120, 88, 100, 127, 143, 255, 68, 119, 83, 7, 66, 166, 27, 23, 80, 125, 9, 46, 145, 156, 243, 22, 83, 211, 8, 35, 170, 35, 114, 19, 164, 195, 105, 203, 237, 224, 179, 35, 220, 241, 6, 219, 70, 182, 106, 219, 14, 56, 79, 237, 134, 178, 157, 99, 156, 56, 195, 139, 63, 33, 215, 198, 168, 150, 145, 204, 234, 244, 70, 239, 140, 47, 49, 237, 81, 95, 152, 175, 249, 10, 92, 210, 136, 94, 25, 200, 66, 122, 143, 43, 169, 180, 224, 255, 82, 155 }, null, null, "mostar" },
                    { 2, null, null, "2", "Nedim", null, "Jugo", new byte[] { 109, 185, 101, 142, 43, 107, 53, 9, 203, 104, 234, 164, 188, 84, 197, 8, 251, 78, 99, 174, 53, 194, 99, 108, 204, 200, 80, 166, 221, 211, 75, 63, 152, 103, 211, 85, 2, 16, 102, 103, 17, 13, 34, 223, 97, 196, 88, 193, 67, 113, 139, 203, 176, 235, 213, 227, 249, 204, 144, 208, 125, 218, 250, 9 }, new byte[] { 218, 10, 47, 252, 253, 255, 70, 13, 8, 235, 212, 209, 208, 5, 61, 122, 215, 202, 152, 153, 243, 242, 23, 196, 15, 170, 170, 139, 0, 244, 220, 178, 120, 88, 100, 127, 143, 255, 68, 119, 83, 7, 66, 166, 27, 23, 80, 125, 9, 46, 145, 156, 243, 22, 83, 211, 8, 35, 170, 35, 114, 19, 164, 195, 105, 203, 237, 224, 179, 35, 220, 241, 6, 219, 70, 182, 106, 219, 14, 56, 79, 237, 134, 178, 157, 99, 156, 56, 195, 139, 63, 33, 215, 198, 168, 150, 145, 204, 234, 244, 70, 239, 140, 47, 49, 237, 81, 95, 152, 175, 249, 10, 92, 210, 136, 94, 25, 200, 66, 122, 143, 43, 169, 180, 224, 255, 82, 155 }, null, null, "mostar" },
                    { 3, null, null, "3", "Adil", null, "Joldic", new byte[] { 109, 185, 101, 142, 43, 107, 53, 9, 203, 104, 234, 164, 188, 84, 197, 8, 251, 78, 99, 174, 53, 194, 99, 108, 204, 200, 80, 166, 221, 211, 75, 63, 152, 103, 211, 85, 2, 16, 102, 103, 17, 13, 34, 223, 97, 196, 88, 193, 67, 113, 139, 203, 176, 235, 213, 227, 249, 204, 144, 208, 125, 218, 250, 9 }, new byte[] { 218, 10, 47, 252, 253, 255, 70, 13, 8, 235, 212, 209, 208, 5, 61, 122, 215, 202, 152, 153, 243, 242, 23, 196, 15, 170, 170, 139, 0, 244, 220, 178, 120, 88, 100, 127, 143, 255, 68, 119, 83, 7, 66, 166, 27, 23, 80, 125, 9, 46, 145, 156, 243, 22, 83, 211, 8, 35, 170, 35, 114, 19, 164, 195, 105, 203, 237, 224, 179, 35, 220, 241, 6, 219, 70, 182, 106, 219, 14, 56, 79, 237, 134, 178, 157, 99, 156, 56, 195, 139, 63, 33, 215, 198, 168, 150, 145, 204, 234, 244, 70, 239, 140, 47, 49, 237, 81, 95, 152, 175, 249, 10, 92, 210, 136, 94, 25, 200, 66, 122, 143, 43, 169, 180, 224, 255, 82, 155 }, null, null, "mostar" },
                    { 4, null, null, "4", "Denis", null, "Music", new byte[] { 109, 185, 101, 142, 43, 107, 53, 9, 203, 104, 234, 164, 188, 84, 197, 8, 251, 78, 99, 174, 53, 194, 99, 108, 204, 200, 80, 166, 221, 211, 75, 63, 152, 103, 211, 85, 2, 16, 102, 103, 17, 13, 34, 223, 97, 196, 88, 193, 67, 113, 139, 203, 176, 235, 213, 227, 249, 204, 144, 208, 125, 218, 250, 9 }, new byte[] { 218, 10, 47, 252, 253, 255, 70, 13, 8, 235, 212, 209, 208, 5, 61, 122, 215, 202, 152, 153, 243, 242, 23, 196, 15, 170, 170, 139, 0, 244, 220, 178, 120, 88, 100, 127, 143, 255, 68, 119, 83, 7, 66, 166, 27, 23, 80, 125, 9, 46, 145, 156, 243, 22, 83, 211, 8, 35, 170, 35, 114, 19, 164, 195, 105, 203, 237, 224, 179, 35, 220, 241, 6, 219, 70, 182, 106, 219, 14, 56, 79, 237, 134, 178, 157, 99, 156, 56, 195, 139, 63, 33, 215, 198, 168, 150, 145, 204, 234, 244, 70, 239, 140, 47, 49, 237, 81, 95, 152, 175, 249, 10, 92, 210, 136, 94, 25, 200, 66, 122, 143, 43, 169, 180, 224, 255, 82, 155 }, null, null, "mostar" },
                    { 5, null, null, "5", "Adil", null, "Joldic", new byte[] { 109, 185, 101, 142, 43, 107, 53, 9, 203, 104, 234, 164, 188, 84, 197, 8, 251, 78, 99, 174, 53, 194, 99, 108, 204, 200, 80, 166, 221, 211, 75, 63, 152, 103, 211, 85, 2, 16, 102, 103, 17, 13, 34, 223, 97, 196, 88, 193, 67, 113, 139, 203, 176, 235, 213, 227, 249, 204, 144, 208, 125, 218, 250, 9 }, new byte[] { 218, 10, 47, 252, 253, 255, 70, 13, 8, 235, 212, 209, 208, 5, 61, 122, 215, 202, 152, 153, 243, 242, 23, 196, 15, 170, 170, 139, 0, 244, 220, 178, 120, 88, 100, 127, 143, 255, 68, 119, 83, 7, 66, 166, 27, 23, 80, 125, 9, 46, 145, 156, 243, 22, 83, 211, 8, 35, 170, 35, 114, 19, 164, 195, 105, 203, 237, 224, 179, 35, 220, 241, 6, 219, 70, 182, 106, 219, 14, 56, 79, 237, 134, 178, 157, 99, 156, 56, 195, 139, 63, 33, 215, 198, 168, 150, 145, 204, 234, 244, 70, 239, 140, 47, 49, 237, 81, 95, 152, 175, 249, 10, 92, 210, 136, 94, 25, 200, 66, 122, 143, 43, 169, 180, 224, 255, 82, 155 }, null, null, "mostar" },
                    { 6, null, null, "6", "Denis", null, "Music", new byte[] { 109, 185, 101, 142, 43, 107, 53, 9, 203, 104, 234, 164, 188, 84, 197, 8, 251, 78, 99, 174, 53, 194, 99, 108, 204, 200, 80, 166, 221, 211, 75, 63, 152, 103, 211, 85, 2, 16, 102, 103, 17, 13, 34, 223, 97, 196, 88, 193, 67, 113, 139, 203, 176, 235, 213, 227, 249, 204, 144, 208, 125, 218, 250, 9 }, new byte[] { 218, 10, 47, 252, 253, 255, 70, 13, 8, 235, 212, 209, 208, 5, 61, 122, 215, 202, 152, 153, 243, 242, 23, 196, 15, 170, 170, 139, 0, 244, 220, 178, 120, 88, 100, 127, 143, 255, 68, 119, 83, 7, 66, 166, 27, 23, 80, 125, 9, 46, 145, 156, 243, 22, 83, 211, 8, 35, 170, 35, 114, 19, 164, 195, 105, 203, 237, 224, 179, 35, 220, 241, 6, 219, 70, 182, 106, 219, 14, 56, 79, 237, 134, 178, 157, 99, 156, 56, 195, 139, 63, 33, 215, 198, 168, 150, 145, 204, 234, 244, 70, 239, 140, 47, 49, 237, 81, 95, 152, 175, 249, 10, 92, 210, 136, 94, 25, 200, 66, 122, 143, 43, 169, 180, 224, 255, 82, 155 }, null, null, "mostar" }
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Description", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "A warning notif", "Warning", "mostar" },
                    { 2, "A error notif", "Error", "mostar" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "bugojno", "Bugojno" },
                    { "mostar", "Mostar" },
                    { "sarajevo", "Sarajevo" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "Basic", "mostar" },
                    { 2, "Advanced", "mostar" }
                });

            migrationBuilder.InsertData(
                table: "Zones",
                columns: new[] { "Id", "Name", "Price", "TenantId" },
                values: new object[,]
                {
                    { 1, "Zone one", 1.5m, null },
                    { 2, "Zone two", 2.1m, null },
                    { 3, "Zone three", 2.7m, null }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "DriversLicenseNumber", "HireDate", "License", "WorkingHoursInAWeek" },
                values: new object[,]
                {
                    { 1, "a1435affaa", new DateOnly(2024, 12, 1), "1123123", null },
                    { 2, "adasd43aa", new DateOnly(2024, 12, 1), "11jdfghsdjg23", null }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "Comment", "Date", "Picture", "Rating", "TenantId", "UserId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5f, "mostar", 5 },
                    { 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3f, "mostar", 6 }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Department", "HireDate", "ManagerLevel" },
                values: new object[,]
                {
                    { 3, "HR", new DateOnly(2024, 12, 1), "1" },
                    { 4, "IT", new DateOnly(2024, 12, 1), "2" }
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
                columns: new[] { "Id", "GPSCode", "Location", "Name", "TenantId", "ZoneId" },
                values: new object[,]
                {
                    { 1, "6.6.6", "Bafo", "Bafo", "mostar", 1 },
                    { 2, "13123", "Sutina", "Sutina1", "mostar", 2 }
                });

            migrationBuilder.InsertData(
                table: "Lines",
                columns: new[] { "Id", "CompleteDistance", "EndingStationId", "IsActive", "Name", "StartingStationId", "TenantId" },
                values: new object[,]
                {
                    { 1, "10", 1, true, "21", 2, "mostar" },
                    { 2, "10", 1, true, "21", 2, "mostar" }
                });

            migrationBuilder.InsertData(
                table: "PassengerCreditCards",
                columns: new[] { "Id", "CreditCardId", "PassengerId", "SavingDate", "TenantId" },
                values: new object[,]
                {
                    { 1, 1, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mostar" },
                    { 2, 2, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mostar" }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "BusId", "DriverId", "ShiftDate", "ShiftEndingTime", "ShiftStartingTime", "TenantId" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateOnly(2024, 1, 1), new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0), "mostar" },
                    { 2, 2, 2, new DateOnly(2024, 1, 1), new TimeOnly(16, 0, 0), new TimeOnly(8, 0, 0), "mostar" }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "Date", "Description", "Duration", "IsActive", "LineId", "NotificationTypeId", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 1, 1), "New notif", new TimeOnly(1, 1, 1), true, 1, 1, "mostar" },
                    { 2, new DateOnly(2024, 1, 1), "New notif", new TimeOnly(1, 1, 1), true, 2, 2, "mostar" }
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "DistanceFromTheNextStation", "LineId", "StationId", "TenantId" },
                values: new object[,]
                {
                    { 1, 15.6f, 1, 1, "mostar" },
                    { 2, 15.6f, 2, 2, "mostar" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedDate", "ExpirationDate", "LineId", "QrCode", "TenantId", "TicketTypeId", "UserId", "ZoneId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new byte[0], "mostar", 1, 2, 1 },
                    { 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new byte[0], "mostar", 2, 1, 1 }
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

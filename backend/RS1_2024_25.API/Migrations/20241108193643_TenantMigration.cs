using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1_2024_25.API.Migrations
{
    /// <inheritdoc />
    public partial class TenantMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "MyAppUsers");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "MyAppUsers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "MyAppUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "MyAppUsers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "IsManager",
                table: "MyAppUsers",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "MyAppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DiscountID",
                table: "MyAppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MyAppUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriversLicenseNumber",
                table: "MyAppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "MyAppUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "MyAppUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "License",
                table: "MyAppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Manager_HireDate",
                table: "MyAppUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "MyAppUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "MyAppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "WorkingHoursInAWeek",
                table: "MyAppUsers",
                type: "real",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<int>(type: "int", nullable: false),
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
                    DiscountValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
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
                        name: "FK_Shifts_MyAppUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "MyAppUsers",
                        principalColumn: "Id");
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
                        name: "FK_PassengerCreditCards_MyAppUsers_PassengerId",
                        column: x => x.PassengerId,
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
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingStationID = table.Column<int>(type: "int", nullable: false),
                    EndingStationID = table.Column<int>(type: "int", nullable: false),
                    CompleteDistance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lines_Stations_EndingStationID",
                        column: x => x.EndingStationID,
                        principalTable: "Stations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lines_Stations_StartingStationID",
                        column: x => x.StartingStationID,
                        principalTable: "Stations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_MyAppUsers_DiscountID",
                table: "MyAppUsers",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_EndingStationID",
                table: "Lines",
                column: "EndingStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_StartingStationID",
                table: "Lines",
                column: "StartingStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LineId",
                table: "Notifications",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerCreditCards_CreditCardId",
                table: "PassengerCreditCards",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerCreditCards_PassengerId",
                table: "PassengerCreditCards",
                column: "PassengerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MyAppUsers_Discounts_DiscountID",
                table: "MyAppUsers",
                column: "DiscountID",
                principalTable: "Discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyAppUsers_Discounts_DiscountID",
                table: "MyAppUsers");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Feedbacks");

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
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_MyAppUsers_DiscountID",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "DiscountID",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "DriversLicenseNumber",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "License",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "Manager_HireDate",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "WorkingHoursInAWeek",
                table: "MyAppUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MyAppUsers",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "MyAppUsers",
                newName: "IsManager");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "MyAppUsers",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "MyAppUsers",
                newName: "Password");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "MyAppUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");
        }
    }
}

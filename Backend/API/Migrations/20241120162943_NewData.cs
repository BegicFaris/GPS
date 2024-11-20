using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GPS.API.Migrations
{
    /// <inheritdoc />
    public partial class NewData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 4,
                column: "HireDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "GPSCode", "Location", "Name", "ZoneId" },
                values: new object[,]
                {
                    { 1, "6.6.6", "Bafo", "Bafo", 1 },
                    { 2, "13123", "Sutina", "Sutina1", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2024, 11, 20, 17, 19, 23, 744, DateTimeKind.Local).AddTicks(1019));

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2024, 11, 20, 17, 19, 23, 746, DateTimeKind.Local).AddTicks(4258));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 11, 20, 17, 19, 23, 746, DateTimeKind.Local).AddTicks(7277));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 11, 20, 17, 19, 23, 746, DateTimeKind.Local).AddTicks(8134));

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2024, 11, 20, 17, 19, 23, 746, DateTimeKind.Local).AddTicks(5053));

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 4,
                column: "HireDate",
                value: new DateTime(2024, 11, 20, 17, 19, 23, 746, DateTimeKind.Local).AddTicks(5821));
        }
    }
}

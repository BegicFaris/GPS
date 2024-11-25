using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPS.API.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Buses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Buses",
                keyColumn: "Id",
                keyValue: 1,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Buses",
                keyColumn: "Id",
                keyValue: 2,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2024, 11, 22, 22, 21, 7, 421, DateTimeKind.Local).AddTicks(2693));

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2024, 11, 22, 22, 21, 7, 423, DateTimeKind.Local).AddTicks(4960));

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2024, 11, 22, 22, 21, 7, 423, DateTimeKind.Local).AddTicks(5676));

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 4,
                column: "HireDate",
                value: new DateTime(2024, 11, 22, 22, 21, 7, 423, DateTimeKind.Local).AddTicks(6551));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Buses");

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2024, 11, 22, 20, 10, 26, 855, DateTimeKind.Local).AddTicks(7182));

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2024, 11, 22, 20, 10, 26, 857, DateTimeKind.Local).AddTicks(9086));

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2024, 11, 22, 20, 10, 26, 857, DateTimeKind.Local).AddTicks(9759));

            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 4,
                column: "HireDate",
                value: new DateTime(2024, 11, 22, 20, 10, 26, 858, DateTimeKind.Local).AddTicks(537));
        }
    }
}

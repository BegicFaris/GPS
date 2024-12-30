using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GPS.API.Migrations
{
    /// <inheritdoc />
    public partial class Changingdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketTypes_TicketTypeId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Zones_ZoneId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketTypeId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "TicketTypeId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "ZoneId",
                table: "Tickets",
                newName: "TicketInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ZoneId",
                table: "Tickets",
                newName: "IX_Tickets_TicketInfoId");

            migrationBuilder.CreateTable(
                name: "TicketInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: false),
                    TicketTypeId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketInfos_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketInfos_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 13, 40, 248, 78, 152, 234, 235, 7, 169, 64, 158, 154, 160, 216, 234, 225, 174, 173, 105, 73, 144, 45, 155, 141, 117, 64, 117, 159, 62, 55, 236, 110, 43, 70, 240, 5, 181, 80, 41, 61, 25, 131, 133, 141, 83, 165, 136, 60, 38, 13, 183, 60, 237, 123, 89, 168, 119, 128, 100, 100, 85, 210, 6 }, new byte[] { 208, 111, 107, 245, 1, 42, 97, 243, 79, 254, 7, 22, 135, 234, 61, 249, 86, 128, 184, 187, 235, 213, 52, 75, 2, 123, 48, 189, 99, 139, 138, 5, 121, 80, 178, 3, 167, 65, 224, 69, 33, 12, 9, 98, 123, 130, 255, 64, 136, 203, 11, 157, 76, 68, 59, 119, 109, 192, 126, 214, 144, 41, 170, 39, 0, 128, 48, 184, 125, 217, 166, 195, 215, 181, 37, 150, 137, 172, 11, 61, 10, 143, 16, 235, 173, 24, 84, 170, 203, 127, 169, 123, 122, 63, 172, 47, 93, 69, 240, 195, 54, 122, 227, 235, 109, 76, 110, 99, 39, 132, 205, 58, 142, 18, 99, 178, 138, 205, 114, 32, 125, 220, 173, 185, 140, 50, 231, 21 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 13, 40, 248, 78, 152, 234, 235, 7, 169, 64, 158, 154, 160, 216, 234, 225, 174, 173, 105, 73, 144, 45, 155, 141, 117, 64, 117, 159, 62, 55, 236, 110, 43, 70, 240, 5, 181, 80, 41, 61, 25, 131, 133, 141, 83, 165, 136, 60, 38, 13, 183, 60, 237, 123, 89, 168, 119, 128, 100, 100, 85, 210, 6 }, new byte[] { 208, 111, 107, 245, 1, 42, 97, 243, 79, 254, 7, 22, 135, 234, 61, 249, 86, 128, 184, 187, 235, 213, 52, 75, 2, 123, 48, 189, 99, 139, 138, 5, 121, 80, 178, 3, 167, 65, 224, 69, 33, 12, 9, 98, 123, 130, 255, 64, 136, 203, 11, 157, 76, 68, 59, 119, 109, 192, 126, 214, 144, 41, 170, 39, 0, 128, 48, 184, 125, 217, 166, 195, 215, 181, 37, 150, 137, 172, 11, 61, 10, 143, 16, 235, 173, 24, 84, 170, 203, 127, 169, 123, 122, 63, 172, 47, 93, 69, 240, 195, 54, 122, 227, 235, 109, 76, 110, 99, 39, 132, 205, 58, 142, 18, 99, 178, 138, 205, 114, 32, 125, 220, 173, 185, 140, 50, 231, 21 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 13, 40, 248, 78, 152, 234, 235, 7, 169, 64, 158, 154, 160, 216, 234, 225, 174, 173, 105, 73, 144, 45, 155, 141, 117, 64, 117, 159, 62, 55, 236, 110, 43, 70, 240, 5, 181, 80, 41, 61, 25, 131, 133, 141, 83, 165, 136, 60, 38, 13, 183, 60, 237, 123, 89, 168, 119, 128, 100, 100, 85, 210, 6 }, new byte[] { 208, 111, 107, 245, 1, 42, 97, 243, 79, 254, 7, 22, 135, 234, 61, 249, 86, 128, 184, 187, 235, 213, 52, 75, 2, 123, 48, 189, 99, 139, 138, 5, 121, 80, 178, 3, 167, 65, 224, 69, 33, 12, 9, 98, 123, 130, 255, 64, 136, 203, 11, 157, 76, 68, 59, 119, 109, 192, 126, 214, 144, 41, 170, 39, 0, 128, 48, 184, 125, 217, 166, 195, 215, 181, 37, 150, 137, 172, 11, 61, 10, 143, 16, 235, 173, 24, 84, 170, 203, 127, 169, 123, 122, 63, 172, 47, 93, 69, 240, 195, 54, 122, 227, 235, 109, 76, 110, 99, 39, 132, 205, 58, 142, 18, 99, 178, 138, 205, 114, 32, 125, 220, 173, 185, 140, 50, 231, 21 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 13, 40, 248, 78, 152, 234, 235, 7, 169, 64, 158, 154, 160, 216, 234, 225, 174, 173, 105, 73, 144, 45, 155, 141, 117, 64, 117, 159, 62, 55, 236, 110, 43, 70, 240, 5, 181, 80, 41, 61, 25, 131, 133, 141, 83, 165, 136, 60, 38, 13, 183, 60, 237, 123, 89, 168, 119, 128, 100, 100, 85, 210, 6 }, new byte[] { 208, 111, 107, 245, 1, 42, 97, 243, 79, 254, 7, 22, 135, 234, 61, 249, 86, 128, 184, 187, 235, 213, 52, 75, 2, 123, 48, 189, 99, 139, 138, 5, 121, 80, 178, 3, 167, 65, 224, 69, 33, 12, 9, 98, 123, 130, 255, 64, 136, 203, 11, 157, 76, 68, 59, 119, 109, 192, 126, 214, 144, 41, 170, 39, 0, 128, 48, 184, 125, 217, 166, 195, 215, 181, 37, 150, 137, 172, 11, 61, 10, 143, 16, 235, 173, 24, 84, 170, 203, 127, 169, 123, 122, 63, 172, 47, 93, 69, 240, 195, 54, 122, 227, 235, 109, 76, 110, 99, 39, 132, 205, 58, 142, 18, 99, 178, 138, 205, 114, 32, 125, 220, 173, 185, 140, 50, 231, 21 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 13, 40, 248, 78, 152, 234, 235, 7, 169, 64, 158, 154, 160, 216, 234, 225, 174, 173, 105, 73, 144, 45, 155, 141, 117, 64, 117, 159, 62, 55, 236, 110, 43, 70, 240, 5, 181, 80, 41, 61, 25, 131, 133, 141, 83, 165, 136, 60, 38, 13, 183, 60, 237, 123, 89, 168, 119, 128, 100, 100, 85, 210, 6 }, new byte[] { 208, 111, 107, 245, 1, 42, 97, 243, 79, 254, 7, 22, 135, 234, 61, 249, 86, 128, 184, 187, 235, 213, 52, 75, 2, 123, 48, 189, 99, 139, 138, 5, 121, 80, 178, 3, 167, 65, 224, 69, 33, 12, 9, 98, 123, 130, 255, 64, 136, 203, 11, 157, 76, 68, 59, 119, 109, 192, 126, 214, 144, 41, 170, 39, 0, 128, 48, 184, 125, 217, 166, 195, 215, 181, 37, 150, 137, 172, 11, 61, 10, 143, 16, 235, 173, 24, 84, 170, 203, 127, 169, 123, 122, 63, 172, 47, 93, 69, 240, 195, 54, 122, 227, 235, 109, 76, 110, 99, 39, 132, 205, 58, 142, 18, 99, 178, 138, 205, 114, 32, 125, 220, 173, 185, 140, 50, 231, 21 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 69, 13, 40, 248, 78, 152, 234, 235, 7, 169, 64, 158, 154, 160, 216, 234, 225, 174, 173, 105, 73, 144, 45, 155, 141, 117, 64, 117, 159, 62, 55, 236, 110, 43, 70, 240, 5, 181, 80, 41, 61, 25, 131, 133, 141, 83, 165, 136, 60, 38, 13, 183, 60, 237, 123, 89, 168, 119, 128, 100, 100, 85, 210, 6 }, new byte[] { 208, 111, 107, 245, 1, 42, 97, 243, 79, 254, 7, 22, 135, 234, 61, 249, 86, 128, 184, 187, 235, 213, 52, 75, 2, 123, 48, 189, 99, 139, 138, 5, 121, 80, 178, 3, 167, 65, 224, 69, 33, 12, 9, 98, 123, 130, 255, 64, 136, 203, 11, 157, 76, 68, 59, 119, 109, 192, 126, 214, 144, 41, 170, 39, 0, 128, 48, 184, 125, 217, 166, 195, 215, 181, 37, 150, 137, 172, 11, 61, 10, 143, 16, 235, 173, 24, 84, 170, 203, 127, 169, 123, 122, 63, 172, 47, 93, 69, 240, 195, 54, 122, 227, 235, 109, 76, 110, 99, 39, 132, 205, 58, 142, 18, 99, 178, 138, 205, 114, 32, 125, 220, 173, 185, 140, 50, 231, 21 } });

            migrationBuilder.InsertData(
                table: "TicketInfos",
                columns: new[] { "Id", "Price", "TenantId", "TicketTypeId", "ZoneId" },
                values: new object[,]
                {
                    { 1, 1.5m, "mostar", 1, 1 },
                    { 2, 2.1m, "mostar", 1, 2 },
                    { 3, 2.9m, "mostar", 1, 3 },
                    { 4, 50.0m, "mostar", 2, 1 },
                    { 5, 75.0m, "mostar", 2, 2 },
                    { 6, 95.0m, "mostar", 2, 3 }
                });

            migrationBuilder.UpdateData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Single");

            migrationBuilder.UpdateData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Monthly");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2,
                column: "TicketInfoId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Zones",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Zone 1");

            migrationBuilder.UpdateData(
                table: "Zones",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Zone 2");

            migrationBuilder.UpdateData(
                table: "Zones",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Zone 3");

            migrationBuilder.CreateIndex(
                name: "IX_TicketInfos_TicketTypeId",
                table: "TicketInfos",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketInfos_ZoneId",
                table: "TicketInfos",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketInfos_TicketInfoId",
                table: "Tickets",
                column: "TicketInfoId",
                principalTable: "TicketInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketInfos_TicketInfoId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketInfos");

            migrationBuilder.RenameColumn(
                name: "TicketInfoId",
                table: "Tickets",
                newName: "ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_TicketInfoId",
                table: "Tickets",
                newName: "IX_Tickets_ZoneId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Zones",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TicketTypeId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 84, 158, 249, 143, 74, 75, 8, 88, 84, 202, 19, 165, 91, 218, 222, 137, 85, 197, 136, 95, 229, 17, 109, 196, 80, 95, 2, 130, 228, 166, 77, 47, 93, 113, 118, 244, 126, 168, 217, 201, 96, 231, 200, 197, 188, 80, 223, 201, 40, 249, 195, 104, 163, 154, 116, 81, 22, 31, 247, 28, 205, 62, 134, 95 }, new byte[] { 225, 235, 208, 119, 73, 203, 92, 36, 250, 19, 3, 67, 255, 248, 191, 126, 78, 213, 171, 41, 118, 163, 218, 255, 110, 239, 238, 104, 114, 235, 66, 196, 165, 91, 165, 205, 228, 220, 106, 107, 57, 224, 127, 40, 234, 34, 114, 241, 80, 172, 103, 164, 147, 233, 90, 45, 106, 59, 72, 154, 68, 41, 87, 119, 35, 238, 76, 229, 120, 81, 130, 134, 94, 76, 56, 201, 39, 68, 60, 231, 164, 183, 248, 164, 61, 58, 66, 218, 205, 66, 58, 109, 185, 254, 201, 103, 39, 12, 175, 211, 110, 134, 33, 172, 190, 140, 6, 174, 87, 156, 10, 177, 250, 143, 107, 2, 29, 239, 106, 116, 100, 248, 6, 143, 110, 144, 121, 96 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 84, 158, 249, 143, 74, 75, 8, 88, 84, 202, 19, 165, 91, 218, 222, 137, 85, 197, 136, 95, 229, 17, 109, 196, 80, 95, 2, 130, 228, 166, 77, 47, 93, 113, 118, 244, 126, 168, 217, 201, 96, 231, 200, 197, 188, 80, 223, 201, 40, 249, 195, 104, 163, 154, 116, 81, 22, 31, 247, 28, 205, 62, 134, 95 }, new byte[] { 225, 235, 208, 119, 73, 203, 92, 36, 250, 19, 3, 67, 255, 248, 191, 126, 78, 213, 171, 41, 118, 163, 218, 255, 110, 239, 238, 104, 114, 235, 66, 196, 165, 91, 165, 205, 228, 220, 106, 107, 57, 224, 127, 40, 234, 34, 114, 241, 80, 172, 103, 164, 147, 233, 90, 45, 106, 59, 72, 154, 68, 41, 87, 119, 35, 238, 76, 229, 120, 81, 130, 134, 94, 76, 56, 201, 39, 68, 60, 231, 164, 183, 248, 164, 61, 58, 66, 218, 205, 66, 58, 109, 185, 254, 201, 103, 39, 12, 175, 211, 110, 134, 33, 172, 190, 140, 6, 174, 87, 156, 10, 177, 250, 143, 107, 2, 29, 239, 106, 116, 100, 248, 6, 143, 110, 144, 121, 96 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 84, 158, 249, 143, 74, 75, 8, 88, 84, 202, 19, 165, 91, 218, 222, 137, 85, 197, 136, 95, 229, 17, 109, 196, 80, 95, 2, 130, 228, 166, 77, 47, 93, 113, 118, 244, 126, 168, 217, 201, 96, 231, 200, 197, 188, 80, 223, 201, 40, 249, 195, 104, 163, 154, 116, 81, 22, 31, 247, 28, 205, 62, 134, 95 }, new byte[] { 225, 235, 208, 119, 73, 203, 92, 36, 250, 19, 3, 67, 255, 248, 191, 126, 78, 213, 171, 41, 118, 163, 218, 255, 110, 239, 238, 104, 114, 235, 66, 196, 165, 91, 165, 205, 228, 220, 106, 107, 57, 224, 127, 40, 234, 34, 114, 241, 80, 172, 103, 164, 147, 233, 90, 45, 106, 59, 72, 154, 68, 41, 87, 119, 35, 238, 76, 229, 120, 81, 130, 134, 94, 76, 56, 201, 39, 68, 60, 231, 164, 183, 248, 164, 61, 58, 66, 218, 205, 66, 58, 109, 185, 254, 201, 103, 39, 12, 175, 211, 110, 134, 33, 172, 190, 140, 6, 174, 87, 156, 10, 177, 250, 143, 107, 2, 29, 239, 106, 116, 100, 248, 6, 143, 110, 144, 121, 96 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 84, 158, 249, 143, 74, 75, 8, 88, 84, 202, 19, 165, 91, 218, 222, 137, 85, 197, 136, 95, 229, 17, 109, 196, 80, 95, 2, 130, 228, 166, 77, 47, 93, 113, 118, 244, 126, 168, 217, 201, 96, 231, 200, 197, 188, 80, 223, 201, 40, 249, 195, 104, 163, 154, 116, 81, 22, 31, 247, 28, 205, 62, 134, 95 }, new byte[] { 225, 235, 208, 119, 73, 203, 92, 36, 250, 19, 3, 67, 255, 248, 191, 126, 78, 213, 171, 41, 118, 163, 218, 255, 110, 239, 238, 104, 114, 235, 66, 196, 165, 91, 165, 205, 228, 220, 106, 107, 57, 224, 127, 40, 234, 34, 114, 241, 80, 172, 103, 164, 147, 233, 90, 45, 106, 59, 72, 154, 68, 41, 87, 119, 35, 238, 76, 229, 120, 81, 130, 134, 94, 76, 56, 201, 39, 68, 60, 231, 164, 183, 248, 164, 61, 58, 66, 218, 205, 66, 58, 109, 185, 254, 201, 103, 39, 12, 175, 211, 110, 134, 33, 172, 190, 140, 6, 174, 87, 156, 10, 177, 250, 143, 107, 2, 29, 239, 106, 116, 100, 248, 6, 143, 110, 144, 121, 96 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 84, 158, 249, 143, 74, 75, 8, 88, 84, 202, 19, 165, 91, 218, 222, 137, 85, 197, 136, 95, 229, 17, 109, 196, 80, 95, 2, 130, 228, 166, 77, 47, 93, 113, 118, 244, 126, 168, 217, 201, 96, 231, 200, 197, 188, 80, 223, 201, 40, 249, 195, 104, 163, 154, 116, 81, 22, 31, 247, 28, 205, 62, 134, 95 }, new byte[] { 225, 235, 208, 119, 73, 203, 92, 36, 250, 19, 3, 67, 255, 248, 191, 126, 78, 213, 171, 41, 118, 163, 218, 255, 110, 239, 238, 104, 114, 235, 66, 196, 165, 91, 165, 205, 228, 220, 106, 107, 57, 224, 127, 40, 234, 34, 114, 241, 80, 172, 103, 164, 147, 233, 90, 45, 106, 59, 72, 154, 68, 41, 87, 119, 35, 238, 76, 229, 120, 81, 130, 134, 94, 76, 56, 201, 39, 68, 60, 231, 164, 183, 248, 164, 61, 58, 66, 218, 205, 66, 58, 109, 185, 254, 201, 103, 39, 12, 175, 211, 110, 134, 33, 172, 190, 140, 6, 174, 87, 156, 10, 177, 250, 143, 107, 2, 29, 239, 106, 116, 100, 248, 6, 143, 110, 144, 121, 96 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 84, 158, 249, 143, 74, 75, 8, 88, 84, 202, 19, 165, 91, 218, 222, 137, 85, 197, 136, 95, 229, 17, 109, 196, 80, 95, 2, 130, 228, 166, 77, 47, 93, 113, 118, 244, 126, 168, 217, 201, 96, 231, 200, 197, 188, 80, 223, 201, 40, 249, 195, 104, 163, 154, 116, 81, 22, 31, 247, 28, 205, 62, 134, 95 }, new byte[] { 225, 235, 208, 119, 73, 203, 92, 36, 250, 19, 3, 67, 255, 248, 191, 126, 78, 213, 171, 41, 118, 163, 218, 255, 110, 239, 238, 104, 114, 235, 66, 196, 165, 91, 165, 205, 228, 220, 106, 107, 57, 224, 127, 40, 234, 34, 114, 241, 80, 172, 103, 164, 147, 233, 90, 45, 106, 59, 72, 154, 68, 41, 87, 119, 35, 238, 76, 229, 120, 81, 130, 134, 94, 76, 56, 201, 39, 68, 60, 231, 164, 183, 248, 164, 61, 58, 66, 218, 205, 66, 58, 109, 185, 254, 201, 103, 39, 12, 175, 211, 110, 134, 33, 172, 190, 140, 6, 174, 87, 156, 10, 177, 250, 143, 107, 2, 29, 239, 106, 116, 100, 248, 6, 143, 110, 144, 121, 96 } });

            migrationBuilder.UpdateData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Basic");

            migrationBuilder.UpdateData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Advanced");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "TicketTypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "TicketTypeId", "ZoneId" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "Zones",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Price" },
                values: new object[] { "1", 1.5m });

            migrationBuilder.UpdateData(
                table: "Zones",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Price" },
                values: new object[] { "2", 2.1m });

            migrationBuilder.UpdateData(
                table: "Zones",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "Price" },
                values: new object[] { "3", 2.7m });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeId",
                table: "Tickets",
                column: "TicketTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketTypes_TicketTypeId",
                table: "Tickets",
                column: "TicketTypeId",
                principalTable: "TicketTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Zones_ZoneId",
                table: "Tickets",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id");
        }
    }
}

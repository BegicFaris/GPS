using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPS.API.Migrations
{
    /// <inheritdoc />
    public partial class editnotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 210, 9, 162, 22, 169, 40, 121, 77, 169, 155, 27, 167, 195, 219, 200, 159, 182, 135, 169, 37, 142, 185, 246, 38, 48, 227, 219, 229, 175, 229, 138, 54, 229, 249, 228, 20, 122, 135, 111, 53, 230, 191, 211, 227, 224, 217, 233, 155, 137, 223, 177, 185, 252, 77, 22, 17, 174, 144, 139, 155, 124, 215, 217, 178 }, new byte[] { 94, 112, 220, 33, 92, 118, 94, 64, 183, 91, 200, 175, 200, 28, 170, 198, 202, 109, 21, 160, 191, 9, 193, 70, 2, 192, 73, 61, 38, 72, 87, 177, 137, 226, 79, 132, 122, 60, 202, 184, 29, 57, 230, 15, 203, 218, 72, 90, 225, 87, 34, 6, 190, 201, 169, 86, 24, 57, 119, 203, 142, 35, 237, 161, 65, 154, 90, 49, 78, 154, 145, 71, 134, 200, 126, 53, 3, 82, 148, 170, 227, 129, 142, 205, 92, 182, 121, 141, 169, 249, 75, 224, 214, 185, 91, 182, 63, 113, 130, 75, 100, 53, 42, 129, 97, 153, 23, 48, 202, 115, 46, 222, 150, 45, 130, 190, 241, 245, 133, 160, 175, 102, 169, 228, 38, 168, 39, 143 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 210, 9, 162, 22, 169, 40, 121, 77, 169, 155, 27, 167, 195, 219, 200, 159, 182, 135, 169, 37, 142, 185, 246, 38, 48, 227, 219, 229, 175, 229, 138, 54, 229, 249, 228, 20, 122, 135, 111, 53, 230, 191, 211, 227, 224, 217, 233, 155, 137, 223, 177, 185, 252, 77, 22, 17, 174, 144, 139, 155, 124, 215, 217, 178 }, new byte[] { 94, 112, 220, 33, 92, 118, 94, 64, 183, 91, 200, 175, 200, 28, 170, 198, 202, 109, 21, 160, 191, 9, 193, 70, 2, 192, 73, 61, 38, 72, 87, 177, 137, 226, 79, 132, 122, 60, 202, 184, 29, 57, 230, 15, 203, 218, 72, 90, 225, 87, 34, 6, 190, 201, 169, 86, 24, 57, 119, 203, 142, 35, 237, 161, 65, 154, 90, 49, 78, 154, 145, 71, 134, 200, 126, 53, 3, 82, 148, 170, 227, 129, 142, 205, 92, 182, 121, 141, 169, 249, 75, 224, 214, 185, 91, 182, 63, 113, 130, 75, 100, 53, 42, 129, 97, 153, 23, 48, 202, 115, 46, 222, 150, 45, 130, 190, 241, 245, 133, 160, 175, 102, 169, 228, 38, 168, 39, 143 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 210, 9, 162, 22, 169, 40, 121, 77, 169, 155, 27, 167, 195, 219, 200, 159, 182, 135, 169, 37, 142, 185, 246, 38, 48, 227, 219, 229, 175, 229, 138, 54, 229, 249, 228, 20, 122, 135, 111, 53, 230, 191, 211, 227, 224, 217, 233, 155, 137, 223, 177, 185, 252, 77, 22, 17, 174, 144, 139, 155, 124, 215, 217, 178 }, new byte[] { 94, 112, 220, 33, 92, 118, 94, 64, 183, 91, 200, 175, 200, 28, 170, 198, 202, 109, 21, 160, 191, 9, 193, 70, 2, 192, 73, 61, 38, 72, 87, 177, 137, 226, 79, 132, 122, 60, 202, 184, 29, 57, 230, 15, 203, 218, 72, 90, 225, 87, 34, 6, 190, 201, 169, 86, 24, 57, 119, 203, 142, 35, 237, 161, 65, 154, 90, 49, 78, 154, 145, 71, 134, 200, 126, 53, 3, 82, 148, 170, 227, 129, 142, 205, 92, 182, 121, 141, 169, 249, 75, 224, 214, 185, 91, 182, 63, 113, 130, 75, 100, 53, 42, 129, 97, 153, 23, 48, 202, 115, 46, 222, 150, 45, 130, 190, 241, 245, 133, 160, 175, 102, 169, 228, 38, 168, 39, 143 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 210, 9, 162, 22, 169, 40, 121, 77, 169, 155, 27, 167, 195, 219, 200, 159, 182, 135, 169, 37, 142, 185, 246, 38, 48, 227, 219, 229, 175, 229, 138, 54, 229, 249, 228, 20, 122, 135, 111, 53, 230, 191, 211, 227, 224, 217, 233, 155, 137, 223, 177, 185, 252, 77, 22, 17, 174, 144, 139, 155, 124, 215, 217, 178 }, new byte[] { 94, 112, 220, 33, 92, 118, 94, 64, 183, 91, 200, 175, 200, 28, 170, 198, 202, 109, 21, 160, 191, 9, 193, 70, 2, 192, 73, 61, 38, 72, 87, 177, 137, 226, 79, 132, 122, 60, 202, 184, 29, 57, 230, 15, 203, 218, 72, 90, 225, 87, 34, 6, 190, 201, 169, 86, 24, 57, 119, 203, 142, 35, 237, 161, 65, 154, 90, 49, 78, 154, 145, 71, 134, 200, 126, 53, 3, 82, 148, 170, 227, 129, 142, 205, 92, 182, 121, 141, 169, 249, 75, 224, 214, 185, 91, 182, 63, 113, 130, 75, 100, 53, 42, 129, 97, 153, 23, 48, 202, 115, 46, 222, 150, 45, 130, 190, 241, 245, 133, 160, 175, 102, 169, 228, 38, 168, 39, 143 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 210, 9, 162, 22, 169, 40, 121, 77, 169, 155, 27, 167, 195, 219, 200, 159, 182, 135, 169, 37, 142, 185, 246, 38, 48, 227, 219, 229, 175, 229, 138, 54, 229, 249, 228, 20, 122, 135, 111, 53, 230, 191, 211, 227, 224, 217, 233, 155, 137, 223, 177, 185, 252, 77, 22, 17, 174, 144, 139, 155, 124, 215, 217, 178 }, new byte[] { 94, 112, 220, 33, 92, 118, 94, 64, 183, 91, 200, 175, 200, 28, 170, 198, 202, 109, 21, 160, 191, 9, 193, 70, 2, 192, 73, 61, 38, 72, 87, 177, 137, 226, 79, 132, 122, 60, 202, 184, 29, 57, 230, 15, 203, 218, 72, 90, 225, 87, 34, 6, 190, 201, 169, 86, 24, 57, 119, 203, 142, 35, 237, 161, 65, 154, 90, 49, 78, 154, 145, 71, 134, 200, 126, 53, 3, 82, 148, 170, 227, 129, 142, 205, 92, 182, 121, 141, 169, 249, 75, 224, 214, 185, 91, 182, 63, 113, 130, 75, 100, 53, 42, 129, 97, 153, 23, 48, 202, 115, 46, 222, 150, 45, 130, 190, 241, 245, 133, 160, 175, 102, 169, 228, 38, 168, 39, 143 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 210, 9, 162, 22, 169, 40, 121, 77, 169, 155, 27, 167, 195, 219, 200, 159, 182, 135, 169, 37, 142, 185, 246, 38, 48, 227, 219, 229, 175, 229, 138, 54, 229, 249, 228, 20, 122, 135, 111, 53, 230, 191, 211, 227, 224, 217, 233, 155, 137, 223, 177, 185, 252, 77, 22, 17, 174, 144, 139, 155, 124, 215, 217, 178 }, new byte[] { 94, 112, 220, 33, 92, 118, 94, 64, 183, 91, 200, 175, 200, 28, 170, 198, 202, 109, 21, 160, 191, 9, 193, 70, 2, 192, 73, 61, 38, 72, 87, 177, 137, 226, 79, 132, 122, 60, 202, 184, 29, 57, 230, 15, 203, 218, 72, 90, 225, 87, 34, 6, 190, 201, 169, 86, 24, 57, 119, 203, 142, 35, 237, 161, 65, 154, 90, 49, 78, 154, 145, 71, 134, 200, 126, 53, 3, 82, 148, 170, 227, 129, 142, 205, 92, 182, 121, 141, 169, 249, 75, 224, 214, 185, 91, 182, 63, 113, 130, 75, 100, 53, 42, 129, 97, 153, 23, 48, 202, 115, 46, 222, 150, 45, 130, 190, 241, 245, 133, 160, 175, 102, 169, 228, 38, 168, 39, 143 } });

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreationDate",
                table: "Notifications",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 110, 95, 249, 181, 183, 137, 244, 63, 4, 199, 81, 31, 239, 195, 73, 120, 172, 79, 130, 198, 198, 12, 70, 129, 90, 138, 235, 154, 79, 243, 229, 6, 238, 245, 185, 216, 128, 6, 48, 185, 83, 52, 149, 133, 122, 127, 114, 255, 252, 20, 43, 28, 185, 88, 187, 100, 65, 87, 71, 28, 51, 49, 172, 51 }, new byte[] { 35, 125, 146, 80, 193, 208, 246, 176, 100, 126, 55, 168, 235, 69, 83, 70, 14, 237, 224, 229, 255, 148, 121, 142, 98, 207, 202, 77, 145, 183, 14, 13, 199, 22, 52, 189, 245, 241, 30, 189, 51, 90, 191, 23, 139, 117, 233, 183, 134, 139, 93, 15, 144, 192, 90, 229, 47, 230, 254, 61, 194, 44, 64, 202, 22, 127, 45, 71, 197, 117, 166, 124, 40, 113, 156, 49, 34, 145, 123, 248, 91, 30, 181, 113, 13, 43, 160, 84, 198, 145, 236, 47, 241, 4, 128, 121, 41, 12, 22, 130, 44, 205, 144, 57, 227, 86, 94, 96, 99, 201, 171, 232, 135, 95, 109, 81, 66, 62, 26, 63, 182, 204, 17, 227, 175, 22, 114, 136 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 110, 95, 249, 181, 183, 137, 244, 63, 4, 199, 81, 31, 239, 195, 73, 120, 172, 79, 130, 198, 198, 12, 70, 129, 90, 138, 235, 154, 79, 243, 229, 6, 238, 245, 185, 216, 128, 6, 48, 185, 83, 52, 149, 133, 122, 127, 114, 255, 252, 20, 43, 28, 185, 88, 187, 100, 65, 87, 71, 28, 51, 49, 172, 51 }, new byte[] { 35, 125, 146, 80, 193, 208, 246, 176, 100, 126, 55, 168, 235, 69, 83, 70, 14, 237, 224, 229, 255, 148, 121, 142, 98, 207, 202, 77, 145, 183, 14, 13, 199, 22, 52, 189, 245, 241, 30, 189, 51, 90, 191, 23, 139, 117, 233, 183, 134, 139, 93, 15, 144, 192, 90, 229, 47, 230, 254, 61, 194, 44, 64, 202, 22, 127, 45, 71, 197, 117, 166, 124, 40, 113, 156, 49, 34, 145, 123, 248, 91, 30, 181, 113, 13, 43, 160, 84, 198, 145, 236, 47, 241, 4, 128, 121, 41, 12, 22, 130, 44, 205, 144, 57, 227, 86, 94, 96, 99, 201, 171, 232, 135, 95, 109, 81, 66, 62, 26, 63, 182, 204, 17, 227, 175, 22, 114, 136 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 110, 95, 249, 181, 183, 137, 244, 63, 4, 199, 81, 31, 239, 195, 73, 120, 172, 79, 130, 198, 198, 12, 70, 129, 90, 138, 235, 154, 79, 243, 229, 6, 238, 245, 185, 216, 128, 6, 48, 185, 83, 52, 149, 133, 122, 127, 114, 255, 252, 20, 43, 28, 185, 88, 187, 100, 65, 87, 71, 28, 51, 49, 172, 51 }, new byte[] { 35, 125, 146, 80, 193, 208, 246, 176, 100, 126, 55, 168, 235, 69, 83, 70, 14, 237, 224, 229, 255, 148, 121, 142, 98, 207, 202, 77, 145, 183, 14, 13, 199, 22, 52, 189, 245, 241, 30, 189, 51, 90, 191, 23, 139, 117, 233, 183, 134, 139, 93, 15, 144, 192, 90, 229, 47, 230, 254, 61, 194, 44, 64, 202, 22, 127, 45, 71, 197, 117, 166, 124, 40, 113, 156, 49, 34, 145, 123, 248, 91, 30, 181, 113, 13, 43, 160, 84, 198, 145, 236, 47, 241, 4, 128, 121, 41, 12, 22, 130, 44, 205, 144, 57, 227, 86, 94, 96, 99, 201, 171, 232, 135, 95, 109, 81, 66, 62, 26, 63, 182, 204, 17, 227, 175, 22, 114, 136 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 110, 95, 249, 181, 183, 137, 244, 63, 4, 199, 81, 31, 239, 195, 73, 120, 172, 79, 130, 198, 198, 12, 70, 129, 90, 138, 235, 154, 79, 243, 229, 6, 238, 245, 185, 216, 128, 6, 48, 185, 83, 52, 149, 133, 122, 127, 114, 255, 252, 20, 43, 28, 185, 88, 187, 100, 65, 87, 71, 28, 51, 49, 172, 51 }, new byte[] { 35, 125, 146, 80, 193, 208, 246, 176, 100, 126, 55, 168, 235, 69, 83, 70, 14, 237, 224, 229, 255, 148, 121, 142, 98, 207, 202, 77, 145, 183, 14, 13, 199, 22, 52, 189, 245, 241, 30, 189, 51, 90, 191, 23, 139, 117, 233, 183, 134, 139, 93, 15, 144, 192, 90, 229, 47, 230, 254, 61, 194, 44, 64, 202, 22, 127, 45, 71, 197, 117, 166, 124, 40, 113, 156, 49, 34, 145, 123, 248, 91, 30, 181, 113, 13, 43, 160, 84, 198, 145, 236, 47, 241, 4, 128, 121, 41, 12, 22, 130, 44, 205, 144, 57, 227, 86, 94, 96, 99, 201, 171, 232, 135, 95, 109, 81, 66, 62, 26, 63, 182, 204, 17, 227, 175, 22, 114, 136 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 110, 95, 249, 181, 183, 137, 244, 63, 4, 199, 81, 31, 239, 195, 73, 120, 172, 79, 130, 198, 198, 12, 70, 129, 90, 138, 235, 154, 79, 243, 229, 6, 238, 245, 185, 216, 128, 6, 48, 185, 83, 52, 149, 133, 122, 127, 114, 255, 252, 20, 43, 28, 185, 88, 187, 100, 65, 87, 71, 28, 51, 49, 172, 51 }, new byte[] { 35, 125, 146, 80, 193, 208, 246, 176, 100, 126, 55, 168, 235, 69, 83, 70, 14, 237, 224, 229, 255, 148, 121, 142, 98, 207, 202, 77, 145, 183, 14, 13, 199, 22, 52, 189, 245, 241, 30, 189, 51, 90, 191, 23, 139, 117, 233, 183, 134, 139, 93, 15, 144, 192, 90, 229, 47, 230, 254, 61, 194, 44, 64, 202, 22, 127, 45, 71, 197, 117, 166, 124, 40, 113, 156, 49, 34, 145, 123, 248, 91, 30, 181, 113, 13, 43, 160, 84, 198, 145, 236, 47, 241, 4, 128, 121, 41, 12, 22, 130, 44, 205, 144, 57, 227, 86, 94, 96, 99, 201, 171, 232, 135, 95, 109, 81, 66, 62, 26, 63, 182, 204, 17, 227, 175, 22, 114, 136 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 110, 95, 249, 181, 183, 137, 244, 63, 4, 199, 81, 31, 239, 195, 73, 120, 172, 79, 130, 198, 198, 12, 70, 129, 90, 138, 235, 154, 79, 243, 229, 6, 238, 245, 185, 216, 128, 6, 48, 185, 83, 52, 149, 133, 122, 127, 114, 255, 252, 20, 43, 28, 185, 88, 187, 100, 65, 87, 71, 28, 51, 49, 172, 51 }, new byte[] { 35, 125, 146, 80, 193, 208, 246, 176, 100, 126, 55, 168, 235, 69, 83, 70, 14, 237, 224, 229, 255, 148, 121, 142, 98, 207, 202, 77, 145, 183, 14, 13, 199, 22, 52, 189, 245, 241, 30, 189, 51, 90, 191, 23, 139, 117, 233, 183, 134, 139, 93, 15, 144, 192, 90, 229, 47, 230, 254, 61, 194, 44, 64, 202, 22, 127, 45, 71, 197, 117, 166, 124, 40, 113, 156, 49, 34, 145, 123, 248, 91, 30, 181, 113, 13, 43, 160, 84, 198, 145, 236, 47, 241, 4, 128, 121, 41, 12, 22, 130, 44, 205, 144, 57, 227, 86, 94, 96, 99, 201, 171, 232, 135, 95, 109, 81, 66, 62, 26, 63, 182, 204, 17, 227, 175, 22, 114, 136 } });

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateOnly(2024, 1, 1));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateOnly(2024, 1, 1));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateOnly(2024, 1, 1));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateOnly(2024, 1, 1));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateOnly(2024, 1, 1));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateOnly(2024, 1, 1));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateOnly(2024, 1, 1));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateOnly(2024, 1, 1));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateOnly(2024, 1, 1));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateOnly(2025, 1, 1));
        }
    }
}

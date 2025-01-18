using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPS.API.Migrations
{
    /// <inheritdoc />
    public partial class Addingtestda43t3a2e : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 156, 24, 180, 179, 210, 244, 238, 38, 11, 160, 137, 158, 81, 1, 96, 84, 255, 13, 77, 119, 113, 170, 170, 129, 44, 75, 17, 233, 164, 109, 209, 174, 129, 60, 57, 98, 54, 179, 17, 121, 151, 128, 32, 98, 224, 229, 92, 168, 241, 235, 64, 87, 139, 154, 40, 91, 152, 183, 153, 71, 234, 50, 188, 29 }, new byte[] { 82, 110, 140, 220, 10, 162, 1, 1, 138, 107, 164, 240, 146, 149, 203, 161, 227, 137, 143, 241, 229, 67, 249, 224, 149, 168, 94, 16, 124, 59, 235, 137, 124, 56, 40, 37, 29, 1, 191, 187, 43, 159, 184, 213, 126, 47, 54, 236, 166, 88, 242, 74, 229, 153, 69, 79, 187, 151, 241, 104, 239, 136, 120, 238, 190, 11, 224, 242, 46, 162, 143, 10, 230, 213, 32, 77, 100, 184, 132, 137, 234, 193, 47, 197, 37, 254, 26, 8, 64, 155, 205, 204, 211, 208, 28, 68, 177, 174, 147, 225, 40, 252, 100, 197, 194, 11, 153, 253, 189, 237, 117, 61, 158, 22, 8, 100, 202, 168, 58, 68, 108, 42, 60, 74, 6, 7, 145, 51 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 156, 24, 180, 179, 210, 244, 238, 38, 11, 160, 137, 158, 81, 1, 96, 84, 255, 13, 77, 119, 113, 170, 170, 129, 44, 75, 17, 233, 164, 109, 209, 174, 129, 60, 57, 98, 54, 179, 17, 121, 151, 128, 32, 98, 224, 229, 92, 168, 241, 235, 64, 87, 139, 154, 40, 91, 152, 183, 153, 71, 234, 50, 188, 29 }, new byte[] { 82, 110, 140, 220, 10, 162, 1, 1, 138, 107, 164, 240, 146, 149, 203, 161, 227, 137, 143, 241, 229, 67, 249, 224, 149, 168, 94, 16, 124, 59, 235, 137, 124, 56, 40, 37, 29, 1, 191, 187, 43, 159, 184, 213, 126, 47, 54, 236, 166, 88, 242, 74, 229, 153, 69, 79, 187, 151, 241, 104, 239, 136, 120, 238, 190, 11, 224, 242, 46, 162, 143, 10, 230, 213, 32, 77, 100, 184, 132, 137, 234, 193, 47, 197, 37, 254, 26, 8, 64, 155, 205, 204, 211, 208, 28, 68, 177, 174, 147, 225, 40, 252, 100, 197, 194, 11, 153, 253, 189, 237, 117, 61, 158, 22, 8, 100, 202, 168, 58, 68, 108, 42, 60, 74, 6, 7, 145, 51 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 156, 24, 180, 179, 210, 244, 238, 38, 11, 160, 137, 158, 81, 1, 96, 84, 255, 13, 77, 119, 113, 170, 170, 129, 44, 75, 17, 233, 164, 109, 209, 174, 129, 60, 57, 98, 54, 179, 17, 121, 151, 128, 32, 98, 224, 229, 92, 168, 241, 235, 64, 87, 139, 154, 40, 91, 152, 183, 153, 71, 234, 50, 188, 29 }, new byte[] { 82, 110, 140, 220, 10, 162, 1, 1, 138, 107, 164, 240, 146, 149, 203, 161, 227, 137, 143, 241, 229, 67, 249, 224, 149, 168, 94, 16, 124, 59, 235, 137, 124, 56, 40, 37, 29, 1, 191, 187, 43, 159, 184, 213, 126, 47, 54, 236, 166, 88, 242, 74, 229, 153, 69, 79, 187, 151, 241, 104, 239, 136, 120, 238, 190, 11, 224, 242, 46, 162, 143, 10, 230, 213, 32, 77, 100, 184, 132, 137, 234, 193, 47, 197, 37, 254, 26, 8, 64, 155, 205, 204, 211, 208, 28, 68, 177, 174, 147, 225, 40, 252, 100, 197, 194, 11, 153, 253, 189, 237, 117, 61, 158, 22, 8, 100, 202, 168, 58, 68, 108, 42, 60, 74, 6, 7, 145, 51 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 156, 24, 180, 179, 210, 244, 238, 38, 11, 160, 137, 158, 81, 1, 96, 84, 255, 13, 77, 119, 113, 170, 170, 129, 44, 75, 17, 233, 164, 109, 209, 174, 129, 60, 57, 98, 54, 179, 17, 121, 151, 128, 32, 98, 224, 229, 92, 168, 241, 235, 64, 87, 139, 154, 40, 91, 152, 183, 153, 71, 234, 50, 188, 29 }, new byte[] { 82, 110, 140, 220, 10, 162, 1, 1, 138, 107, 164, 240, 146, 149, 203, 161, 227, 137, 143, 241, 229, 67, 249, 224, 149, 168, 94, 16, 124, 59, 235, 137, 124, 56, 40, 37, 29, 1, 191, 187, 43, 159, 184, 213, 126, 47, 54, 236, 166, 88, 242, 74, 229, 153, 69, 79, 187, 151, 241, 104, 239, 136, 120, 238, 190, 11, 224, 242, 46, 162, 143, 10, 230, 213, 32, 77, 100, 184, 132, 137, 234, 193, 47, 197, 37, 254, 26, 8, 64, 155, 205, 204, 211, 208, 28, 68, 177, 174, 147, 225, 40, 252, 100, 197, 194, 11, 153, 253, 189, 237, 117, 61, 158, 22, 8, 100, 202, 168, 58, 68, 108, 42, 60, 74, 6, 7, 145, 51 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 156, 24, 180, 179, 210, 244, 238, 38, 11, 160, 137, 158, 81, 1, 96, 84, 255, 13, 77, 119, 113, 170, 170, 129, 44, 75, 17, 233, 164, 109, 209, 174, 129, 60, 57, 98, 54, 179, 17, 121, 151, 128, 32, 98, 224, 229, 92, 168, 241, 235, 64, 87, 139, 154, 40, 91, 152, 183, 153, 71, 234, 50, 188, 29 }, new byte[] { 82, 110, 140, 220, 10, 162, 1, 1, 138, 107, 164, 240, 146, 149, 203, 161, 227, 137, 143, 241, 229, 67, 249, 224, 149, 168, 94, 16, 124, 59, 235, 137, 124, 56, 40, 37, 29, 1, 191, 187, 43, 159, 184, 213, 126, 47, 54, 236, 166, 88, 242, 74, 229, 153, 69, 79, 187, 151, 241, 104, 239, 136, 120, 238, 190, 11, 224, 242, 46, 162, 143, 10, 230, 213, 32, 77, 100, 184, 132, 137, 234, 193, 47, 197, 37, 254, 26, 8, 64, 155, 205, 204, 211, 208, 28, 68, 177, 174, 147, 225, 40, 252, 100, 197, 194, 11, 153, 253, 189, 237, 117, 61, 158, 22, 8, 100, 202, 168, 58, 68, 108, 42, 60, 74, 6, 7, 145, 51 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 156, 24, 180, 179, 210, 244, 238, 38, 11, 160, 137, 158, 81, 1, 96, 84, 255, 13, 77, 119, 113, 170, 170, 129, 44, 75, 17, 233, 164, 109, 209, 174, 129, 60, 57, 98, 54, 179, 17, 121, 151, 128, 32, 98, 224, 229, 92, 168, 241, 235, 64, 87, 139, 154, 40, 91, 152, 183, 153, 71, 234, 50, 188, 29 }, new byte[] { 82, 110, 140, 220, 10, 162, 1, 1, 138, 107, 164, 240, 146, 149, 203, 161, 227, 137, 143, 241, 229, 67, 249, 224, 149, 168, 94, 16, 124, 59, 235, 137, 124, 56, 40, 37, 29, 1, 191, 187, 43, 159, 184, 213, 126, 47, 54, 236, 166, 88, 242, 74, 229, 153, 69, 79, 187, 151, 241, 104, 239, 136, 120, 238, 190, 11, 224, 242, 46, 162, 143, 10, 230, 213, 32, 77, 100, 184, 132, 137, 234, 193, 47, 197, 37, 254, 26, 8, 64, 155, 205, 204, 211, 208, 28, 68, 177, 174, 147, 225, 40, 252, 100, 197, 194, 11, 153, 253, 189, 237, 117, 61, 158, 22, 8, 100, 202, 168, 58, 68, 108, 42, 60, 74, 6, 7, 145, 51 } });

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 10, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 15, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 8, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 11, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 5,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 7, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 6,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 13, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 7,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 10, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 8,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 15, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 9,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 8, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 10,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(12, 0, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 11,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 9, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 12,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 16, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 13,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 14, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 14,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 11, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 15,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 9, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 16,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 8, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 17,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 12, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 18,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 15, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 19,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 10, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 20,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 14, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 21,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 7, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 22,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 11, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 23,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 10, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 24,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 9, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 25,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 10, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 26,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 15, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 27,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 9, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 28,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 8, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 29,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 12, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 30,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 14, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 31,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 8, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 32,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 11, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 33,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 9, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 34,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 13, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 35,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 11, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 36,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 6, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 37,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 14, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 38,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 7, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 39,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 9, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 40,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 13, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 41,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 10, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 42,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 12, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 43,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 8, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 44,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 9, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 45,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 7, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 46,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 9, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 47,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 15, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 48,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 10, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 49,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 7, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 50,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 10, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 51,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 6, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 52,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 9, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 53,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 8, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 54,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 13, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 55,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 12, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 145, 95, 200, 135, 43, 7, 6, 35, 145, 145, 25, 171, 154, 55, 169, 162, 188, 36, 101, 169, 219, 42, 186, 159, 244, 181, 195, 135, 158, 36, 185, 144, 19, 208, 137, 55, 67, 70, 85, 202, 155, 116, 124, 34, 150, 131, 119, 139, 47, 43, 60, 86, 239, 249, 239, 15, 41, 246, 50, 115, 150, 220, 94, 149 }, new byte[] { 166, 197, 129, 234, 221, 54, 154, 1, 53, 32, 72, 160, 234, 184, 57, 14, 167, 188, 142, 47, 114, 189, 12, 144, 117, 99, 175, 121, 213, 208, 15, 13, 156, 39, 46, 5, 42, 76, 176, 141, 79, 57, 84, 129, 177, 95, 141, 147, 195, 142, 44, 102, 148, 133, 219, 100, 176, 195, 158, 88, 50, 198, 74, 106, 168, 76, 4, 5, 120, 175, 3, 40, 143, 249, 43, 139, 226, 83, 47, 54, 208, 152, 97, 178, 35, 246, 236, 178, 192, 110, 171, 65, 64, 188, 46, 143, 184, 0, 98, 168, 4, 189, 117, 121, 191, 52, 220, 221, 240, 100, 163, 44, 202, 144, 152, 2, 42, 199, 167, 127, 196, 85, 177, 10, 113, 117, 251, 49 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 145, 95, 200, 135, 43, 7, 6, 35, 145, 145, 25, 171, 154, 55, 169, 162, 188, 36, 101, 169, 219, 42, 186, 159, 244, 181, 195, 135, 158, 36, 185, 144, 19, 208, 137, 55, 67, 70, 85, 202, 155, 116, 124, 34, 150, 131, 119, 139, 47, 43, 60, 86, 239, 249, 239, 15, 41, 246, 50, 115, 150, 220, 94, 149 }, new byte[] { 166, 197, 129, 234, 221, 54, 154, 1, 53, 32, 72, 160, 234, 184, 57, 14, 167, 188, 142, 47, 114, 189, 12, 144, 117, 99, 175, 121, 213, 208, 15, 13, 156, 39, 46, 5, 42, 76, 176, 141, 79, 57, 84, 129, 177, 95, 141, 147, 195, 142, 44, 102, 148, 133, 219, 100, 176, 195, 158, 88, 50, 198, 74, 106, 168, 76, 4, 5, 120, 175, 3, 40, 143, 249, 43, 139, 226, 83, 47, 54, 208, 152, 97, 178, 35, 246, 236, 178, 192, 110, 171, 65, 64, 188, 46, 143, 184, 0, 98, 168, 4, 189, 117, 121, 191, 52, 220, 221, 240, 100, 163, 44, 202, 144, 152, 2, 42, 199, 167, 127, 196, 85, 177, 10, 113, 117, 251, 49 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 145, 95, 200, 135, 43, 7, 6, 35, 145, 145, 25, 171, 154, 55, 169, 162, 188, 36, 101, 169, 219, 42, 186, 159, 244, 181, 195, 135, 158, 36, 185, 144, 19, 208, 137, 55, 67, 70, 85, 202, 155, 116, 124, 34, 150, 131, 119, 139, 47, 43, 60, 86, 239, 249, 239, 15, 41, 246, 50, 115, 150, 220, 94, 149 }, new byte[] { 166, 197, 129, 234, 221, 54, 154, 1, 53, 32, 72, 160, 234, 184, 57, 14, 167, 188, 142, 47, 114, 189, 12, 144, 117, 99, 175, 121, 213, 208, 15, 13, 156, 39, 46, 5, 42, 76, 176, 141, 79, 57, 84, 129, 177, 95, 141, 147, 195, 142, 44, 102, 148, 133, 219, 100, 176, 195, 158, 88, 50, 198, 74, 106, 168, 76, 4, 5, 120, 175, 3, 40, 143, 249, 43, 139, 226, 83, 47, 54, 208, 152, 97, 178, 35, 246, 236, 178, 192, 110, 171, 65, 64, 188, 46, 143, 184, 0, 98, 168, 4, 189, 117, 121, 191, 52, 220, 221, 240, 100, 163, 44, 202, 144, 152, 2, 42, 199, 167, 127, 196, 85, 177, 10, 113, 117, 251, 49 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 145, 95, 200, 135, 43, 7, 6, 35, 145, 145, 25, 171, 154, 55, 169, 162, 188, 36, 101, 169, 219, 42, 186, 159, 244, 181, 195, 135, 158, 36, 185, 144, 19, 208, 137, 55, 67, 70, 85, 202, 155, 116, 124, 34, 150, 131, 119, 139, 47, 43, 60, 86, 239, 249, 239, 15, 41, 246, 50, 115, 150, 220, 94, 149 }, new byte[] { 166, 197, 129, 234, 221, 54, 154, 1, 53, 32, 72, 160, 234, 184, 57, 14, 167, 188, 142, 47, 114, 189, 12, 144, 117, 99, 175, 121, 213, 208, 15, 13, 156, 39, 46, 5, 42, 76, 176, 141, 79, 57, 84, 129, 177, 95, 141, 147, 195, 142, 44, 102, 148, 133, 219, 100, 176, 195, 158, 88, 50, 198, 74, 106, 168, 76, 4, 5, 120, 175, 3, 40, 143, 249, 43, 139, 226, 83, 47, 54, 208, 152, 97, 178, 35, 246, 236, 178, 192, 110, 171, 65, 64, 188, 46, 143, 184, 0, 98, 168, 4, 189, 117, 121, 191, 52, 220, 221, 240, 100, 163, 44, 202, 144, 152, 2, 42, 199, 167, 127, 196, 85, 177, 10, 113, 117, 251, 49 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 145, 95, 200, 135, 43, 7, 6, 35, 145, 145, 25, 171, 154, 55, 169, 162, 188, 36, 101, 169, 219, 42, 186, 159, 244, 181, 195, 135, 158, 36, 185, 144, 19, 208, 137, 55, 67, 70, 85, 202, 155, 116, 124, 34, 150, 131, 119, 139, 47, 43, 60, 86, 239, 249, 239, 15, 41, 246, 50, 115, 150, 220, 94, 149 }, new byte[] { 166, 197, 129, 234, 221, 54, 154, 1, 53, 32, 72, 160, 234, 184, 57, 14, 167, 188, 142, 47, 114, 189, 12, 144, 117, 99, 175, 121, 213, 208, 15, 13, 156, 39, 46, 5, 42, 76, 176, 141, 79, 57, 84, 129, 177, 95, 141, 147, 195, 142, 44, 102, 148, 133, 219, 100, 176, 195, 158, 88, 50, 198, 74, 106, 168, 76, 4, 5, 120, 175, 3, 40, 143, 249, 43, 139, 226, 83, 47, 54, 208, 152, 97, 178, 35, 246, 236, 178, 192, 110, 171, 65, 64, 188, 46, 143, 184, 0, 98, 168, 4, 189, 117, 121, 191, 52, 220, 221, 240, 100, 163, 44, 202, 144, 152, 2, 42, 199, 167, 127, 196, 85, 177, 10, 113, 117, 251, 49 } });

            migrationBuilder.UpdateData(
                table: "MyAppUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 145, 95, 200, 135, 43, 7, 6, 35, 145, 145, 25, 171, 154, 55, 169, 162, 188, 36, 101, 169, 219, 42, 186, 159, 244, 181, 195, 135, 158, 36, 185, 144, 19, 208, 137, 55, 67, 70, 85, 202, 155, 116, 124, 34, 150, 131, 119, 139, 47, 43, 60, 86, 239, 249, 239, 15, 41, 246, 50, 115, 150, 220, 94, 149 }, new byte[] { 166, 197, 129, 234, 221, 54, 154, 1, 53, 32, 72, 160, 234, 184, 57, 14, 167, 188, 142, 47, 114, 189, 12, 144, 117, 99, 175, 121, 213, 208, 15, 13, 156, 39, 46, 5, 42, 76, 176, 141, 79, 57, 84, 129, 177, 95, 141, 147, 195, 142, 44, 102, 148, 133, 219, 100, 176, 195, 158, 88, 50, 198, 74, 106, 168, 76, 4, 5, 120, 175, 3, 40, 143, 249, 43, 139, 226, 83, 47, 54, 208, 152, 97, 178, 35, 246, 236, 178, 192, 110, 171, 65, 64, 188, 46, 143, 184, 0, 98, 168, 4, 189, 117, 121, 191, 52, 220, 221, 240, 100, 163, 44, 202, 144, 152, 2, 42, 199, 167, 127, 196, 85, 177, 10, 113, 117, 251, 49 } });

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 10));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 15));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 8));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 11));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 5,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 7));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 6,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 13));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 7,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 10));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 8,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 15));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 9,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 8));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 10,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 12));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 11,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 9));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 12,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 16));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 13,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 14));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 14,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 11));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 15,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 9));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 16,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 8));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 17,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 12));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 18,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 15));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 19,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 10));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 20,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 14));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 21,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 7));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 22,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 11));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 23,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 10));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 24,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 9));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 25,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 10));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 26,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 15));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 27,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 9));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 28,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 8));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 29,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 12));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 30,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 14));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 31,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 8));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 32,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 11));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 33,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 9));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 34,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 13));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 35,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 11));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 36,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 6));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 37,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 14));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 38,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 7));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 39,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 9));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 40,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 13));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 41,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 10));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 42,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 12));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 43,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 8));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 44,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 9));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 45,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 7));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 46,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 9));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 47,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 15));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 48,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 10));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 49,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 7));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 50,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 10));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 51,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 6));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 52,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 9));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 53,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 8));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 54,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 13));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 55,
                column: "DistanceFromTheNextStation",
                value: new TimeOnly(0, 0, 12));
        }
    }
}

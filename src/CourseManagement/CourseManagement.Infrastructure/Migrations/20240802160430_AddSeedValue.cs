using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4fa02999-630a-4cc7-8ee4-281dee335536"), null, "Teacher", null },
                    { new Guid("64347f96-85a1-4326-95f9-8291f9b64611"), null, "Admin", null },
                    { new Guid("c77748f7-eb8b-4e0d-aff9-0f1beced819e"), null, "Student", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("abc5d607-8ccc-46ef-b56c-c0e8fff6cc8e"), 0, "022b5807-2310-494f-93cc-f67ba6cad2c2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", false, "Admin", "", false, null, null, null, "AQAAAAIAAYagAAAAEPwkCLdOqMx3lLCQhaZ3z27YG8N4R2DRaXCEJB3yrnzOiLgWqCPWZoOweTbhrcJFCA==", null, false, null, false, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("64347f96-85a1-4326-95f9-8291f9b64611"), new Guid("abc5d607-8ccc-46ef-b56c-c0e8fff6cc8e") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4fa02999-630a-4cc7-8ee4-281dee335536"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c77748f7-eb8b-4e0d-aff9-0f1beced819e"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("64347f96-85a1-4326-95f9-8291f9b64611"), new Guid("abc5d607-8ccc-46ef-b56c-c0e8fff6cc8e") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("64347f96-85a1-4326-95f9-8291f9b64611"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("abc5d607-8ccc-46ef-b56c-c0e8fff6cc8e"));
        }
    }
}

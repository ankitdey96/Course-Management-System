using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4fa02999-630a-4cc7-8ee4-281dee335536"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c77748f7-eb8b-4e0d-aff9-0f1beced819e"));

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("cccedbab-fb08-44a4-b01c-dc2acb893b79"), null, "Teacher", null },
                    { new Guid("ecc7fb6d-145a-4d16-983c-052adab45764"), null, "Student", null }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("abc5d607-8ccc-46ef-b56c-c0e8fff6cc8e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "96e63ab1-2f1d-4948-a6f0-8afdbad2862d", "AQAAAAIAAYagAAAAEL2YXpprJKLmZxFygn9Y3kg1seuYQwdDjAAN3f2d/wwBKIAI9oB5dSDxLWsXn/2Kxw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cccedbab-fb08-44a4-b01c-dc2acb893b79"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ecc7fb6d-145a-4d16-983c-052adab45764"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4fa02999-630a-4cc7-8ee4-281dee335536"), null, "Teacher", null },
                    { new Guid("c77748f7-eb8b-4e0d-aff9-0f1beced819e"), null, "Student", null }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("abc5d607-8ccc-46ef-b56c-c0e8fff6cc8e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "022b5807-2310-494f-93cc-f67ba6cad2c2", "AQAAAAIAAYagAAAAEPwkCLdOqMx3lLCQhaZ3z27YG8N4R2DRaXCEJB3yrnzOiLgWqCPWZoOweTbhrcJFCA==" });
        }
    }
}

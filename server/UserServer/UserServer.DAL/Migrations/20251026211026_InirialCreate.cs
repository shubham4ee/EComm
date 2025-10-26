using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InirialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("1e4410a1-1a94-4463-93c0-dbbb4d92854b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("25369254-84af-44bc-a0e4-f98f17b7afa2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("a74a0a14-dce1-42b8-b691-3a06be826fb6"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("289037e1-4191-4670-9bef-24277044b72e"), new DateTime(2025, 10, 26, 21, 10, 23, 971, DateTimeKind.Utc).AddTicks(2250), "admin@gmail.com", new DateTime(2025, 10, 26, 21, 10, 23, 971, DateTimeKind.Utc).AddTicks(2252), "Admin123!", "admin", "admin" },
                    { new Guid("7efcf5f7-f80d-42b9-8263-d0bff6797f27"), new DateTime(2025, 10, 26, 21, 10, 23, 971, DateTimeKind.Utc).AddTicks(2284), "user1@gmail.com", new DateTime(2025, 10, 26, 21, 10, 23, 971, DateTimeKind.Utc).AddTicks(2284), "User1@123", "user", "user1" },
                    { new Guid("bba6b73b-fca6-46a2-bde4-ebbc807f32b1"), new DateTime(2025, 10, 26, 21, 10, 23, 971, DateTimeKind.Utc).AddTicks(2287), "user2@gmail.com", new DateTime(2025, 10, 26, 21, 10, 23, 971, DateTimeKind.Utc).AddTicks(2287), "User2@123", "user", "user2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("289037e1-4191-4670-9bef-24277044b72e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("7efcf5f7-f80d-42b9-8263-d0bff6797f27"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("bba6b73b-fca6-46a2-bde4-ebbc807f32b1"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("1e4410a1-1a94-4463-93c0-dbbb4d92854b"), new DateTime(2024, 11, 28, 11, 40, 55, 366, DateTimeKind.Utc).AddTicks(9118), "user1@gmail.com", new DateTime(2024, 11, 28, 11, 40, 55, 366, DateTimeKind.Utc).AddTicks(9118), "User1@123", "user", "user1" },
                    { new Guid("25369254-84af-44bc-a0e4-f98f17b7afa2"), new DateTime(2024, 11, 28, 11, 40, 55, 366, DateTimeKind.Utc).AddTicks(9121), "user2@gmail.com", new DateTime(2024, 11, 28, 11, 40, 55, 366, DateTimeKind.Utc).AddTicks(9121), "User2@123", "user", "user2" },
                    { new Guid("a74a0a14-dce1-42b8-b691-3a06be826fb6"), new DateTime(2024, 11, 28, 11, 40, 55, 366, DateTimeKind.Utc).AddTicks(9085), "admin@gmail.com", new DateTime(2024, 11, 28, 11, 40, 55, 366, DateTimeKind.Utc).AddTicks(9088), "Admin123!", "admin", "admin" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Adding_porject_status_field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("27e25b31-0e97-4bb1-a304-938d2bcde9f9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("40173de7-2ea3-4b16-b087-9c38f9b928d4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("80b67db8-2d60-4dc0-91d5-5122dcfc298c"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Projects",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Created");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("27e25b31-0e97-4bb1-a304-938d2bcde9f9"), new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4623), "user2@gmail.com", new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4623), "User2@123", "user", "user2" },
                    { new Guid("40173de7-2ea3-4b16-b087-9c38f9b928d4"), new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4620), "user1@gmail.com", new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4620), "User1@123", "user", "user1" },
                    { new Guid("80b67db8-2d60-4dc0-91d5-5122dcfc298c"), new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4563), "admin@gmail.com", new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4566), "Admin123!", "admin", "admin" }
                });
        }
    }
}

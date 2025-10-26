using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class update_CompositKey_for_UserProjects_mapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("62d0d516-417b-4f96-8215-b8905c69c6c7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("7933dccb-ead4-4b72-ac4b-e7b1db5b2135"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("bde310ec-6a3d-49d6-a46f-dc8c96e6883e"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserProjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserProjects");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "UserProjects");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("1767e4a3-e5f8-4630-94c9-76e60eddcfde"), new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(2984), "admin@gmail.com", new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(2987), "Admin123!", "admin", "admin" },
                    { new Guid("400e472a-7632-4fc6-8d54-d418d43e3ab9"), new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(3014), "user2@gmail.com", new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(3015), "User2@123", "user", "user2" },
                    { new Guid("e7849871-25e8-47cd-91d7-bb23d19e31a8"), new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(3011), "user1@gmail.com", new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(3011), "User1@123", "user", "user1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("1767e4a3-e5f8-4630-94c9-76e60eddcfde"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("400e472a-7632-4fc6-8d54-d418d43e3ab9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("e7849871-25e8-47cd-91d7-bb23d19e31a8"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProjects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserProjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "UserProjects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("62d0d516-417b-4f96-8215-b8905c69c6c7"), new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5482), "user2@gmail.com", new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5482), "User2@123", "user", "user2" },
                    { new Guid("7933dccb-ead4-4b72-ac4b-e7b1db5b2135"), new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5422), "admin@gmail.com", new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5424), "Admin123!", "admin", "admin" },
                    { new Guid("bde310ec-6a3d-49d6-a46f-dc8c96e6883e"), new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5478), "user1@gmail.com", new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5478), "User1@123", "user", "user1" }
                });
        }
    }
}

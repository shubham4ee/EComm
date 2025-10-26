using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Adding_ConversionStatus_In_CADFile_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("0a88b976-8aa2-43d7-8e92-1c12ffac9240"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("5e00e4c8-3d49-4966-8bff-00b4cf894ddb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("73fec2e9-5296-41db-94c1-a7c03cad6f4b"));

            migrationBuilder.AddColumn<string>(
                name: "ConversionStatus",
                table: "CADFiles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Not Initiated");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("54435c75-3a7b-4392-b0cb-4128a311283d"), new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2277), "user1@gmail.com", new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2277), "User1@123", "user", "user1" },
                    { new Guid("9a8e05d6-4d1c-4ca7-9518-a0ae0a3fb0f5"), new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2283), "user2@gmail.com", new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2283), "User2@123", "user", "user2" },
                    { new Guid("d2048e82-36ac-43f1-b4fa-d67e480239fd"), new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2241), "admin@gmail.com", new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2243), "Admin123!", "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("54435c75-3a7b-4392-b0cb-4128a311283d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("9a8e05d6-4d1c-4ca7-9518-a0ae0a3fb0f5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("d2048e82-36ac-43f1-b4fa-d67e480239fd"));

            migrationBuilder.DropColumn(
                name: "ConversionStatus",
                table: "CADFiles");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("0a88b976-8aa2-43d7-8e92-1c12ffac9240"), new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6112), "user1@gmail.com", new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6113), "User1@123", "user", "user1" },
                    { new Guid("5e00e4c8-3d49-4966-8bff-00b4cf894ddb"), new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6081), "admin@gmail.com", new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6083), "Admin123!", "admin", "admin" },
                    { new Guid("73fec2e9-5296-41db-94c1-a7c03cad6f4b"), new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6135), "user2@gmail.com", new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6135), "User2@123", "user", "user2" }
                });
        }
    }
}

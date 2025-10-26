using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CADFiles_Pojectid_and_FileName_Unique_constraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("a4b1fd9e-de0e-4e44-ae5d-bb15dd1257de"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("b6ec60f6-b63e-4db3-aec6-bdceb2fb66bc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("ff06d06b-06e2-44ab-a691-3a0c2a80cc37"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("27e25b31-0e97-4bb1-a304-938d2bcde9f9"), new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4623), "user2@gmail.com", new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4623), "User2@123", "user", "user2" },
                    { new Guid("40173de7-2ea3-4b16-b087-9c38f9b928d4"), new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4620), "user1@gmail.com", new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4620), "User1@123", "user", "user1" },
                    { new Guid("80b67db8-2d60-4dc0-91d5-5122dcfc298c"), new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4563), "admin@gmail.com", new DateTime(2024, 11, 28, 10, 37, 3, 139, DateTimeKind.Utc).AddTicks(4566), "Admin123!", "admin", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CADFiles_FileName_ProjectId",
                table: "CADFiles",
                columns: new[] { "FileName", "ProjectId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CADFiles_FileName_ProjectId",
                table: "CADFiles");

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("a4b1fd9e-de0e-4e44-ae5d-bb15dd1257de"), new DateTime(2024, 11, 28, 5, 50, 21, 951, DateTimeKind.Utc).AddTicks(1300), "user1@gmail.com", new DateTime(2024, 11, 28, 5, 50, 21, 951, DateTimeKind.Utc).AddTicks(1300), "User1@123", "user", "user1" },
                    { new Guid("b6ec60f6-b63e-4db3-aec6-bdceb2fb66bc"), new DateTime(2024, 11, 28, 5, 50, 21, 951, DateTimeKind.Utc).AddTicks(1303), "user2@gmail.com", new DateTime(2024, 11, 28, 5, 50, 21, 951, DateTimeKind.Utc).AddTicks(1303), "User2@123", "user", "user2" },
                    { new Guid("ff06d06b-06e2-44ab-a691-3a0c2a80cc37"), new DateTime(2024, 11, 28, 5, 50, 21, 951, DateTimeKind.Utc).AddTicks(1274), "admin@gmail.com", new DateTime(2024, 11, 28, 5, 50, 21, 951, DateTimeKind.Utc).AddTicks(1275), "Admin123!", "admin", "admin" }
                });
        }
    }
}

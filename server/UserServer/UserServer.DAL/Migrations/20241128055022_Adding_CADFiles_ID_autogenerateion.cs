using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Adding_CADFiles_ID_autogenerateion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("01255b70-dc54-47d0-8df9-23aba71b581f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("0c6d09ee-bcaf-43ff-9d1c-f43b389fc02b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("55650b8a-8dd6-4b86-9b5e-62af58f0b0ba"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "CADFiles",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "CADFiles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v4()");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("01255b70-dc54-47d0-8df9-23aba71b581f"), new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(908), "user1@gmail.com", new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(908), "User1@123", "user", "user1" },
                    { new Guid("0c6d09ee-bcaf-43ff-9d1c-f43b389fc02b"), new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(911), "user2@gmail.com", new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(911), "User2@123", "user", "user2" },
                    { new Guid("55650b8a-8dd6-4b86-9b5e-62af58f0b0ba"), new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(880), "admin@gmail.com", new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(882), "Admin123!", "admin", "admin" }
                });
        }
    }
}

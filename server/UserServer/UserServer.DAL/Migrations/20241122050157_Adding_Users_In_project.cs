using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Users_In_project : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatedBy",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects");

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

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    ProjectsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.ProjectsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("01255b70-dc54-47d0-8df9-23aba71b581f"), new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(908), "user1@gmail.com", new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(908), "User1@123", "user", "user1" },
                    { new Guid("0c6d09ee-bcaf-43ff-9d1c-f43b389fc02b"), new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(911), "user2@gmail.com", new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(911), "User2@123", "user", "user2" },
                    { new Guid("55650b8a-8dd6-4b86-9b5e-62af58f0b0ba"), new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(880), "admin@gmail.com", new DateTime(2024, 11, 22, 5, 1, 57, 203, DateTimeKind.Utc).AddTicks(882), "Admin123!", "admin", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_UsersId",
                table: "ProjectUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("1767e4a3-e5f8-4630-94c9-76e60eddcfde"), new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(2984), "admin@gmail.com", new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(2987), "Admin123!", "admin", "admin" },
                    { new Guid("400e472a-7632-4fc6-8d54-d418d43e3ab9"), new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(3014), "user2@gmail.com", new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(3015), "User2@123", "user", "user2" },
                    { new Guid("e7849871-25e8-47cd-91d7-bb23d19e31a8"), new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(3011), "user1@gmail.com", new DateTime(2024, 11, 21, 13, 48, 24, 330, DateTimeKind.Utc).AddTicks(3011), "User1@123", "user", "user1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatedBy",
                table: "Projects",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "User_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

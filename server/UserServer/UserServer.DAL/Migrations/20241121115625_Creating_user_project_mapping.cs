using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Creating_user_project_mapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("0131f5f4-bfe3-43b8-9141-fdfdffeeb6fb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("a3d8468d-6e5d-4156-ba66-b6662e8b8516"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("f01e1e81-2ec4-494c-baa8-9a5994fe86d1"));

            migrationBuilder.CreateTable(
                name: "UserProjectMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProjectMapping_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjectMapping_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("0a88b976-8aa2-43d7-8e92-1c12ffac9240"), new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6112), "user1@gmail.com", new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6113), "User1@123", "user", "user1" },
                    { new Guid("5e00e4c8-3d49-4966-8bff-00b4cf894ddb"), new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6081), "admin@gmail.com", new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6083), "Admin123!", "admin", "admin" },
                    { new Guid("73fec2e9-5296-41db-94c1-a7c03cad6f4b"), new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6135), "user2@gmail.com", new DateTime(2024, 11, 21, 11, 56, 25, 208, DateTimeKind.Utc).AddTicks(6135), "User2@123", "user", "user2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectMapping_ProjectId",
                table: "UserProjectMapping",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectMapping_UserId",
                table: "UserProjectMapping",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjectMapping");

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("0131f5f4-bfe3-43b8-9141-fdfdffeeb6fb"), new DateTime(2024, 11, 19, 13, 17, 48, 700, DateTimeKind.Utc).AddTicks(6495), "user1@gmail.com", new DateTime(2024, 11, 19, 13, 17, 48, 700, DateTimeKind.Utc).AddTicks(6495), "User1@123", "user", "user1" },
                    { new Guid("a3d8468d-6e5d-4156-ba66-b6662e8b8516"), new DateTime(2024, 11, 19, 13, 17, 48, 700, DateTimeKind.Utc).AddTicks(6467), "admin@gmail.com", new DateTime(2024, 11, 19, 13, 17, 48, 700, DateTimeKind.Utc).AddTicks(6468), "Admin123!", "admin", "admin" },
                    { new Guid("f01e1e81-2ec4-494c-baa8-9a5994fe86d1"), new DateTime(2024, 11, 19, 13, 17, 48, 700, DateTimeKind.Utc).AddTicks(6497), "user2@gmail.com", new DateTime(2024, 11, 19, 13, 17, 48, 700, DateTimeKind.Utc).AddTicks(6498), "User2@123", "user", "user2" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CompositKey_for_UserProjects_mapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjectMapping");

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

            migrationBuilder.CreateTable(
                name: "UserProjects",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjects", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_UserProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjects_Users_UserId",
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
                    { new Guid("62d0d516-417b-4f96-8215-b8905c69c6c7"), new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5482), "user2@gmail.com", new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5482), "User2@123", "user", "user2" },
                    { new Guid("7933dccb-ead4-4b72-ac4b-e7b1db5b2135"), new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5422), "admin@gmail.com", new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5424), "Admin123!", "admin", "admin" },
                    { new Guid("bde310ec-6a3d-49d6-a46f-dc8c96e6883e"), new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5478), "user1@gmail.com", new DateTime(2024, 11, 21, 13, 22, 37, 184, DateTimeKind.Utc).AddTicks(5478), "User1@123", "user", "user1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_ProjectId",
                table: "UserProjects",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjects");

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

            migrationBuilder.CreateTable(
                name: "UserProjectMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    { new Guid("54435c75-3a7b-4392-b0cb-4128a311283d"), new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2277), "user1@gmail.com", new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2277), "User1@123", "user", "user1" },
                    { new Guid("9a8e05d6-4d1c-4ca7-9518-a0ae0a3fb0f5"), new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2283), "user2@gmail.com", new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2283), "User2@123", "user", "user2" },
                    { new Guid("d2048e82-36ac-43f1-b4fa-d67e480239fd"), new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2241), "admin@gmail.com", new DateTime(2024, 11, 21, 12, 39, 12, 135, DateTimeKind.Utc).AddTicks(2243), "Admin123!", "admin", "admin" }
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
    }
}

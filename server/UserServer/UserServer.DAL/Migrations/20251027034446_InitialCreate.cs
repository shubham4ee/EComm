using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine("Starting migration in Up Method: InitialCreate");
            // ----------------------------------------------------------------------
            // STEP 1: SCHEMA CREATION (PURE SQL INJECTION - REQUIRED TO FIX ERROR)
            // This SQL block creates all tables, constraints, and indices BEFORE any data operations.
            // ----------------------------------------------------------------------

            migrationBuilder.Sql(@"
        -- 1. Create the required PostgreSQL extension for UUID generation
        CREATE EXTENSION IF NOT EXISTS ""uuid-ossp"";

        -- 2. CREATE ALL TABLES
        CREATE TABLE ""Users"" (
            ""User_id"" uuid NOT NULL DEFAULT (uuid_generate_v4()),
            ""Username"" character varying(100) NOT NULL,
            ""Email"" character varying(100) NOT NULL,
            ""Password"" character varying(150) NOT NULL,
            ""Role"" text NOT NULL,
            ""CreatedAt"" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
            ""ModifiedAt"" timestamp with time zone NOT NULL,
            CONSTRAINT ""PK_Users"" PRIMARY KEY (""User_id"")
        );

        CREATE TABLE ""Projects"" (
            ""Id"" uuid NOT NULL,
            ""ProjectName"" character varying(150) NOT NULL,
            ""Description"" text NOT NULL,
            ""CreatedBy"" uuid NOT NULL,
            ""CreatedAt"" timestamp with time zone NOT NULL,
            ""ModifiedAt"" timestamp with time zone NOT NULL,
            ""Status"" character varying(50) NOT NULL DEFAULT 'Created',
            CONSTRAINT ""PK_Projects"" PRIMARY KEY (""Id"")
        );

        CREATE TABLE ""CADFiles"" (
            ""Id"" uuid NOT NULL DEFAULT (uuid_generate_v4()),
            ""ProjectId"" uuid NOT NULL,
            ""FileName"" character varying(255) NOT NULL,
            ""FilePath"" character varying(255) NOT NULL,
            ""UploadedBy"" uuid NOT NULL,
            ""CreatedAt"" timestamp with time zone NOT NULL,
            ""ModifiedAt"" timestamp with time zone NOT NULL,
            ""ConversionStatus"" character varying(50) NOT NULL DEFAULT 'Not Initiated',
            CONSTRAINT ""PK_CADFiles"" PRIMARY KEY (""Id"")
        );
        
        CREATE TABLE ""Permissions"" (
            ""Id"" uuid NOT NULL,
            ""UserId"" uuid NOT NULL,
            ""ProjectId"" uuid NOT NULL,
            ""CanView"" boolean NOT NULL,
            ""CanEdit"" boolean NOT NULL,
            ""CanDelete"" boolean NOT NULL,
            ""CreatedAt"" timestamp with time zone NOT NULL,
            ""ModifiedAt"" timestamp with time zone NOT NULL,
            CONSTRAINT ""PK_Permissions"" PRIMARY KEY (""Id"")
        );
        
        CREATE TABLE ""ProjectUser"" (
            ""ProjectsId"" uuid NOT NULL,
            ""UsersId"" uuid NOT NULL,
            CONSTRAINT ""PK_ProjectUser"" PRIMARY KEY (""ProjectsId"", ""UsersId"")
        );
        
        CREATE TABLE ""Metadata"" (
            ""Id"" uuid NOT NULL,
            ""CADFileId"" uuid NOT NULL,
            ""Metadata"" jsonb NOT NULL,
            ""CreatedAt"" timestamp with time zone NOT NULL,
            ""ModifiedAt"" timestamp with time zone NOT NULL,
            CONSTRAINT ""PK_Metadata"" PRIMARY KEY (""Id"")
        );
        
        CREATE TABLE ""UserProjects"" (
            ""UserId"" uuid NOT NULL,
            ""ProjectId"" uuid NOT NULL,
            ""AssignedDate"" timestamp with time zone NOT NULL,
            CONSTRAINT ""PK_UserProjects"" PRIMARY KEY (""UserId"", ""ProjectId"")
        );


        -- 3. ADD CONSTRAINTS (Foreign Keys)
        ALTER TABLE ""Projects"" ADD CONSTRAINT ""FK_Projects_Users_CreatedBy"" FOREIGN KEY (""CreatedBy"") REFERENCES ""Users"" (""User_id"") ON DELETE CASCADE;
        
        ALTER TABLE ""CADFiles"" ADD CONSTRAINT ""FK_CADFiles_Projects_ProjectId"" FOREIGN KEY (""ProjectId"") REFERENCES ""Projects"" (""Id"") ON DELETE CASCADE;
        ALTER TABLE ""CADFiles"" ADD CONSTRAINT ""FK_CADFiles_Users_UploadedBy"" FOREIGN KEY (""UploadedBy"") REFERENCES ""Users"" (""User_id"") ON DELETE CASCADE;

        ALTER TABLE ""Permissions"" ADD CONSTRAINT ""FK_Permissions_Projects_ProjectId"" FOREIGN KEY (""ProjectId"") REFERENCES ""Projects"" (""Id"") ON DELETE CASCADE;
        ALTER TABLE ""Permissions"" ADD CONSTRAINT ""FK_Permissions_Users_UserId"" FOREIGN KEY (""UserId"") REFERENCES ""Users"" (""User_id"") ON DELETE CASCADE;

        ALTER TABLE ""ProjectUser"" ADD CONSTRAINT ""FK_ProjectUser_Projects_ProjectsId"" FOREIGN KEY (""ProjectsId"") REFERENCES ""Projects"" (""Id"") ON DELETE CASCADE;
        ALTER TABLE ""ProjectUser"" ADD CONSTRAINT ""FK_ProjectUser_Users_UsersId"" FOREIGN KEY (""UsersId"") REFERENCES ""Users"" (""User_id"") ON DELETE CASCADE;

        ALTER TABLE ""Metadata"" ADD CONSTRAINT ""FK_Metadata_CADFiles_CADFileId"" FOREIGN KEY (""CADFileId"") REFERENCES ""CADFiles"" (""Id"") ON DELETE CASCADE;
        
        ALTER TABLE ""UserProjects"" ADD CONSTRAINT ""FK_UserProjects_Projects_ProjectId"" FOREIGN KEY (""ProjectId"") REFERENCES ""Projects"" (""Id"") ON DELETE CASCADE;
        ALTER TABLE ""UserProjects"" ADD CONSTRAINT ""FK_UserProjects_Users_UserId"" FOREIGN KEY (""UserId"") REFERENCES ""Users"" (""User_id"") ON DELETE CASCADE;


        -- 4. CREATE INDICES
        CREATE UNIQUE INDEX ""IX_Users_Email"" ON ""Users"" (""Email"");
        CREATE UNIQUE INDEX ""IX_Users_Username"" ON ""Users"" (""Username"");
        CREATE INDEX ""IX_CADFiles_ProjectId"" ON ""CADFiles"" (""ProjectId"");
        CREATE INDEX ""IX_CADFiles_UploadedBy"" ON ""CADFiles"" (""UploadedBy"");
        CREATE UNIQUE INDEX ""IX_CADFiles_FileName_ProjectId"" ON ""CADFiles"" (""FileName"", ""ProjectId"");
        CREATE UNIQUE INDEX ""IX_Metadata_CADFileId"" ON ""Metadata"" (""CADFileId"");
        CREATE INDEX ""IX_Permissions_ProjectId"" ON ""Permissions"" (""ProjectId"");
        CREATE INDEX ""IX_Permissions_UserId"" ON ""Permissions"" (""UserId"");
        CREATE INDEX ""IX_ProjectUser_UsersId"" ON ""ProjectUser"" (""UsersId"");
        CREATE INDEX ""IX_UserProjects_ProjectId"" ON ""UserProjects"" (""ProjectId"");
    ");


            // ----------------------------------------------------------------------
            // STEP 2: SEED DATA INSERTION (This uses the current C# InsertData format and GUIDs)
            // The previous DeleteData calls have been removed as the database is now new and empty.
            // ----------------------------------------------------------------------
            Console.WriteLine("Running Seed Data in Up Method: InitialCreate");
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username" },
                values: new object[,]
                {
            // Note: The DateTime ticks are adjusted to match the C# format.
            { new Guid("8584729c-aa05-420f-8714-279cd809269d"), new DateTime(2025, 10, 27, 3, 44, 44, 904, DateTimeKind.Utc).AddTicks(5381), "user1@gmail.com", new DateTime(2025, 10, 27, 3, 44, 44, 904, DateTimeKind.Utc).AddTicks(5381), "User1@123", "user", "user1" },
            { new Guid("c33ecff4-12ec-403d-b4c0-01ecf4627a60"), new DateTime(2025, 10, 27, 3, 44, 44, 904, DateTimeKind.Utc).AddTicks(5384), "user2@gmail.com", new DateTime(2025, 10, 27, 3, 44, 44, 904, DateTimeKind.Utc).AddTicks(5384), "User2@123", "user", "user2" },
            { new Guid("ed2a321d-cc44-4d65-854f-57c02cb59405"), new DateTime(2025, 10, 27, 3, 44, 44, 904, DateTimeKind.Utc).AddTicks(5346), "admin@gmail.com", new DateTime(2025, 10, 27, 3, 44, 44, 904, DateTimeKind.Utc).AddTicks(5347), "Admin123!", "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine("Down Method: InitialCreate");
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("8584729c-aa05-420f-8714-279cd809269d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("c33ecff4-12ec-403d-b4c0-01ecf4627a60"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: new Guid("ed2a321d-cc44-4d65-854f-57c02cb59405"));

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
    }
}

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251027034446_InitialCreate') THEN
    DELETE FROM "Users"
    WHERE "User_id" = '289037e1-4191-4670-9bef-24277044b72e';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251027034446_InitialCreate') THEN
    DELETE FROM "Users"
    WHERE "User_id" = '7efcf5f7-f80d-42b9-8263-d0bff6797f27';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251027034446_InitialCreate') THEN
    DELETE FROM "Users"
    WHERE "User_id" = 'bba6b73b-fca6-46a2-bde4-ebbc807f32b1';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251027034446_InitialCreate') THEN
    INSERT INTO "Users" ("User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username")
    VALUES ('8584729c-aa05-420f-8714-279cd809269d', TIMESTAMPTZ '2025-10-27T03:44:44.904538Z', 'user1@gmail.com', TIMESTAMPTZ '2025-10-27T03:44:44.904538Z', 'User1@123', 'user', 'user1');
    INSERT INTO "Users" ("User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username")
    VALUES ('c33ecff4-12ec-403d-b4c0-01ecf4627a60', TIMESTAMPTZ '2025-10-27T03:44:44.904538Z', 'user2@gmail.com', TIMESTAMPTZ '2025-10-27T03:44:44.904538Z', 'User2@123', 'user', 'user2');
    INSERT INTO "Users" ("User_id", "CreatedAt", "Email", "ModifiedAt", "Password", "Role", "Username")
    VALUES ('ed2a321d-cc44-4d65-854f-57c02cb59405', TIMESTAMPTZ '2025-10-27T03:44:44.904534Z', 'admin@gmail.com', TIMESTAMPTZ '2025-10-27T03:44:44.904534Z', 'Admin123!', 'admin', 'admin');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251027034446_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251027034446_InitialCreate', '8.0.10');
    END IF;
END $EF$;
COMMIT;


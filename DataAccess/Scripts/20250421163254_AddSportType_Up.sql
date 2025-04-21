START TRANSACTION;
ALTER TABLE "Games" ADD "Sport" integer NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250421163254_AddSportType', '9.0.4');

COMMIT;


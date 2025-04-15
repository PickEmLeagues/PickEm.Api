START TRANSACTION;
ALTER TABLE "Games" ADD "Season" text NOT NULL DEFAULT '';

ALTER TABLE "Games" ADD "Week" integer NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250415065633_AddSeason', '9.0.4');

COMMIT;


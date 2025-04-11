START TRANSACTION;
DROP TABLE "LeagueUser";

DROP TABLE "Picks";

DROP TABLE "Schedules";

DROP TABLE "Games";

DROP TABLE "Leagues";

DROP TABLE "Users";

DELETE FROM "__EFMigrationsHistory"
WHERE "MigrationId" = '20250411204428_InitialMigration';

COMMIT;


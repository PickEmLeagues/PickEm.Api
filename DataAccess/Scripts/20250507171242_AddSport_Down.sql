START TRANSACTION;
ALTER TABLE "Games" DROP CONSTRAINT "FK_Games_Sports_SportId";

ALTER TABLE "Leagues" DROP CONSTRAINT "FK_Leagues_Sports_SportId";

ALTER TABLE "Picks" DROP CONSTRAINT "FK_Picks_Schedules_GameId";

DROP TABLE "Sports";

DROP INDEX "IX_Picks_GameId";

DROP INDEX "IX_Leagues_SportId";

DROP INDEX "IX_Games_SportId";

ALTER TABLE "Picks" DROP COLUMN "GameId";

ALTER TABLE "Leagues" DROP COLUMN "SportId";

ALTER TABLE "Games" DROP COLUMN "SportId";

ALTER TABLE "Leagues" ADD "Sport" integer NOT NULL DEFAULT 0;

ALTER TABLE "Games" ADD "Season" text NOT NULL DEFAULT '';

ALTER TABLE "Games" ADD "Sport" integer NOT NULL DEFAULT 0;

CREATE INDEX "IX_Picks_GameLeagueId" ON "Picks" ("GameLeagueId");

ALTER TABLE "Picks" ADD CONSTRAINT "FK_Picks_Schedules_GameLeagueId" FOREIGN KEY ("GameLeagueId") REFERENCES "Schedules" ("Id") ON DELETE CASCADE;

DELETE FROM "__EFMigrationsHistory"
WHERE "MigrationId" = '20250507171242_AddSport';

COMMIT;


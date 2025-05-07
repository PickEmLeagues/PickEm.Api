using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PickEm.Api.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picks_Schedules_GameLeagueId",
                table: "Picks");

            migrationBuilder.DropIndex(
                name: "IX_Picks_GameLeagueId",
                table: "Picks");

            migrationBuilder.DropColumn(
                name: "Sport",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Sport",
                table: "Games");

            migrationBuilder.AddColumn<long>(
                name: "GameId",
                table: "Picks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SportId",
                table: "Leagues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SportId",
                table: "Games",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Season = table.Column<string>(type: "text", nullable: false),
                    Week = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picks_GameId",
                table: "Picks",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_SportId",
                table: "Games",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Sports_SportId",
                table: "Games",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picks_Schedules_GameId",
                table: "Picks",
                column: "GameId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Sports_SportId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Sports_SportId",
                table: "Leagues");

            migrationBuilder.DropForeignKey(
                name: "FK_Picks_Schedules_GameId",
                table: "Picks");

            migrationBuilder.DropTable(
                name: "Sports");

            migrationBuilder.DropIndex(
                name: "IX_Picks_GameId",
                table: "Picks");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Games_SportId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Picks");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "Sport",
                table: "Leagues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Sport",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Picks_GameLeagueId",
                table: "Picks",
                column: "GameLeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picks_Schedules_GameLeagueId",
                table: "Picks",
                column: "GameLeagueId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

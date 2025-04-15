using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PickEm.Api.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSeason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Season",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "Games");
        }
    }
}

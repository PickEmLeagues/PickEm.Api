using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PickEm.Api.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSportType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sport",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sport",
                table: "Games");
        }
    }
}

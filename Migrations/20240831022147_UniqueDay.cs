using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrasGames.Migrations
{
    /// <inheritdoc />
    public partial class UniqueDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Agenda_Day",
                table: "Agenda",
                column: "Day",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agenda_Day",
                table: "Agenda");
        }
    }
}

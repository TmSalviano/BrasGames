using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrasGames.Migrations
{
    /// <inheritdoc />
    public partial class PrimaryKeyNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Agenda",
                newName: "Day");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Agenda",
                newName: "Id");
        }
    }
}

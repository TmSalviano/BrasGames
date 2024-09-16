using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrasGames.Migrations.ServiceDb
{
    /// <inheritdoc />
    public partial class UpdateChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consoles_Orders_OrderId",
                table: "Consoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Controllers_Orders_OrderId",
                table: "Controllers");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Orders_OrderId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Games_OrderId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Controllers_OrderId",
                table: "Controllers");

            migrationBuilder.DropIndex(
                name: "IX_Consoles_OrderId",
                table: "Consoles");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Controllers");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Consoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Games",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Controllers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Consoles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    GovernmentName = table.Column<string>(type: "TEXT", nullable: false),
                    orderState = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_OrderId",
                table: "Games",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_OrderId",
                table: "Controllers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Consoles_OrderId",
                table: "Consoles",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consoles_Orders_OrderId",
                table: "Consoles",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Controllers_Orders_OrderId",
                table: "Controllers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Orders_OrderId",
                table: "Games",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}

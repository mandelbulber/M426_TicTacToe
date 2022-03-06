using Microsoft.EntityFrameworkCore.Migrations;

namespace M426_TicTacToe.Data.Migrations
{
    public partial class added_history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Player2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Game = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Histories");
        }
    }
}

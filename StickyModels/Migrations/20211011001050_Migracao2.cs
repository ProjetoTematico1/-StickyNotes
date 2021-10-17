using Microsoft.EntityFrameworkCore.Migrations;

namespace StickyModels.Migrations
{
    public partial class Migracao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FK_Colorcod_color",
                table: "Card",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    cod_color = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    hex_text = table.Column<string>(type: "TEXT", nullable: true),
                    font_color = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.cod_color);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_FK_Colorcod_color",
                table: "Card",
                column: "FK_Colorcod_color");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Color_FK_Colorcod_color",
                table: "Card",
                column: "FK_Colorcod_color",
                principalTable: "Color",
                principalColumn: "cod_color",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Color_FK_Colorcod_color",
                table: "Card");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropIndex(
                name: "IX_Card_FK_Colorcod_color",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "FK_Colorcod_color",
                table: "Card");
        }
    }
}

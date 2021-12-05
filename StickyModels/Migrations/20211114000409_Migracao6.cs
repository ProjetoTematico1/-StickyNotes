using Microsoft.EntityFrameworkCore.Migrations;

namespace StickyModels.Migrations
{
    public partial class Migracao6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceColumnModelcod_column",
                table: "Card",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlaceColumn",
                columns: table => new
                {
                    cod_column = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cod_place = table.Column<int>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: true),
                    order = table.Column<int>(type: "INTEGER", nullable: false),
                    FK_Placecod_column = table.Column<int>(type: "INTEGER", nullable: true),
                    PlaceModelcod_place = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceColumn", x => x.cod_column);
                    table.ForeignKey(
                        name: "FK_PlaceColumn_Place_PlaceModelcod_place",
                        column: x => x.PlaceModelcod_place,
                        principalTable: "Place",
                        principalColumn: "cod_place",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaceColumn_PlaceColumn_FK_Placecod_column",
                        column: x => x.FK_Placecod_column,
                        principalTable: "PlaceColumn",
                        principalColumn: "cod_column",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_PlaceColumnModelcod_column",
                table: "Card",
                column: "PlaceColumnModelcod_column");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceColumn_FK_Placecod_column",
                table: "PlaceColumn",
                column: "FK_Placecod_column");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceColumn_PlaceModelcod_place",
                table: "PlaceColumn",
                column: "PlaceModelcod_place");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_PlaceColumn_PlaceColumnModelcod_column",
                table: "Card",
                column: "PlaceColumnModelcod_column",
                principalTable: "PlaceColumn",
                principalColumn: "cod_column",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_PlaceColumn_PlaceColumnModelcod_column",
                table: "Card");

            migrationBuilder.DropTable(
                name: "PlaceColumn");

            migrationBuilder.DropIndex(
                name: "IX_Card_PlaceColumnModelcod_column",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "PlaceColumnModelcod_column",
                table: "Card");
        }
    }
}

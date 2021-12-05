using Microsoft.EntityFrameworkCore.Migrations;

namespace StickyModels.Migrations
{
    public partial class Migracao7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Place_cod_place",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_PlaceColumn_PlaceColumnModelcod_column",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceColumn_Place_PlaceModelcod_place",
                table: "PlaceColumn");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceColumn_PlaceColumn_FK_Placecod_column",
                table: "PlaceColumn");

            migrationBuilder.DropIndex(
                name: "IX_PlaceColumn_FK_Placecod_column",
                table: "PlaceColumn");

            migrationBuilder.DropIndex(
                name: "IX_PlaceColumn_PlaceModelcod_place",
                table: "PlaceColumn");

            migrationBuilder.DropIndex(
                name: "IX_Card_PlaceColumnModelcod_column",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "FK_Placecod_column",
                table: "PlaceColumn");

            migrationBuilder.DropColumn(
                name: "PlaceModelcod_place",
                table: "PlaceColumn");

            migrationBuilder.DropColumn(
                name: "PlaceColumnModelcod_column",
                table: "Card");

            migrationBuilder.RenameColumn(
                name: "cod_place",
                table: "Card",
                newName: "cod_column");

            migrationBuilder.RenameIndex(
                name: "IX_Card_cod_place",
                table: "Card",
                newName: "IX_Card_cod_column");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceColumn_cod_place",
                table: "PlaceColumn",
                column: "cod_place");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_PlaceColumn_cod_column",
                table: "Card",
                column: "cod_column",
                principalTable: "PlaceColumn",
                principalColumn: "cod_column",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceColumn_Place_cod_place",
                table: "PlaceColumn",
                column: "cod_place",
                principalTable: "Place",
                principalColumn: "cod_place",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_PlaceColumn_cod_column",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceColumn_Place_cod_place",
                table: "PlaceColumn");

            migrationBuilder.DropIndex(
                name: "IX_PlaceColumn_cod_place",
                table: "PlaceColumn");

            migrationBuilder.RenameColumn(
                name: "cod_column",
                table: "Card",
                newName: "cod_place");

            migrationBuilder.RenameIndex(
                name: "IX_Card_cod_column",
                table: "Card",
                newName: "IX_Card_cod_place");

            migrationBuilder.AddColumn<int>(
                name: "FK_Placecod_column",
                table: "PlaceColumn",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaceModelcod_place",
                table: "PlaceColumn",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaceColumnModelcod_column",
                table: "Card",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaceColumn_FK_Placecod_column",
                table: "PlaceColumn",
                column: "FK_Placecod_column");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceColumn_PlaceModelcod_place",
                table: "PlaceColumn",
                column: "PlaceModelcod_place");

            migrationBuilder.CreateIndex(
                name: "IX_Card_PlaceColumnModelcod_column",
                table: "Card",
                column: "PlaceColumnModelcod_column");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Place_cod_place",
                table: "Card",
                column: "cod_place",
                principalTable: "Place",
                principalColumn: "cod_place",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_PlaceColumn_PlaceColumnModelcod_column",
                table: "Card",
                column: "PlaceColumnModelcod_column",
                principalTable: "PlaceColumn",
                principalColumn: "cod_column",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceColumn_Place_PlaceModelcod_place",
                table: "PlaceColumn",
                column: "PlaceModelcod_place",
                principalTable: "Place",
                principalColumn: "cod_place",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceColumn_PlaceColumn_FK_Placecod_column",
                table: "PlaceColumn",
                column: "FK_Placecod_column",
                principalTable: "PlaceColumn",
                principalColumn: "cod_column",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

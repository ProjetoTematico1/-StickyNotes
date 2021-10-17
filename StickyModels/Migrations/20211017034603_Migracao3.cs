using Microsoft.EntityFrameworkCore.Migrations;

namespace StickyModels.Migrations
{
    public partial class Migracao3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Color_FK_Colorcod_color",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Place_FK_Placecod_place",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Tag_FK_Tagcod_tag",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Card_FK_Cardcod_card",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_FK_Cardcod_card",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Card_FK_Colorcod_color",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_FK_Placecod_place",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_FK_Tagcod_tag",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "FK_Cardcod_card",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "FK_Colorcod_color",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "FK_Placecod_place",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "FK_Tagcod_tag",
                table: "Card");

            migrationBuilder.AddColumn<bool>(
                name: "open",
                table: "Card",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Image_cod_card",
                table: "Image",
                column: "cod_card");

            migrationBuilder.CreateIndex(
                name: "IX_Card_cod_color",
                table: "Card",
                column: "cod_color");

            migrationBuilder.CreateIndex(
                name: "IX_Card_cod_place",
                table: "Card",
                column: "cod_place");

            migrationBuilder.CreateIndex(
                name: "IX_Card_cod_tag",
                table: "Card",
                column: "cod_tag");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Color_cod_color",
                table: "Card",
                column: "cod_color",
                principalTable: "Color",
                principalColumn: "cod_color",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Place_cod_place",
                table: "Card",
                column: "cod_place",
                principalTable: "Place",
                principalColumn: "cod_place",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Tag_cod_tag",
                table: "Card",
                column: "cod_tag",
                principalTable: "Tag",
                principalColumn: "cod_tag",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Card_cod_card",
                table: "Image",
                column: "cod_card",
                principalTable: "Card",
                principalColumn: "cod_card",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Color_cod_color",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Place_cod_place",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Tag_cod_tag",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Card_cod_card",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_cod_card",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Card_cod_color",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_cod_place",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_cod_tag",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "open",
                table: "Card");

            migrationBuilder.AddColumn<int>(
                name: "FK_Cardcod_card",
                table: "Image",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FK_Colorcod_color",
                table: "Card",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FK_Placecod_place",
                table: "Card",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FK_Tagcod_tag",
                table: "Card",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_FK_Cardcod_card",
                table: "Image",
                column: "FK_Cardcod_card");

            migrationBuilder.CreateIndex(
                name: "IX_Card_FK_Colorcod_color",
                table: "Card",
                column: "FK_Colorcod_color");

            migrationBuilder.CreateIndex(
                name: "IX_Card_FK_Placecod_place",
                table: "Card",
                column: "FK_Placecod_place");

            migrationBuilder.CreateIndex(
                name: "IX_Card_FK_Tagcod_tag",
                table: "Card",
                column: "FK_Tagcod_tag");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Color_FK_Colorcod_color",
                table: "Card",
                column: "FK_Colorcod_color",
                principalTable: "Color",
                principalColumn: "cod_color",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Place_FK_Placecod_place",
                table: "Card",
                column: "FK_Placecod_place",
                principalTable: "Place",
                principalColumn: "cod_place",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Tag_FK_Tagcod_tag",
                table: "Card",
                column: "FK_Tagcod_tag",
                principalTable: "Tag",
                principalColumn: "cod_tag",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Card_FK_Cardcod_card",
                table: "Image",
                column: "FK_Cardcod_card",
                principalTable: "Card",
                principalColumn: "cod_card",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

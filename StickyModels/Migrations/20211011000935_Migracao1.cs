using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StickyModels.Migrations
{
    public partial class Migracao1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    cod_place = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.cod_place);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    cod_tag = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    text = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.cod_tag);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    cod_card = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cod_tag = table.Column<int>(type: "INTEGER", nullable: true),
                    cod_color = table.Column<int>(type: "INTEGER", nullable: true),
                    cod_place = table.Column<int>(type: "INTEGER", nullable: true),
                    text = table.Column<string>(type: "TEXT", nullable: true),
                    dt_reminder = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FK_Tagcod_tag = table.Column<int>(type: "INTEGER", nullable: true),
                    FK_Placecod_place = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.cod_card);
                    table.ForeignKey(
                        name: "FK_Card_Place_FK_Placecod_place",
                        column: x => x.FK_Placecod_place,
                        principalTable: "Place",
                        principalColumn: "cod_place",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Card_Tag_FK_Tagcod_tag",
                        column: x => x.FK_Tagcod_tag,
                        principalTable: "Tag",
                        principalColumn: "cod_tag",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    cod_image = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cod_card = table.Column<int>(type: "INTEGER", nullable: false),
                    path = table.Column<string>(type: "TEXT", nullable: true),
                    FK_Cardcod_card = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.cod_image);
                    table.ForeignKey(
                        name: "FK_Image_Card_FK_Cardcod_card",
                        column: x => x.FK_Cardcod_card,
                        principalTable: "Card",
                        principalColumn: "cod_card",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_FK_Placecod_place",
                table: "Card",
                column: "FK_Placecod_place");

            migrationBuilder.CreateIndex(
                name: "IX_Card_FK_Tagcod_tag",
                table: "Card",
                column: "FK_Tagcod_tag");

            migrationBuilder.CreateIndex(
                name: "IX_Image_FK_Cardcod_card",
                table: "Image",
                column: "FK_Cardcod_card");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}

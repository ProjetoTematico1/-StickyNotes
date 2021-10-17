using Microsoft.EntityFrameworkCore.Migrations;

namespace StickyModels.Migrations
{
    public partial class Migracao5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "stroke_color",
                table: "Color",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stroke_color",
                table: "Color");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace StickyModels.Migrations
{
    public partial class Migracao4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "place_position_x",
                table: "Place",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "place_position_y",
                table: "Place",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "card_position_x",
                table: "Card",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "card_position_y",
                table: "Card",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "reminder_confirm",
                table: "Card",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "place_position_x",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "place_position_y",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "card_position_x",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "card_position_y",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "reminder_confirm",
                table: "Card");
        }
    }
}

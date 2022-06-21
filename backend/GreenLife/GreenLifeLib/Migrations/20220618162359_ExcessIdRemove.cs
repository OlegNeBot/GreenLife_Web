using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenLifeLib.Migrations
{
    public partial class ExcessIdRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HabitPhraseId",
                table: "habit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HabitPhraseId",
                table: "habit",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

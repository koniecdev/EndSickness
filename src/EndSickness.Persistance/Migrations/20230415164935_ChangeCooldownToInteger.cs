using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndSickness.Persistance.Migrations
{
    public partial class ChangeCooldownToInteger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cooldown",
                table: "Medicines");

            migrationBuilder.AddColumn<int>(
                name: "HourlyCooldown",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyCooldown",
                table: "Medicines");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Cooldown",
                table: "Medicines",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}

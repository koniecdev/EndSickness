using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndSickness.Persistance.Migrations
{
    public partial class SeedDatabaseAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxTreatmentTime",
                table: "Medicines");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MaxDaysOfTreatment",
                table: "Medicines",
                type: "time",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "StatusId", "UserId", "Username" },
                values: new object[] { 1, "defaultAdmin@koniec.dev", 1, "00000000-0000-0000-0000-000000000000", "DefaultAdmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "MaxDaysOfTreatment",
                table: "Medicines");

            migrationBuilder.AddColumn<int>(
                name: "MaxTreatmentTime",
                table: "Medicines",
                type: "int",
                nullable: true);
        }
    }
}

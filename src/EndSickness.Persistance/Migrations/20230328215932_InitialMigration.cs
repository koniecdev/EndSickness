using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndSickness.Persistance.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicineLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineLists_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    Cooldown = table.Column<TimeSpan>(type: "time", nullable: false),
                    MaxDailyAmount = table.Column<int>(type: "int", nullable: true),
                    MaxTreatmentTime = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastlyTaken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicineListId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineLogs_MedicineLists_MedicineListId",
                        column: x => x.MedicineListId,
                        principalTable: "MedicineLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MedicineLogs_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MedicineMedicineList",
                columns: table => new
                {
                    MedicineListsId = table.Column<int>(type: "int", nullable: false),
                    MedicinesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineMedicineList", x => new { x.MedicineListsId, x.MedicinesId });
                    table.ForeignKey(
                        name: "FK_MedicineMedicineList_MedicineLists_MedicineListsId",
                        column: x => x.MedicineListsId,
                        principalTable: "MedicineLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MedicineMedicineList_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineLists_AppUserId",
                table: "MedicineLists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineLogs_MedicineId",
                table: "MedicineLogs",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineLogs_MedicineListId",
                table: "MedicineLogs",
                column: "MedicineListId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineMedicineList_MedicinesId",
                table: "MedicineMedicineList",
                column: "MedicinesId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_AppUserId",
                table: "Medicines",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineLogs");

            migrationBuilder.DropTable(
                name: "MedicineMedicineList");

            migrationBuilder.DropTable(
                name: "MedicineLists");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}

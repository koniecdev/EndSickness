using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndSickness.Persistance.Migrations
{
    public partial class RemoveMedicineListFromApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineLogs_MedicineLists_MedicineListId",
                table: "MedicineLogs");

            migrationBuilder.DropTable(
                name: "MedicineMedicineList");

            migrationBuilder.DropTable(
                name: "MedicineLists");

            migrationBuilder.RenameColumn(
                name: "MedicineListId",
                table: "MedicineLogs",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineLogs_MedicineListId",
                table: "MedicineLogs",
                newName: "IX_MedicineLogs_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineLogs_AppUsers_AppUserId",
                table: "MedicineLogs",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineLogs_AppUsers_AppUserId",
                table: "MedicineLogs");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "MedicineLogs",
                newName: "MedicineListId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineLogs_AppUserId",
                table: "MedicineLogs",
                newName: "IX_MedicineLogs_MedicineListId");

            migrationBuilder.CreateTable(
                name: "MedicineLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineMedicineList_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineLists_AppUserId",
                table: "MedicineLists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineMedicineList_MedicinesId",
                table: "MedicineMedicineList",
                column: "MedicinesId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineLogs_MedicineLists_MedicineListId",
                table: "MedicineLogs",
                column: "MedicineListId",
                principalTable: "MedicineLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

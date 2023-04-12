//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace EndSickness.Persistance.Migrations
//{
//    public partial class RefactorUserId : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_MedicineLogs_AppUsers_AppUserId",
//                table: "MedicineLogs");

//            migrationBuilder.DropForeignKey(
//                name: "FK_Medicines_AppUsers_AppUserId",
//                table: "Medicines");

//            migrationBuilder.DropTable(
//                name: "AppUsers");

//            migrationBuilder.DropIndex(
//                name: "IX_Medicines_AppUserId",
//                table: "Medicines");

//            migrationBuilder.DropIndex(
//                name: "IX_MedicineLogs_AppUserId",
//                table: "MedicineLogs");

//            migrationBuilder.DropColumn(
//                name: "AppUserId",
//                table: "Medicines");

//            migrationBuilder.DropColumn(
//                name: "AppUserId",
//                table: "MedicineLogs");

//            migrationBuilder.AlterColumn<int>(
//                name: "MaxDaysOfTreatment",
//                table: "Medicines",
//                type: "int",
//                nullable: true,
//                oldClrType: typeof(TimeSpan),
//                oldType: "time",
//                oldNullable: true);

//            migrationBuilder.AddColumn<string>(
//                name: "OwnerId",
//                table: "Medicines",
//                type: "nvarchar(max)",
//                nullable: false,
//                defaultValue: "");

//            migrationBuilder.AddColumn<string>(
//                name: "OwnerId",
//                table: "MedicineLogs",
//                type: "nvarchar(max)",
//                nullable: false,
//                defaultValue: "");
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropColumn(
//                name: "OwnerId",
//                table: "Medicines");

//            migrationBuilder.DropColumn(
//                name: "OwnerId",
//                table: "MedicineLogs");

//            migrationBuilder.AlterColumn<TimeSpan>(
//                name: "MaxDaysOfTreatment",
//                table: "Medicines",
//                type: "time",
//                nullable: true,
//                oldClrType: typeof(int),
//                oldType: "int",
//                oldNullable: true);

//            migrationBuilder.AddColumn<int>(
//                name: "AppUserId",
//                table: "Medicines",
//                type: "int",
//                nullable: false,
//                defaultValue: 0);

//            migrationBuilder.AddColumn<int>(
//                name: "AppUserId",
//                table: "MedicineLogs",
//                type: "int",
//                nullable: false,
//                defaultValue: 0);

//            migrationBuilder.CreateTable(
//                name: "AppUsers",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
//                    StatusId = table.Column<int>(type: "int", nullable: false),
//                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
//                    Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_AppUsers", x => x.Id);
//                });

//            migrationBuilder.InsertData(
//                table: "AppUsers",
//                columns: new[] { "Id", "Email", "StatusId", "UserId", "Username" },
//                values: new object[] { 1, "defaultAdmin@koniec.dev", 1, "00000000-0000-0000-0000-000000000000", "DefaultAdmin" });

//            migrationBuilder.CreateIndex(
//                name: "IX_Medicines_AppUserId",
//                table: "Medicines",
//                column: "AppUserId");

//            migrationBuilder.CreateIndex(
//                name: "IX_MedicineLogs_AppUserId",
//                table: "MedicineLogs",
//                column: "AppUserId");

//            migrationBuilder.AddForeignKey(
//                name: "FK_MedicineLogs_AppUsers_AppUserId",
//                table: "MedicineLogs",
//                column: "AppUserId",
//                principalTable: "AppUsers",
//                principalColumn: "Id",
//                onDelete: ReferentialAction.Cascade);

//            migrationBuilder.AddForeignKey(
//                name: "FK_Medicines_AppUsers_AppUserId",
//                table: "Medicines",
//                column: "AppUserId",
//                principalTable: "AppUsers",
//                principalColumn: "Id",
//                onDelete: ReferentialAction.Cascade);
//        }
//    }
//}

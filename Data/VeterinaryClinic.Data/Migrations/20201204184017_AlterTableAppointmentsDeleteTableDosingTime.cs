using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryClinic.Data.Migrations
{
    public partial class AlterTableAppointmentsDeleteTableDosingTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DosingTimes");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartTime",
                table: "Appointments",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualStartTime",
                table: "Appointments");

            migrationBuilder.CreateTable(
                name: "DosingTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    MedicationId = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DosingTimes_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DosingTimes_IsDeleted",
                table: "DosingTimes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DosingTimes_MedicationId",
                table: "DosingTimes",
                column: "MedicationId");
        }
    }
}

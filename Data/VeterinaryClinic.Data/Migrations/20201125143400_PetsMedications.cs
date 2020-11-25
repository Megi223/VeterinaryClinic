using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryClinic.Data.Migrations
{
    public partial class PetsMedications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Diagnoses_DiagnoseId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_DiagnoseId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "DiagnoseId",
                table: "Medications");

            migrationBuilder.CreateTable(
                name: "PetsMedications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetsMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetsMedications_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PetsMedications_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetsMedications_IsDeleted",
                table: "PetsMedications",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PetsMedications_MedicationId",
                table: "PetsMedications",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PetsMedications_PetId",
                table: "PetsMedications",
                column: "PetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetsMedications");

            migrationBuilder.AddColumn<int>(
                name: "DiagnoseId",
                table: "Medications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DiagnoseId",
                table: "Medications",
                column: "DiagnoseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Diagnoses_DiagnoseId",
                table: "Medications",
                column: "DiagnoseId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

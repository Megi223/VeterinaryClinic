using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace VeterinaryClinic.Data.Migrations
{
    public partial class PetTableChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "PassportId",
                table: "Pets",
                newName: "IdentificationNumber");

            migrationBuilder.AlterColumn<int>(
                name: "DiagnoseId",
                table: "Pets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Pets",
                type: "DateTime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdentificationNumber",
                table: "Pets",
                newName: "PassportId");

            migrationBuilder.AlterColumn<int>(
                name: "DiagnoseId",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Pets",
                type: "DateTime",
                nullable: false,
                defaultValue: DateTime.MinValue,
                oldClrType: typeof(DateTime),
                oldType: "DateTime",
                oldNullable: true);
        }
    }
}

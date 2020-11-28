namespace VeterinaryClinic.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AlterVetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Vets",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: string.Empty);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Vets");
        }
    }
}

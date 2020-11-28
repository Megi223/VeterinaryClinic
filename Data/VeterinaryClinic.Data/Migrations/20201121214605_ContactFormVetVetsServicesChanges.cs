namespace VeterinaryClinic.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ContactFormVetVetsServicesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VetsServices_Owners_OwnerId",
                table: "VetsServices");

            migrationBuilder.DropForeignKey(
                name: "FK_VetsServices_Services_ServiceId",
                table: "VetsServices");

            migrationBuilder.DropIndex(
                name: "IX_VetsServices_OwnerId",
                table: "VetsServices");

            migrationBuilder.DropIndex(
                name: "IX_VetsServices_ServiceId",
                table: "VetsServices");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "VetsServices");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Vets");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "ContactForms");

            migrationBuilder.RenameColumn(
                name: "Answered",
                table: "ContactForms",
                newName: "Accepted");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceId",
                table: "VetsServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId1",
                table: "VetsServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PetId",
                table: "ContactForms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "VetId",
                table: "ContactForms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.CreateIndex(
                name: "IX_VetsServices_ServiceId1",
                table: "VetsServices",
                column: "ServiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_ContactForms_PetId",
                table: "ContactForms",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactForms_VetId",
                table: "ContactForms",
                column: "VetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactForms_Pets_PetId",
                table: "ContactForms",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactForms_Vets_VetId",
                table: "ContactForms",
                column: "VetId",
                principalTable: "Vets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VetsServices_Services_ServiceId1",
                table: "VetsServices",
                column: "ServiceId1",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactForms_Pets_PetId",
                table: "ContactForms");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactForms_Vets_VetId",
                table: "ContactForms");

            migrationBuilder.DropForeignKey(
                name: "FK_VetsServices_Services_ServiceId1",
                table: "VetsServices");

            migrationBuilder.DropIndex(
                name: "IX_VetsServices_ServiceId1",
                table: "VetsServices");

            migrationBuilder.DropIndex(
                name: "IX_ContactForms_PetId",
                table: "ContactForms");

            migrationBuilder.DropIndex(
                name: "IX_ContactForms_VetId",
                table: "ContactForms");

            migrationBuilder.DropColumn(
                name: "ServiceId1",
                table: "VetsServices");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "ContactForms");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "ContactForms");

            migrationBuilder.RenameColumn(
                name: "Accepted",
                table: "ContactForms",
                newName: "Answered");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "VetsServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "VetsServices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Vets",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "ContactForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.CreateIndex(
                name: "IX_VetsServices_OwnerId",
                table: "VetsServices",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_VetsServices_ServiceId",
                table: "VetsServices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_VetsServices_Owners_OwnerId",
                table: "VetsServices",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VetsServices_Services_ServiceId",
                table: "VetsServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

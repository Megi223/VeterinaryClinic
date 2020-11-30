namespace VeterinaryClinic.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AlterReviewsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Owners_OwnerId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Review_OwnerId",
                table: "Reviews",
                newName: "IX_Reviews_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_IsDeleted",
                table: "Reviews",
                newName: "IX_Reviews_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Owners_OwnerId",
                table: "Reviews",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Owners_OwnerId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_OwnerId",
                table: "Review",
                newName: "IX_Review_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_IsDeleted",
                table: "Review",
                newName: "IX_Review_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Owners_OwnerId",
                table: "Review",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

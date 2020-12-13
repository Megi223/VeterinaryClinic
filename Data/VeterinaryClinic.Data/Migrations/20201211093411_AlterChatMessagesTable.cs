namespace VeterinaryClinic.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AlterChatMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverRole",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderRole",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverRole",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "SenderRole",
                table: "ChatMessages");
        }
    }
}

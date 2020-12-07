using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryClinic.Data.Migrations
{
    public partial class AlterChatsRemindersTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Medications_MedicationId",
                table: "Reminders");

            migrationBuilder.DropTable(
                name: "ChatNotifications");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_MedicationId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "MedicationId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "ReceivedOn",
                table: "Reminders");

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Vets_VetId",
                        column: x => x.VetId,
                        principalTable: "Vets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Vets_VetId",
                        column: x => x.VetId,
                        principalTable: "Vets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_IsDeleted",
                table: "ChatMessages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_OwnerId",
                table: "ChatMessages",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_VetId",
                table: "ChatMessages",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_IsDeleted",
                table: "Notifications",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_OwnerId",
                table: "Notifications",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_VetId",
                table: "Notifications",
                column: "VetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Reminders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Reminders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MedicationId",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedOn",
                table: "Reminders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ChatNotifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceivedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VetId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatNotifications_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatNotifications_Vets_VetId",
                        column: x => x.VetId,
                        principalTable: "Vets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_MedicationId",
                table: "Reminders",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatNotifications_IsDeleted",
                table: "ChatNotifications",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ChatNotifications_OwnerId",
                table: "ChatNotifications",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatNotifications_VetId",
                table: "ChatNotifications",
                column: "VetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Medications_MedicationId",
                table: "Reminders",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddingBlindUserEmailSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmailSettingsId",
                table: "BlindUsersEmails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EmailsSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncomingServer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutgoingServer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailsSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsersEmails_EmailSettingsId",
                table: "BlindUsersEmails",
                column: "EmailSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlindUsersEmails_EmailsSettings_EmailSettingsId",
                table: "BlindUsersEmails",
                column: "EmailSettingsId",
                principalTable: "EmailsSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlindUsersEmails_EmailsSettings_EmailSettingsId",
                table: "BlindUsersEmails");

            migrationBuilder.DropTable(
                name: "EmailsSettings");

            migrationBuilder.DropIndex(
                name: "IX_BlindUsersEmails_EmailSettingsId",
                table: "BlindUsersEmails");

            migrationBuilder.DropColumn(
                name: "EmailSettingsId",
                table: "BlindUsersEmails");
        }
    }
}

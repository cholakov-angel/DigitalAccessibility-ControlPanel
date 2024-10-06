using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddingLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlindUsersLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlindUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogCode = table.Column<int>(type: "int", nullable: false),
                    LogText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeOfLog = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlindUsersLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlindUsersLogs_BlindUsers_BlindUserId",
                        column: x => x.BlindUserId,
                        principalTable: "BlindUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsersLogs_BlindUserId",
                table: "BlindUsersLogs",
                column: "BlindUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlindUsersLogs");
        }
    }
}

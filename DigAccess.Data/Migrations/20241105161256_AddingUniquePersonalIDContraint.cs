using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddingUniquePersonalIDContraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BlindUsers_PersonalId",
                table: "BlindUsers",
                column: "PersonalId",
                unique: true,
                filter: "[PersonalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonalId",
                table: "AspNetUsers",
                column: "PersonalId",
                unique: true,
                filter: "[PersonalId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlindUsers_PersonalId",
                table: "BlindUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonalId",
                table: "AspNetUsers");
        }
    }
}

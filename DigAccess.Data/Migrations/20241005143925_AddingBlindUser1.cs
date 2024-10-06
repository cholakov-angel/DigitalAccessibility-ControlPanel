using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddingBlindUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "BlindUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                table: "BlindUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsers_CityId",
                table: "BlindUsers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlindUsers_Cities_CityId",
                table: "BlindUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlindUsers_Cities_CityId",
                table: "BlindUsers");

            migrationBuilder.DropIndex(
                name: "IX_BlindUsers_CityId",
                table: "BlindUsers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "BlindUsers");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "BlindUsers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class DataChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "53c4614c-f814-407f-b798-858a9e20f1d4",
                column: "MasterKey",
                value: "833F1343F4523219514F35441");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                column: "LicenceNumber",
                value: "94334903B343F304426914338");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                column: "LicenceNumber",
                value: "993333D019344634334024553");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                column: "LicenceNumber",
                value: "0490313332F03E45341234944");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "53c4614c-f814-407f-b798-858a9e20f1d4",
                column: "MasterKey",
                value: null);

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                column: "LicenceNumber",
                value: "4234432832383044D34439233");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                column: "LicenceNumber",
                value: "3433512933333432993840444");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                column: "LicenceNumber",
                value: "3114433439430F383915E3342");
        }
    }
}

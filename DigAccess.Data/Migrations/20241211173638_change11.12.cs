using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class change1112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                column: "LicenceNumber",
                value: "02344133D59449333423334E4");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                column: "LicenceNumber",
                value: "3E148333409344924813332F4");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                column: "LicenceNumber",
                value: "4241348433444443931033021");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

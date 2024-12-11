using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class chandsgs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "369ea3ff-9318-4336-b3f9-83436e7ecc8e", "4bae9afb-921c-4732-87be-ab5eb984e4ca" });

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                column: "LicenceNumber",
                value: "39348040330924943B3423334");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                column: "LicenceNumber",
                value: "8344133E20440443231843933");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                column: "LicenceNumber",
                value: "379040451E594443343433433");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "369ea3ff-9318-4336-b3f9-83436e7ecc8e", "4bae9afb-921c-4732-87be-ab5eb984e4ca" });

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                column: "LicenceNumber",
                value: "301EA43134B53493432948333");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                column: "LicenceNumber",
                value: "4933745429333400333424324");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                column: "LicenceNumber",
                value: "3433474103344E94302343325");
        }
    }
}

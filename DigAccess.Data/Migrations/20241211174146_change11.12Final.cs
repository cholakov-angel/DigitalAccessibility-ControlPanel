using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class change1112Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "afcc821c-70c5-448a-a938-4f320fec7689", "36f7ec79-9a12-4317-97ae-74b3476126d8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e556c4eb-7791-4dd2-a69a-c40bd94d002e", "49952198-64dd-4b77-8c46-2e709c663737" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "369ea3ff-9318-4336-b3f9-83436e7ecc8e", "4bae9afb-921c-4732-87be-ab5eb984e4ca" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "e556c4eb-7791-4dd2-a69a-c40bd94d002e", "36f7ec79-9a12-4317-97ae-74b3476126d8" },
                    { "afcc821c-70c5-448a-a938-4f320fec7689", "49952198-64dd-4b77-8c46-2e709c663737" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e556c4eb-7791-4dd2-a69a-c40bd94d002e", "36f7ec79-9a12-4317-97ae-74b3476126d8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "afcc821c-70c5-448a-a938-4f320fec7689", "49952198-64dd-4b77-8c46-2e709c663737" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "afcc821c-70c5-448a-a938-4f320fec7689", "36f7ec79-9a12-4317-97ae-74b3476126d8" },
                    { "e556c4eb-7791-4dd2-a69a-c40bd94d002e", "49952198-64dd-4b77-8c46-2e709c663737" },
                    { "369ea3ff-9318-4336-b3f9-83436e7ecc8e", "4bae9afb-921c-4732-87be-ab5eb984e4ca" }
                });

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
    }
}

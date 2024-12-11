using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class ChangingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "369ea3ff-9318-4336-b3f9-83436e7ecc8e", "53c4614c-f814-407f-b798-858a9e20f1d4" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8643044C-60A0-42EA-AEDC-F34420A362A7", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "afcc821c-70c5-448a-a938-4f320fec7689", "53c4614c-f814-407f-b798-858a9e20f1d4" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ApprovalStatus", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "MasterKey", "MiddleName", "NormalizedEmail", "NormalizedUserName", "OfficeId", "OrganisationId", "PasswordHash", "PersonalId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "38a0776f-2e21-4df8-b0c5-024ed161de97", 0, 1, "8c2b4b54-89ab-42dc-8a5f-fd029b2f9c3d", "ApplicationUser", "admin@admin.com", false, "Ангел", 0, "Чолаков", true, null, "13102334E0473431441342733", "Лъчезаров", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", null, null, "AQAAAAIAAYagAAAAELiPbYWjLWygobjq6+PrDpye2n6rHkaW+BuX8v7k1PbDNMISIwqW1BhDNIefyUu2JQ==", "7512189541", "0890216475", false, "RTP3FUJC3MXXTYKKEJBD7YTGOTX4XKNQ", false, "admin@admin.com" });

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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8643044C-60A0-42EA-AEDC-F34420A362A7", "38a0776f-2e21-4df8-b0c5-024ed161de97" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8643044C-60A0-42EA-AEDC-F34420A362A7", "38a0776f-2e21-4df8-b0c5-024ed161de97" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "afcc821c-70c5-448a-a938-4f320fec7689", "53c4614c-f814-407f-b798-858a9e20f1d4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8643044C-60A0-42EA-AEDC-F34420A362A7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38a0776f-2e21-4df8-b0c5-024ed161de97");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "369ea3ff-9318-4336-b3f9-83436e7ecc8e", "53c4614c-f814-407f-b798-858a9e20f1d4" });

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                column: "LicenceNumber",
                value: "633233F3934E11B3240953444");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                column: "LicenceNumber",
                value: "43344809F0933812331B33344");

            migrationBuilder.UpdateData(
                table: "BlindUsersLicences",
                keyColumn: "Id",
                keyValue: new Guid("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                column: "LicenceNumber",
                value: "3434201450433354443419354");
        }
    }
}

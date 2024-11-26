using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("111ce6a2-bacc-4575-b9a4-c1b3106a86da"), "Бургас" },
                    { new Guid("65561ce7-3410-4c7a-b37f-86184a174a69"), "Варна" },
                    { new Guid("6b827b51-65a3-4b0c-a59e-9f4d56951f82"), "Монтана" },
                    { new Guid("7c46394f-7cea-4ecf-80c8-7f0f65fd4534"), "Пловдив" },
                    { new Guid("9ac5590a-6946-471f-812f-503544b3fba7"), "Русе" },
                    { new Guid("bfbfa089-59d7-4704-bd00-98cd15ea56d5"), "Видин" },
                    { new Guid("cc974363-80a0-47c1-8433-039d4bf99fd0"), "София" },
                    { new Guid("d4643e31-fb2d-4d97-b2d9-5b8cdbf3a09e"), "Кърджали" },
                    { new Guid("e68a980e-dfc3-4bbc-a439-000f02ed4331"), "Благоевград" },
                    { new Guid("eb68b7bd-6b9f-4ed4-a542-2467f885d6e7"), "Враца" }
                });

            migrationBuilder.InsertData(
                table: "EmailsSettings",
                columns: new[] { "Id", "IncomingServer", "Name", "OutgoingServer", "Port" },
                values: new object[,]
                {
                    { new Guid("5cd666d8-0009-46f6-84dc-00e30fc07501"), "outlook.office365.com", "Microsoft Outlook", "smtp-mail.outlook.com", 993 },
                    { new Guid("66f45e2c-256e-4bdc-a449-347bfc97d1fa"), "imap.gmail.com", "Google Gmail", "smpt.gmail.com", 993 },
                    { new Guid("ddbc2773-69fd-499f-95f2-87068dbe2d58"), "imap.mail.yahoo.com", "Yahoo Mail", "smtp.mail.yahoo.com", 993 }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "IsLicenceKeyRequired", "Name" },
                values: new object[,]
                {
                    { new Guid("564c9939-9c1d-44a5-bc8a-f4cb16bce86a"), true, "ChatGPT" },
                    { new Guid("8855d892-574a-434a-b347-b88c716608b6"), false, "Wikipedia" }
                });

            migrationBuilder.InsertData(
                table: "Organisations",
                columns: new[] { "Id", "Name", "National_Phone" },
                values: new object[,]
                {
                    { new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"), "Съюз на слепите", "028127050" },
                    { new Guid("db7b29f9-8974-463b-8937-a28e3dc8d90f"), "Фондация Хоризонти", "24448858" }
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "CityId", "LocalPhone", "Name", "OrganisationId", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { new Guid("0b919734-b960-436e-85a8-0e01eceab13d"), new Guid("cc974363-80a0-47c1-8433-039d4bf99fd0"), "024448858", "Учебна зала - Фондация Хоризонти", new Guid("db7b29f9-8974-463b-8937-a28e3dc8d90f"), "бул. \"Цариградско шосе\"“", 125 },
                    { new Guid("89729ab3-fe73-49af-9bcf-97cf00c49e2f"), new Guid("cc974363-80a0-47c1-8433-039d4bf99fd0"), "0886100156", "Съюз на слепите София", new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"), "ул. Цар Симеон", 110 },
                    { new Guid("c8723a30-71ec-4d7d-8017-b0df2900a455"), new Guid("9ac5590a-6946-471f-812f-503544b3fba7"), "082841847", "Съюз на слепите - РСО Русе", new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"), "ж.к. Възраждане, ул. „Митрополит Григорий“", 3 },
                    { new Guid("eba30287-4914-4a15-bb3c-2dd3d53c918f"), new Guid("65561ce7-3410-4c7a-b37f-86184a174a69"), "052732151", "Съюз на слепите - РСО Варна", new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"), "ул. Петко Стайнов", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("111ce6a2-bacc-4575-b9a4-c1b3106a86da"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6b827b51-65a3-4b0c-a59e-9f4d56951f82"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7c46394f-7cea-4ecf-80c8-7f0f65fd4534"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bfbfa089-59d7-4704-bd00-98cd15ea56d5"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d4643e31-fb2d-4d97-b2d9-5b8cdbf3a09e"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e68a980e-dfc3-4bbc-a439-000f02ed4331"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("eb68b7bd-6b9f-4ed4-a542-2467f885d6e7"));

            migrationBuilder.DeleteData(
                table: "EmailsSettings",
                keyColumn: "Id",
                keyValue: new Guid("5cd666d8-0009-46f6-84dc-00e30fc07501"));

            migrationBuilder.DeleteData(
                table: "EmailsSettings",
                keyColumn: "Id",
                keyValue: new Guid("66f45e2c-256e-4bdc-a449-347bfc97d1fa"));

            migrationBuilder.DeleteData(
                table: "EmailsSettings",
                keyColumn: "Id",
                keyValue: new Guid("ddbc2773-69fd-499f-95f2-87068dbe2d58"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("564c9939-9c1d-44a5-bc8a-f4cb16bce86a"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("8855d892-574a-434a-b347-b88c716608b6"));

            migrationBuilder.DeleteData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: new Guid("0b919734-b960-436e-85a8-0e01eceab13d"));

            migrationBuilder.DeleteData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: new Guid("89729ab3-fe73-49af-9bcf-97cf00c49e2f"));

            migrationBuilder.DeleteData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: new Guid("c8723a30-71ec-4d7d-8017-b0df2900a455"));

            migrationBuilder.DeleteData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: new Guid("eba30287-4914-4a15-bb3c-2dd3d53c918f"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("65561ce7-3410-4c7a-b37f-86184a174a69"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("9ac5590a-6946-471f-812f-503544b3fba7"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("cc974363-80a0-47c1-8433-039d4bf99fd0"));

            migrationBuilder.DeleteData(
                table: "Organisations",
                keyColumn: "Id",
                keyValue: new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"));

            migrationBuilder.DeleteData(
                table: "Organisations",
                keyColumn: "Id",
                keyValue: new Guid("db7b29f9-8974-463b-8937-a28e3dc8d90f"));
        }
    }
}

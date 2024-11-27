using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigAccess.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLicenceKeyRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    National_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocalPhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StreetNumber = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offices_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalId = table.Column<string>(type: "CHAR(10)", maxLength: 10, nullable: true),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MasterKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    OrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlindUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    PersonalId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TELKNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdministratorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StreetNumber = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlindUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlindUsers_AspNetUsers_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlindUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAnswered = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlindUsersEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlindUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlindUsersEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlindUsersEmails_BlindUsers_BlindUserId",
                        column: x => x.BlindUserId,
                        principalTable: "BlindUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlindUsersEmails_EmailsSettings_EmailSettingsId",
                        column: x => x.EmailSettingsId,
                        principalTable: "EmailsSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlindUsersFeatures",
                columns: table => new
                {
                    BlindUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenceKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlindUsersFeatures", x => new { x.BlindUserId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_BlindUsersFeatures_BlindUsers_BlindUserId",
                        column: x => x.BlindUserId,
                        principalTable: "BlindUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlindUsersFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlindUsersLicences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlindUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MacAddress = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    DateOfActivation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlindUsersLicences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlindUsersLicences_BlindUsers_BlindUserId",
                        column: x => x.BlindUserId,
                        principalTable: "BlindUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10e455ec-e314-4bad-8228-040bf2c9f43f", null, "WaitingApproval", "WAITINGAPPROVAL" },
                    { "369ea3ff-9318-4336-b3f9-83436e7ecc8e", null, "OfficeWorker", "OFFICEWORKER" },
                    { "6f78d6a0-481a-4343-859f-f3eaa5d873df", null, "OrgAdministrator", "ORGADMINISTRATOR" },
                    { "afcc821c-70c5-448a-a938-4f320fec7689", null, "UserAdministrator", "USERADMINISTRATOR" },
                    { "e556c4eb-7791-4dd2-a69a-c40bd94d002e", null, "OfficeAdministrator", "OFFICEADMINISTRATOR" }
                });

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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ApprovalStatus", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "MasterKey", "MiddleName", "NormalizedEmail", "NormalizedUserName", "OfficeId", "OrganisationId", "PasswordHash", "PersonalId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "14423824-2618-46fb-b9fb-38666f84d6e9", 0, 1, "5d9475e6-7527-481e-afab-b0bdf4c69e4a", "ApplicationUser", "horizonti@org.com", false, "Стефан", 0, "Стефанов", true, null, "833E1343F4523319514F35444", "Стефанов", "HORIZONTI@ORG.COM", "HORIZONTI@ORG.COM", null, new Guid("db7b29f9-8974-463b-8937-a28e3dc8d90f"), "AQAAAAIAAYagAAAAENreOGd42QJ4SEZUywLy1srF3OCAL4bAxvaqM92L3wPvKPAR3UWiMQg5qA4Q6Vd4EA==", "0151169845", "0867457815", false, "2N6K6Y6Q4MXGCNTPNZQNWHFE3FUF45NY", false, "horizonti@org.com" },
                    { "89f5afab-4dba-48e5-9375-94c8543bfc48", 0, 1, "27d1d3fa-bcfe-4018-bb03-dfa82fce882a", "ApplicationUser", "blindunion@org.com", false, "Петър", 0, "Петров", true, null, null, "Петров", "BLINDUNION@ORG.COM", "BLINDUNION@ORG.COM", null, new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"), "AQAAAAIAAYagAAAAELL4+GOCpW1GGuK0scEGkRoA88sy6YeKU012G88HWU/bItaxcvn1DG+ZKXL/JBua8w==", "7512059861", "0895446985", false, "JKX7PPCCKW4WFU6P3TY2PKN4SV3QOW7D", false, "blindunion@org.com" }
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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6f78d6a0-481a-4343-859f-f3eaa5d873df", "14423824-2618-46fb-b9fb-38666f84d6e9" },
                    { "6f78d6a0-481a-4343-859f-f3eaa5d873df", "89f5afab-4dba-48e5-9375-94c8543bfc48" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ApprovalStatus", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "MasterKey", "MiddleName", "NormalizedEmail", "NormalizedUserName", "OfficeId", "OrganisationId", "PasswordHash", "PersonalId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "36f7ec79-9a12-4317-97ae-74b3476126d8", 0, 1, "b620419e-00d7-4602-b595-c1fa4e29c40d", "ApplicationUser", "petar.petrov1@gmail.com", false, "Петър", 0, "Стефанов", true, null, "833E1343F4523319514F35444", "Петров", "PETAR.PETROV1@GMAIL.COM", "PETAR.PETROV1@GMAIL.COM", new Guid("89729ab3-fe73-49af-9bcf-97cf00c49e2f"), new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"), "AQAAAAIAAYagAAAAEETkT0AGEK0+Xt9VDugMLgHXDGjQFftwWHbEObNJAPAYGHS+7SST6NYFx9LG3t9Rww==", "7912159865", "0895648985", false, "NDCJYKMQVBYWCNFWA4LUGYCK44BWXFXA", false, "petar.petrov1@gmail.com" },
                    { "49952198-64dd-4b77-8c46-2e709c663737", 0, 1, "38be0e6e-2b0b-44de-a407-7acbba9d4f58", "ApplicationUser", "Angel.Borisov@admin.com", false, "Ангел", 0, "Борисов", true, null, null, "Николов", "ANGEL.BORISOV@ADMIN.COM", "ANGEL.BORISOV@ADMIN.COM", new Guid("eba30287-4914-4a15-bb3c-2dd3d53c918f"), new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"), "AQAAAAIAAYagAAAAEJbq30iBdYk6UXkXeWcDCMC0qIuJL9xgRqA4d1FImLRtIMzPyI7T+ty9MQ8VWqebDQ==", "8808169869", "0896947986", false, "J6YSNRI2SP6JEEUDMSZOY6N6O3MCXG7A", false, "Angel.Borisov@admin.com" },
                    { "4bae9afb-921c-4732-87be-ab5eb984e4ca", 0, 1, "ac9fe303-f9cb-499b-9bdc-d529c4c53997", "ApplicationUser", "zoia.stefanova@worker.com", false, "Зоя", 1, "Николова", true, null, null, "Стефанова", "ZOIA.STEFANOVA@WORKER.COM", "ZOIA.STEFANOVA@WORKER.COM", new Guid("89729ab3-fe73-49af-9bcf-97cf00c49e2f"), new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"), "AQAAAAIAAYagAAAAEM3bsQmvXGw2pc7tq4zT4qX9HfRb/doo3FGUN8yOH7EoEAAEV8ZFIwoC8i64YZHB2A==", "7805059878", "0895447985", false, "W7QE7CBRYKNQZ3KW3TL3OY3RAWIACOV2", false, "zoia.stefanova@worker.com" },
                    { "53c4614c-f814-407f-b798-858a9e20f1d4", 0, 1, "62ff3364-38f4-4963-b34a-4374055f778a", "ApplicationUser", "nikolay.nikolov@admin.com", false, "Николай", 0, "Николова", true, null, null, "Карашимов", "NIKOLAY.NIKOLOV@ADMIN.COM", "NIKOLAY.NIKOLOV@ADMIN.COM", new Guid("89729ab3-fe73-49af-9bcf-97cf00c49e2f"), new Guid("c5e270c8-df31-40ec-938e-d1300457f3ef"), "AQAAAAIAAYagAAAAEJt44Jxyi0L3HAZY5seapTblWHInH6nrhZ3lhMKo2ZFkcfkkh/UYO7EIzhL+ojpcxQ==", "9704059669", "0895447985", false, "VWXASGBD55KBOPAXBKNT355V3K7ENO3S", false, "nikolay.nikolov@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "afcc821c-70c5-448a-a938-4f320fec7689", "36f7ec79-9a12-4317-97ae-74b3476126d8" },
                    { "e556c4eb-7791-4dd2-a69a-c40bd94d002e", "49952198-64dd-4b77-8c46-2e709c663737" },
                    { "369ea3ff-9318-4336-b3f9-83436e7ecc8e", "4bae9afb-921c-4732-87be-ab5eb984e4ca" },
                    { "369ea3ff-9318-4336-b3f9-83436e7ecc8e", "53c4614c-f814-407f-b798-858a9e20f1d4" }
                });

            migrationBuilder.InsertData(
                table: "BlindUsers",
                columns: new[] { "Id", "AdministratorId", "Birthdate", "CityId", "FirstName", "Gender", "LastName", "MiddleName", "PersonalId", "Street", "StreetNumber", "TELKNumber" },
                values: new object[,]
                {
                    { new Guid("2b143304-b5f0-4029-ba97-449f09e66649"), "36f7ec79-9a12-4317-97ae-74b3476126d8", new DateTime(2002, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cc974363-80a0-47c1-8433-039d4bf99fd0"), "Ангел", 0, "Петров", "Борисов", "0252199847", "бул. Ломско шосе", 250, "0598415698" },
                    { new Guid("4376ee53-2314-4d99-87b6-c08d9aecabcc"), "36f7ec79-9a12-4317-97ae-74b3476126d8", new DateTime(1995, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6b827b51-65a3-4b0c-a59e-9f4d56951f82"), "Калин", 0, "Иванов", "Цветанов", "9512099847", "бул. Трети март", 19, "0195415679" },
                    { new Guid("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"), "36f7ec79-9a12-4317-97ae-74b3476126d8", new DateTime(1999, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9ac5590a-6946-471f-812f-503544b3fba7"), "Цветелина", 1, "Петрова", "Ангелова", "9902199878", "бул. България", 190, "0195415679" }
                });

            migrationBuilder.InsertData(
                table: "BlindUsersFeatures",
                columns: new[] { "BlindUserId", "FeatureId", "LicenceKey" },
                values: new object[] { new Guid("2b143304-b5f0-4029-ba97-449f09e66649"), new Guid("8855d892-574a-434a-b347-b88c716608b6"), "" });

            migrationBuilder.InsertData(
                table: "BlindUsersLicences",
                columns: new[] { "Id", "BlindUserId", "DateFrom", "DateOfActivation", "IsActivated", "LicenceNumber", "MacAddress" },
                values: new object[,]
                {
                    { new Guid("01b89966-4d17-458c-92f9-8dc54bb4b973"), new Guid("4376ee53-2314-4d99-87b6-c08d9aecabcc"), new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "E344E4313742315F334043335", null },
                    { new Guid("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"), new Guid("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"), new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "83235043E3495304444393339", null },
                    { new Guid("50fe5fd4-d950-4c24-aeb6-f03ffe011876"), new Guid("2b143304-b5f0-4029-ba97-449f09e66649"), new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "3E33544534231453442234303", null }
                });

            migrationBuilder.InsertData(
                table: "BlindUsersLogs",
                columns: new[] { "Id", "BlindUserId", "DateTimeOfLog", "LogCode", "LogText", "LogType" },
                values: new object[,]
                {
                    { new Guid("7589b531-e7a1-4d77-b44f-0c4fbe62c4bc"), new Guid("4376ee53-2314-4d99-87b6-c08d9aecabcc"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, "Въведеният ключ за ChatGPT е невалиден!", "Грешка" },
                    { new Guid("e7d6b236-fcd3-4333-8f57-32df271bb06f"), new Guid("4376ee53-2314-4d99-87b6-c08d9aecabcc"), new DateTime(2023, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, "Въведеният ключ за ChatGPT е невалиден!", "Грешка" },
                    { new Guid("fe7eb809-4416-462b-9708-3c4d68d5efcf"), new Guid("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"), new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, "Въведеният ключ за ChatGPT е невалиден!", "Грешка" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OfficeId",
                table: "AspNetUsers",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrganisationId",
                table: "AspNetUsers",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonalId",
                table: "AspNetUsers",
                column: "PersonalId",
                unique: true,
                filter: "[PersonalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsers_AdministratorId",
                table: "BlindUsers",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsers_CityId",
                table: "BlindUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsers_PersonalId",
                table: "BlindUsers",
                column: "PersonalId",
                unique: true,
                filter: "[PersonalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsersEmails_BlindUserId",
                table: "BlindUsersEmails",
                column: "BlindUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsersEmails_EmailSettingsId",
                table: "BlindUsersEmails",
                column: "EmailSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsersFeatures_FeatureId",
                table: "BlindUsersFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsersLicences_BlindUserId",
                table: "BlindUsersLicences",
                column: "BlindUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlindUsersLogs_BlindUserId",
                table: "BlindUsersLogs",
                column: "BlindUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CityId",
                table: "Offices",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_OrganisationId",
                table: "Offices",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserId",
                table: "Questions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BlindUsersEmails");

            migrationBuilder.DropTable(
                name: "BlindUsersFeatures");

            migrationBuilder.DropTable(
                name: "BlindUsersLicences");

            migrationBuilder.DropTable(
                name: "BlindUsersLogs");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EmailsSettings");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "BlindUsers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Organisations");
        }
    }
}

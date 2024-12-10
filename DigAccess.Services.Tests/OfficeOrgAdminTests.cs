using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Common;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Blind;
using DigAccess.Data.Entities.Enums;
using DigAccess.Data.Entities.Organisation.Organisation;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Data.Entities;
using DigAccess.Services.Interfaces;
using DigAccess.Services.OrgAdministrator;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using DigAccess.Models.OrgAdministrator;

namespace DigAccess.Services.Tests
{
    [TestFixture]
    public class OfficeOrgAdmin
    {
        private DigAccessDbContext context;
        private List<City> cities;
        private List<OrganisationCompany> organisations;
        private List<Office> offices;
        private List<ApplicationUser> users;
        private List<BlindUser> blindUsers;
        private List<BlindUserLicence> blindLicences;
        private List<IdentityRole> roles;
        private List<BlindUserLog> logs;
        private List<IdentityUserRole<string>> userRoles;
        private IOfficeDetailsService service;
        private UserManager<ApplicationUser> userManager;

        [SetUp]
        public void SetUp()
        {
            organisations = new List<OrganisationCompany>()
            {
                new OrganisationCompany()
                {
                    Id = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef"), 
                    Name = "Съюз на слепите",
                    National_Phone = "028127050"
                },
                new OrganisationCompany()
                {
                    Id = Guid.Parse("db7b29f9-8974-463b-8937-a28e3dc8d90f"),
                    Name = "Фондация Хоризонти",
                    National_Phone = "24448858"
                }
            };
            cities = new List<City>()
            {
                new City() { Id = Guid.Parse("cc974363-80a0-47c1-8433-039d4bf99fd0"), Name = "София" },
                new City() { Id = Guid.Parse("65561ce7-3410-4c7a-b37f-86184a174a69"), Name = "Варна" },
                new City() { Id = Guid.Parse("eb68b7bd-6b9f-4ed4-a542-2467f885d6e7"), Name = "Враца" },
                new City() { Id = Guid.Parse("6b827b51-65a3-4b0c-a59e-9f4d56951f82"), Name = "Монтана" },
                new City() { Id = Guid.Parse("111ce6a2-bacc-4575-b9a4-c1b3106a86da"), Name = "Бургас" },
                new City() { Id = Guid.Parse("7c46394f-7cea-4ecf-80c8-7f0f65fd4534"), Name = "Пловдив" },
                new City() { Id = Guid.Parse("e68a980e-dfc3-4bbc-a439-000f02ed4331"), Name = "Благоевград" },
                new City() { Id = Guid.Parse("9ac5590a-6946-471f-812f-503544b3fba7"), Name = "Русе" },
                new City() { Id = Guid.Parse("bfbfa089-59d7-4704-bd00-98cd15ea56d5"), Name = "Видин" },
                new City() { Id = Guid.Parse("d4643e31-fb2d-4d97-b2d9-5b8cdbf3a09e"), Name = "Кърджали" }
            };
            offices = new List<Office>()
            {
                new Office()
                {
                    Id = GuidParser.GuidParse("89729ab3-fe73-49af-9bcf-97cf00c49e2f"),
                    Name = "Съюз на слепите София",
                    OrganisationId = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef"),
                    LocalPhone = "0886100156",
                    StreetNumber = 110,
                    Street = "ул. Цар Симеон",
                    CityId = Guid.Parse("cc974363-80a0-47c1-8433-039d4bf99fd0")
                },
                new Office()
                {
                    Id = GuidParser.GuidParse("eba30287-4914-4a15-bb3c-2dd3d53c918f"),
                    Name = "Съюз на слепите - РСО Варна",
                    OrganisationId = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef"),
                    LocalPhone = "052732151",
                    StreetNumber = 3,
                    Street = "ул. Петко Стайнов",
                    CityId = Guid.Parse("65561ce7-3410-4c7a-b37f-86184a174a69")
                },
                new Office()
                {
                    Id = GuidParser.GuidParse("c8723a30-71ec-4d7d-8017-b0df2900a455"),
                    Name = "Съюз на слепите - РСО Русе",
                    OrganisationId = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef"),
                    LocalPhone = "082841847",
                    StreetNumber = 3,
                    Street = "ж.к. Възраждане, ул. „Митрополит Григорий“",
                    CityId = Guid.Parse("9ac5590a-6946-471f-812f-503544b3fba7")
                },
                new Office()
                {
                    Id = GuidParser.GuidParse("0b919734-b960-436e-85a8-0e01eceab13d"),
                    Name = "Учебна зала - Фондация Хоризонти",
                    OrganisationId = Guid.Parse("DB7B29F9-8974-463B-8937-A28E3DC8D90F"),
                    LocalPhone = "024448858",
                    StreetNumber = 125,
                    Street = "бул. \"Цариградско шосе\"“",
                    CityId = Guid.Parse("cc974363-80a0-47c1-8433-039d4bf99fd0")
                }
            };
            blindLicences = new List<BlindUserLicence>();
            users = new List<ApplicationUser>()

            {
                new ApplicationUser()
                {
                    Id = "14423824-2618-46fb-b9fb-38666f84d6e9",
                    UserName = "horizonti@org.com",
                    NormalizedUserName = "HORIZONTI@ORG.COM",
                    Email = "horizonti@org.com",
                    NormalizedEmail = "HORIZONTI@ORG.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "AQAAAAIAAYagAAAAENreOGd42QJ4SEZUywLy1srF3OCAL4bAxvaqM92L3wPvKPAR3UWiMQg5qA4Q6Vd4EA==",
                    SecurityStamp = "2N6K6Y6Q4MXGCNTPNZQNWHFE3FUF45NY",
                    ConcurrencyStamp = "5d9475e6-7527-481e-afab-b0bdf4c69e4a",
                    PhoneNumber = "0867457815",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    FirstName = "Стефан",
                    MiddleName = "Стефанов",
                    LastName = "Стефанов",
                    OfficeId = null,
                    PersonalId = "0151169845",
                    MasterKey = "833E1343F4523319514F35444",
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    ApprovalStatus = 1,
                    Gender = Data.Entities.Enums.Gender.Мъж,
                    OrganisationId = Guid.Parse("DB7B29F9-8974-463B-8937-A28E3DC8D90F")
                },
                new ApplicationUser()
                {
                    Id = "DB3BF8FB-2A77-44CA-91EB-5DA2703A4B18",
                    UserName = "waiting@org.com",
                    NormalizedUserName = "waiting@ORG.COM",
                    Email = "waiting@org.com",
                    NormalizedEmail = "waiting@ORG.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "AQAAAAIAAYagAAAAENreOGd42QJ4SEZUywLy1srF3OCAL4bAxvaqM92L3wPvKPAR3UWiMQg5qA4Q6Vd4EA==",
                    SecurityStamp = "2N6K6Y6Q4MXGCNTPNZQNWHFE3FUF45NY",
                    ConcurrencyStamp = "5d9475e6-7527-481e-afab-b0bdf4c69e4a",
                    PhoneNumber = "0867457815",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    FirstName = "Стефан",
                    MiddleName = "Стефанов",
                    LastName = "Стефанов",
                    OfficeId = Guid.Parse("89729AB3-FE73-49AF-9BCF-97CF00C49E2F"),
                    PersonalId = "0151269845",
                    MasterKey = "833E1313F4523319514F35444",
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    ApprovalStatus = 1,
                    Gender = Data.Entities.Enums.Gender.Мъж,
                    OrganisationId = Guid.Parse("C5E270C8-DF31-40EC-938E-D1300457F3EF")
                },
                new ApplicationUser()
                {
                    Id = "36f7ec79-9a12-4317-97ae-74b3476126d8",
                    UserName = "petar.petrov1@gmail.com",
                    NormalizedUserName = "PETAR.PETROV1@GMAIL.COM",
                    Email = "petar.petrov1@gmail.com",
                    NormalizedEmail = "PETAR.PETROV1@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "AQAAAAIAAYagAAAAEETkT0AGEK0+Xt9VDugMLgHXDGjQFftwWHbEObNJAPAYGHS+7SST6NYFx9LG3t9Rww==",
                    SecurityStamp = "NDCJYKMQVBYWCNFWA4LUGYCK44BWXFXA",
                    ConcurrencyStamp = "b620419e-00d7-4602-b595-c1fa4e29c40d",
                    PhoneNumber = "0895648985",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    FirstName = "Петър",
                    MiddleName = "Петров",
                    LastName = "Стефанов",
                    OfficeId = Guid.Parse("89729AB3-FE73-49AF-9BCF-97CF00C49E2F"),
                    PersonalId = "7912159865",
                    MasterKey = "833E1343F4523319514F35444",
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    ApprovalStatus = 1,
                    Gender = Data.Entities.Enums.Gender.Мъж,
                    OrganisationId = Guid.Parse("C5E270C8-DF31-40EC-938E-D1300457F3EF")
                },
                new ApplicationUser()
                {
                    Id = "49952198-64dd-4b77-8c46-2e709c663737",
                    UserName = "Angel.Borisov@admin.com",
                    NormalizedUserName = "ANGEL.BORISOV@ADMIN.COM",
                    Email = "Angel.Borisov@admin.com",
                    NormalizedEmail = "ANGEL.BORISOV@ADMIN.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "AQAAAAIAAYagAAAAEJbq30iBdYk6UXkXeWcDCMC0qIuJL9xgRqA4d1FImLRtIMzPyI7T+ty9MQ8VWqebDQ==",
                    SecurityStamp = "J6YSNRI2SP6JEEUDMSZOY6N6O3MCXG7A",
                    ConcurrencyStamp = "38be0e6e-2b0b-44de-a407-7acbba9d4f58",
                    PhoneNumber = "0896947986",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    FirstName = "Ангел",
                    MiddleName = "Николов",
                    LastName = "Борисов",
                    OfficeId = Guid.Parse("EBA30287-4914-4A15-BB3C-2DD3D53C918F"),
                    PersonalId = "8808169869",
                    MasterKey = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    ApprovalStatus = 1,
                    Gender = Data.Entities.Enums.Gender.Мъж,
                    OrganisationId = Guid.Parse("C5E270C8-DF31-40EC-938E-D1300457F3EF")
                },
                new ApplicationUser()
                {
                    Id = "4bae9afb-921c-4732-87be-ab5eb984e4ca",
                    UserName = "zoia.stefanova@worker.com",
                    NormalizedUserName = "ZOIA.STEFANOVA@WORKER.COM",
                    Email = "zoia.stefanova@worker.com",
                    NormalizedEmail = "ZOIA.STEFANOVA@WORKER.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "AQAAAAIAAYagAAAAEM3bsQmvXGw2pc7tq4zT4qX9HfRb/doo3FGUN8yOH7EoEAAEV8ZFIwoC8i64YZHB2A==",
                    SecurityStamp = "W7QE7CBRYKNQZ3KW3TL3OY3RAWIACOV2",
                    ConcurrencyStamp = "ac9fe303-f9cb-499b-9bdc-d529c4c53997",
                    PhoneNumber = "0895447985",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    FirstName = "Зоя",
                    MiddleName = "Стефанова",
                    LastName = "Николова",
                    OfficeId = Guid.Parse("89729AB3-FE73-49AF-9BCF-97CF00C49E2F"),
                    PersonalId = "7805059878",
                    MasterKey = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    ApprovalStatus = 1,
                    Gender = Data.Entities.Enums.Gender.Жена,
                    OrganisationId = Guid.Parse("C5E270C8-DF31-40EC-938E-D1300457F3EF")
                },
                new ApplicationUser()
                {
                    Id = "53c4614c-f814-407f-b798-858a9e20f1d4",
                    UserName = "nikolay.nikolov@admin.com",
                    NormalizedUserName = "NIKOLAY.NIKOLOV@ADMIN.COM",
                    Email = "nikolay.nikolov@admin.com",
                    NormalizedEmail = "NIKOLAY.NIKOLOV@ADMIN.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "AQAAAAIAAYagAAAAEJt44Jxyi0L3HAZY5seapTblWHInH6nrhZ3lhMKo2ZFkcfkkh/UYO7EIzhL+ojpcxQ==",
                    SecurityStamp = "VWXASGBD55KBOPAXBKNT355V3K7ENO3S",
                    ConcurrencyStamp = "62ff3364-38f4-4963-b34a-4374055f778a",
                    PhoneNumber = "0895447985",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    FirstName = "Николай",
                    MiddleName = "Карашимов",
                    LastName = "Николова",
                    OfficeId = Guid.Parse("89729AB3-FE73-49AF-9BCF-97CF00C49E2F"),
                    PersonalId = "9704059669",
                    MasterKey = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    ApprovalStatus = 1,
                    Gender = Data.Entities.Enums.Gender.Мъж,
                    OrganisationId = Guid.Parse("C5E270C8-DF31-40EC-938E-D1300457F3EF")
                },
                new ApplicationUser()
                {
                    Id = "89f5afab-4dba-48e5-9375-94c8543bfc48",
                    UserName = "blindunion@org.com",
                    NormalizedUserName = "BLINDUNION@ORG.COM",
                    Email = "blindunion@org.com",
                    NormalizedEmail = "BLINDUNION@ORG.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "AQAAAAIAAYagAAAAELL4+GOCpW1GGuK0scEGkRoA88sy6YeKU012G88HWU/bItaxcvn1DG+ZKXL/JBua8w==",
                    SecurityStamp = "JKX7PPCCKW4WFU6P3TY2PKN4SV3QOW7D",
                    ConcurrencyStamp = "27d1d3fa-bcfe-4018-bb03-dfa82fce882a",
                    PhoneNumber = "0895446985",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    FirstName = "Петър",
                    MiddleName = "Петров",
                    LastName = "Петров",
                    OfficeId = null,
                    PersonalId = "7512059861",
                    MasterKey = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    ApprovalStatus = 1,
                    Gender = Data.Entities.Enums.Gender.Мъж,
                    OrganisationId = Guid.Parse("C5E270C8-DF31-40EC-938E-D1300457F3EF")
                }
            };
            roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = "10e455ec-e314-4bad-8228-040bf2c9f43f",
                    Name = "WaitingApproval",
                    NormalizedName = "WAITINGAPPROVAL"
                },
                new IdentityRole()
                {
                    Id = "369ea3ff-9318-4336-b3f9-83436e7ecc8e",
                    Name = "OfficeWorker",
                    NormalizedName = "OFFICEWORKER"
                },
                new IdentityRole()
                {
                    Id = "6f78d6a0-481a-4343-859f-f3eaa5d873df",
                    Name = "OrgAdministrator",
                    NormalizedName = "ORGADMINISTRATOR"
                },
                new IdentityRole()
                {
                    Id = "afcc821c-70c5-448a-a938-4f320fec7689",
                    Name = "UserAdministrator",
                    NormalizedName = "USERADMINISTRATOR"
                },
                new IdentityRole()
                {
                    Id = "e556c4eb-7791-4dd2-a69a-c40bd94d002e",
                    Name = "OfficeAdministrator",
                    NormalizedName = "OFFICEADMINISTRATOR"
                }
            };
            userRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    RoleId = "6f78d6a0-481a-4343-859f-f3eaa5d873df",
                    UserId = "14423824-2618-46fb-b9fb-38666f84d6e9"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "e556c4eb-7791-4dd2-a69a-c40bd94d002e",
                    UserId = "36f7ec79-9a12-4317-97ae-74b3476126d8"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "afcc821c-70c5-448a-a938-4f320fec7689",
                    UserId = "DB3BF8FB-2A77-44CA-91EB-5DA2703A4B18"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "369ea3ff-9318-4336-b3f9-83436e7ecc8e",
                    UserId = "4bae9afb-921c-4732-87be-ab5eb984e4ca"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "369ea3ff-9318-4336-b3f9-83436e7ecc8e",
                    UserId = "53c4614c-f814-407f-b798-858a9e20f1d4"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "6f78d6a0-481a-4343-859f-f3eaa5d873df",
                    UserId = "89f5afab-4dba-48e5-9375-94c8543bfc48"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "10e455ec-e314-4bad-8228-040bf2c9f43f",
                    UserId = "89f5afab-4dba-48e5-9375-94c8543bfc48"
                }
            };
            blindUsers = new List<BlindUser>()
            {
                new BlindUser()
                {
                    Id = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    FirstName = "Ангел",
                    MiddleName = "Борисов",
                    LastName = "Петров",
                    PersonalId = "0252199847",
                    TELKNumber = "0598415698",
                    Birthdate = PersonalIDParser.BirthdateExtract("0252199847"),
                    AdministratorId = "afcc821c-70c5-448a-a938-4f320fec7689",
                    CityId = Guid.Parse("cc974363-80a0-47c1-8433-039d4bf99fd0"),
                    StreetNumber = 250,
                    Street = "бул. Ломско шосе",
                    Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("0252199847"))
                },
                new BlindUser()
                {
                    Id = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    FirstName = "Калин",
                    MiddleName = "Цветанов",
                    LastName = "Иванов",
                    PersonalId = "9512099847",
                    TELKNumber = "0195415679",
                    Birthdate = PersonalIDParser.BirthdateExtract("9512099847"),
                    AdministratorId = "afcc821c-70c5-448a-a938-4f320fec7689",
                    CityId = Guid.Parse("6b827b51-65a3-4b0c-a59e-9f4d56951f82"),
                    StreetNumber = 19,
                    Street = "бул. Трети март",
                    Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("9512099847"))
                },
                new BlindUser()
                {
                    Id = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    FirstName = "Цветелина",
                    MiddleName = "Ангелова",
                    LastName = "Петрова",
                    PersonalId = "9902199878",
                    TELKNumber = "0195415679",
                    Birthdate = PersonalIDParser.BirthdateExtract("9902199878"),
                    AdministratorId = "afcc821c-70c5-448a-a938-4f320fec7689",
                    CityId = Guid.Parse("9ac5590a-6946-471f-812f-503544b3fba7"),
                    StreetNumber = 190,
                    Street = "бул. България",
                    Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("9902199878"))
                }
            };
            logs = new List<BlindUserLog>()
            {
                new BlindUserLog()
                {
                    Id = Guid.Parse("1703FE60-0336-4B18-911D-AB0730F59F52"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LogCode = 100,
                    LogText = "Error",
                    LogType = "Error",
                    DateTimeOfLog = new DateTime(2022, 1, 10)
                }
            };
            var services = new ServiceCollection();

            services.AddDbContext<DigAccessDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>() // Optional: Add Role support
                .AddEntityFrameworkStores<DigAccessDbContext>()
                .AddEntityFrameworkStores<DigAccessDbContext>();

            var serviceProvider = services.BuildServiceProvider();
            this.context = serviceProvider.GetRequiredService<DigAccessDbContext>();

            this.context.AddRange(cities);
            this.context.AddRange(organisations);
            this.context.AddRange(offices);
            this.context.AddRange(users);
            this.context.AddRange(roles);
            this.context.AddRange(userRoles);
            this.context.AddRange(blindUsers);
            this.context.AddRange(logs);
            this.context.SaveChanges();
            userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        } // SetUp

        [Test]
        public async Task GetCities()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);

            var result = await officeOrgAdminService.GetCities();

            Assert.That(result.Count, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task GetOfficeAdmin()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);
            var result = await officeOrgAdminService.GetOfficeAdmin("14423824-2618-46fb-b9fb-38666f84d6e9",
                "36f7ec79-9a12-4317-97ae-74b3476126d8");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task DeleteOfficeInvalid()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);
            bool result = await officeOrgAdminService.DeleteOffice("14423824-2618-46fb-b9fb-38666f84d6e9",
                "eba30287-4914-4a15-bb3c-2dd3d53c918f");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteOffice()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);
            bool result = await officeOrgAdminService.DeleteOffice("14423824-2618-46fb-b9fb-38666f84d6e9",
                "0b919734-b960-436e-85a8-0e01eceab13d");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GetOfficesByName()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);
            var result = await officeOrgAdminService.GetOfficesByName("14423824-2618-46fb-b9fb-38666f84d6e9", "уче");

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetOfficesByNameEmpty()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);
            var result = await officeOrgAdminService.GetOfficesByName("14423824-2618-46fb-b9fb-38666f84d6e9", null);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetOffices()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);
            var result = await officeOrgAdminService.GetOffices("14423824-2618-46fb-b9fb-38666f84d6e9", 1);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AddOffice()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);

            EditOfficeViewModel office = new EditOfficeViewModel();
            office.Name = "Тест";
            office.Phone = "0895648985";
            office.StreetNumber = 160;
            office.CityName = "65561ce7-3410-4c7a-b37f-86184a174a69";
            office.Street = "ул. Петко Славейков";

            bool result = await officeOrgAdminService.AddOffice("14423824-2618-46fb-b9fb-38666f84d6e9", office);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AddOfficeInvalidCity()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);

            EditOfficeViewModel office = new EditOfficeViewModel();
            office.Name = "Тест";
            office.Phone = "0895648985";
            office.StreetNumber = 160;
            office.CityName = "65561ce6-3410-4c7a-b37f-86184a174a69";
            office.Street = "ул. Петко Славейков";

            bool result = await officeOrgAdminService.AddOffice("14423824-2618-46fb-b9fb-38666f84d6e9", office);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditOffice()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);

            EditOfficeViewModel office = new EditOfficeViewModel();
            office.Id = "89729ab3-fe73-49af-9bcf-97cf00c49e2f";
            office.Name = "Тест";
            office.Phone = "0895648985";
            office.StreetNumber = 160;
            office.CityName = "65561ce7-3410-4c7a-b37f-86184a174a69";
            office.Street = "ул. Петко Славейков";

            bool result = await officeOrgAdminService.EditOffice("14423824-2618-46fb-b9fb-38666f84d6e9", office);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task EditOfficeInvalidOffice()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);

            EditOfficeViewModel office = new EditOfficeViewModel();
            office.Id = "89729ab2-fe73-49af-9bcf-97cf00c49e2f";
            office.Name = "Тест";
            office.Phone = "0895648985";
            office.StreetNumber = 160;
            office.CityName = "65561ce7-3410-4c7a-b37f-86184a174a69";
            office.Street = "ул. Петко Славейков";

            bool result = await officeOrgAdminService.EditOffice("14423824-2618-46fb-b9fb-38666f84d6e9", office);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditOfficeInvalidCity()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);

            EditOfficeViewModel office = new EditOfficeViewModel();
            office.Id = "89729ab3-fe73-49af-9bcf-97cf00c49e2f";
            office.Name = "Тест";
            office.Phone = "0895648985";
            office.StreetNumber = 160;
            office.CityName = "65561ce7-3410-4c1a-b37f-86184a174a69";
            office.Street = "ул. Петко Славейков";

            bool result = await officeOrgAdminService.EditOffice("14423824-2618-46fb-b9fb-38666f84d6e9", office);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetFullOfficeDetails()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);

            var result = await officeOrgAdminService.GetFullOfficeDetails("14423824-2618-46fb-b9fb-38666f84d6e9",
                "0b919734-b960-436e-85a8-0e01eceab13d", 1);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task CountWorkers()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);

            int result = await officeOrgAdminService.CountWorkers("14423824-2618-46fb-b9fb-38666f84d6e9",
                "0b919734-b960-436e-85a8-0e01eceab13d");

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task GetOfficeDetails()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);

            var result = await officeOrgAdminService.GetOfficeDetails("14423824-2618-46fb-b9fb-38666f84d6e9",
                "0b919734-b960-436e-85a8-0e01eceab13d");

            Assert.That(result.Name, Is.EqualTo("Учебна зала - Фондация Хоризонти"));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task CountOffices()
        {
            OfficeOrgAdminService officeOrgAdminService = new OfficeOrgAdminService(context, userManager);
            var result = await officeOrgAdminService.CountOffices("14423824-2618-46fb-b9fb-38666f84d6e9");

            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task GetOrganisationDetails()
        {
            OrganisationOrgAdminService orgOrgAdminService = new OrganisationOrgAdminService(context, userManager);
            var result = orgOrgAdminService.GetOrganisationDetails("14423824-2618-46fb-b9fb-38666f84d6e9");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task EditOrganisation()
        {
            OrganisationOrgAdminService orgOrgAdminService = new OrganisationOrgAdminService(context, userManager);
            OrganisationViewModel model = new OrganisationViewModel();
            model.Id = "db7b29f9-8974-463b-8937-a28e3dc8d90f";
            model.Name = "Тест";
            model.National_Phone = "0890216478";

            bool result = await orgOrgAdminService.EditOrganisation("14423824-2618-46fb-b9fb-38666f84d6e9", model);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task EditOrganisationInvalidOffice()
        {
            OrganisationOrgAdminService orgOrgAdminService = new OrganisationOrgAdminService(context, userManager);
            OrganisationViewModel model = new OrganisationViewModel();
            model.Id = "db7b29f9-8974-463b-8931-a28e3dc8d90f";
            model.Name = "Тест";
            model.National_Phone = "0890216478";

            Assert.ThrowsAsync<ArgumentException>(async() => await orgOrgAdminService.EditOrganisation("14423824-2618-46fb-b9fb-38666f84d6e9", model));
        }

        [Test]
        public async Task DoesOfficeHasAnAdminFalse()
        {
            // 89729ab3-fe73-49af-9bcf-97cf00c49e2f
            UsersOrgAdminService usersOrgAdminService = new UsersOrgAdminService(context, userManager);
            bool result = await usersOrgAdminService.DoesOfficeHasAnAdmin("db7b29f9-8974-463b-8931-a28e3dc8d90f");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DoesOfficeHasAnAdminTrue()
        {
            UsersOrgAdminService usersOrgAdminService = new UsersOrgAdminService(context, userManager);
            bool result = await usersOrgAdminService.DoesOfficeHasAnAdmin("89729ab3-fe73-49af-9bcf-97cf00c49e2f");

            Assert.That(result, Is.True);
        }
    }
}



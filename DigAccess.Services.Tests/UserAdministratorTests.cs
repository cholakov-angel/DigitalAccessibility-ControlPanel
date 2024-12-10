using DigAccess.Common;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Organisation.Organisation;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Data.Entities;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Data.Entities.Blind;
using Microsoft.EntityFrameworkCore;
using DigAccess.Data.Entities.Enums;
using DigAccess.Keys;
using DigAccess.Models.UserAdministrator.BlindUser;
using DigAccess.Services.UserAdministrator;

namespace DigAccess.Services.Tests
{
    [TestFixture]
    public class UserAdministratorTests
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
                    Id = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef"), Name = "Съюз на слепите",
                    National_Phone = "028127050"
                },
                new OrganisationCompany()
                {
                    Id = Guid.Parse("db7b29f9-8974-463b-8937-a28e3dc8d90f"), Name = "Фондация Хоризонти",
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
                    OrganisationId = Guid.Parse("db7b29f9-8974-463b-8937-a28e3dc8d90f"),
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
                    Id = "afcc821c-70c5-448a-a938-4f320fec7689",
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
                    UserId = "49952198-64dd-4b77-8c46-2e709c663737"
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
        public async Task GetAllUsers()
        {
            UserAdministratorService userAdminService = new UserAdministratorService(context, userManager);

            var result = await userAdminService.GetAllUsers("afcc821c-70c5-448a-a938-4f320fec7689");

            Assert.That(result.BlindUsers.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetCities()
        {
            BlindUserService blindUserService = new BlindUserService(context, userManager);

            var result = await blindUserService.GetCities();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetUserDetailsValid()
        {
            BlindUserService blindUserService = new BlindUserService(context, userManager);

            var result = await blindUserService.GetUserDetails("2b143304-b5f0-4029-ba97-449f09e66649",
                "afcc821c-70c5-448a-a938-4f320fec7689");

            Assert.That(result.FirstName, Is.EqualTo("Ангел"));
        }

        [Test]
        public async Task GetUserDetailsInvalidUser()
        {
            BlindUserService blindUserService = new BlindUserService(context, userManager);

            Assert.ThrowsAsync<Exception>(async () => await blindUserService.GetUserDetails(
                "2b243304-b5f0-4029-ba97-449f09e66649",
                "afcc821c-70c5-448a-a938-4f320fec7689"));
        }

        [Test]
        public async Task GetUserDetailsInvalidAdmin()
        {
            BlindUserService blindUserService = new BlindUserService(context, userManager);

            Assert.ThrowsAsync<Exception>(async () => await blindUserService.GetUserDetails(
                "2b143304-b5f0-4029-ba97-449f09e66649",
                "afcc921c-70c5-448a-a938-4f320fec7689"));
        }

        [Test]
        public async Task GetAllBlindUsers()
        {
            BlindUserService blindUserService = new BlindUserService(context, userManager);

            var result = await blindUserService.GetAllModels("afcc821c-70c5-448a-a938-4f320fec7689");

            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetBlindUsersByName()
        {
            BlindUserService blindUserService = new BlindUserService(context, userManager);

            var result = await blindUserService.GetModel("afcc821c-70c5-448a-a938-4f320fec7689", "ангел");

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AddBlindUser()
        {
            BlindUserViewModel user = new BlindUserViewModel();
            user.FirstName = "Стефан";
            user.MiddleName = "Борисов";
            user.LastName = "Петров";
            user.City = "cc974363-80a0-47c1-8433-039d4bf99fd0";
            user.Street = "ул. Сан Стефано";
            user.StreetNumber = 16;
            user.PersonalId = "9812151661";
            user.TELKID = "501004165";

            BlindUserService blindUserService = new BlindUserService(context, userManager);
            var result = await blindUserService.Add(user, "afcc821c-70c5-448a-a938-4f320fec7689");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AddBlindUserInvalidCity()
        {
            BlindUserViewModel user = new BlindUserViewModel();
            user.FirstName = "Стефан";
            user.MiddleName = "Борисов";
            user.LastName = "Петров";
            user.City = "cc974363-10a0-47c1-8433-039d4bf99fd0";
            user.Street = "ул. Сан Стефано";
            user.StreetNumber = 16;
            user.PersonalId = "9812151661";
            user.TELKID = "501004165";

            BlindUserService blindUserService = new BlindUserService(context, userManager);
            Assert.ThrowsAsync<Exception>(async () => await blindUserService.Add(user, "afcc821c-70c5-448a-a938-4f320fec7689"));
        }

        [Test]
        public async Task AddBlindUserInvalidPersonalID()
        {
            BlindUserViewModel user = new BlindUserViewModel();
            user.FirstName = "Стефан";
            user.MiddleName = "Борисов";
            user.LastName = "Петров";
            user.City = "cc974363-80a0-47c1-8433-039d4bf99fd0";
            user.Street = "ул. Сан Стефано";
            user.StreetNumber = 16;
            user.PersonalId = "9862151661";
            user.TELKID = "501004165";

            BlindUserService blindUserService = new BlindUserService(context, userManager);
            var result = await blindUserService.Add(user, "afcc821c-70c5-448a-a938-4f320fec7689");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetUserInformation()
        {
            BlindUserService blindUserService = new BlindUserService(context, userManager);
            var result = await blindUserService.GetUserInformation(new DateTime(2022, 12, 15),
                "2b143304-b5f0-4029-ba97-449f09e66649",
                "afcc821c-70c5-448a-a938-4f320fec7689");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetUserInformationInvalid()
        {
            BlindUserService blindUserService = new BlindUserService(context, userManager);
            Assert.ThrowsAsync<Exception>(async () => await blindUserService.GetUserInformation(
                new DateTime(2022, 12, 15),
                "2b143304-b5f0-4029-ba97-449f09e66649",
                "afcc121c-70c5-448a-a938-4f320fec7689"));
        }

        [Test]
        public async Task GetUserMasterKey()
        {
            MasterKeyService masterKeyService = new MasterKeyService(context, userManager);

            var result = await masterKeyService.GetUserMasterKey("14423824-2618-46fb-b9fb-38666f84d6e9");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetLicence()
        {
            blindLicences = new List<BlindUserLicence>()
            {
                new BlindUserLicence()
                {
                    Id = Guid.Parse("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                    BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("АнгелБорисовПетров", "0252199847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 10, 15),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
            };
            await context.BlindUsersLicences.AddRangeAsync(blindLicences);
            await context.SaveChangesAsync();
            LicenceService licenceService = new LicenceService(context, userManager);
            var result = await licenceService.GetLicense("afcc821c-70c5-448a-a938-4f320fec7689",
                "3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetLicenceInvalidUser()
        {
            blindLicences = new List<BlindUserLicence>()
            {
                new BlindUserLicence()
                {
                    Id = Guid.Parse("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                    BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("АнгелБорисовПетров", "0252199847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 10, 15),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
            };
            await context.BlindUsersLicences.AddRangeAsync(blindLicences);
            await context.SaveChangesAsync();
            LicenceService licenceService = new LicenceService(context, userManager);
            Assert.ThrowsAsync<ArgumentException>(async () => await licenceService.GetLicense("afcc121c-70c5-448a-a938-4f320fec7689",
                "3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"));
        }

        [Test]
        public async Task GetLicenceInvalidLicense()
        {
            blindLicences = new List<BlindUserLicence>()
            {
                new BlindUserLicence()
                {
                    Id = Guid.Parse("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                    BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("АнгелБорисовПетров", "0252199847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 10, 15),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
            };
            await context.BlindUsersLicences.AddRangeAsync(blindLicences);
            await context.SaveChangesAsync();
            LicenceService licenceService = new LicenceService(context, userManager);
            Assert.ThrowsAsync<ArgumentException>(async () => await licenceService.GetLicense("afcc821c-70c5-448a-a938-4f320fec7689",
                "3b3ed71b-7b8a-4faf-9cc1-1b6705c02c1f"));
        }

        [Test]
        public async Task GetLicenses()
        {
            blindLicences = new List<BlindUserLicence>()
            {
                new BlindUserLicence()
                {
                    Id = Guid.Parse("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                    BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("АнгелБорисовПетров", "0252199847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 10, 15),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
            };
            await context.BlindUsersLicences.AddRangeAsync(blindLicences);
            await context.SaveChangesAsync();
            LicenceService licenceService = new LicenceService(context, userManager);

            var result = await licenceService.GetLicenses("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da",
                "afcc821c-70c5-448a-a938-4f320fec7689");

            Assert.That(result.Licenses.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetLicensesInvalidUser()
        {
            blindLicences = new List<BlindUserLicence>()
            {
                new BlindUserLicence()
                {
                    Id = Guid.Parse("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                    BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("АнгелБорисовПетров", "0252199847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 10, 15),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
            };
            await context.BlindUsersLicences.AddRangeAsync(blindLicences);
            await context.SaveChangesAsync();
            LicenceService licenceService = new LicenceService(context, userManager);

            Assert.ThrowsAsync<Exception>(async () => await licenceService.GetLicenses("b22c5d23-5aa2-4a91-ae20-c43f16e7b6da",
                "afcc821c-70c5-448a-a938-4f320fec7689"));
        }

        [Test]
        public async Task GetLicensesInvalidAdministrator()
        {
            blindLicences = new List<BlindUserLicence>()
            {
                new BlindUserLicence()
                {
                    Id = Guid.Parse("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                    BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("АнгелБорисовПетров", "0252199847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 10, 15),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
            };
            await context.BlindUsersLicences.AddRangeAsync(blindLicences);
            await context.SaveChangesAsync();
            LicenceService licenceService = new LicenceService(context, userManager);

            Assert.ThrowsAsync<Exception>(async () => await licenceService.GetLicenses("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da",
                "afcc821c-70c5-468a-a938-4f320fec7689"));
        }

        [Test]
        public async Task GenerateLicense()
        {
            LicenceService licenceService = new LicenceService(context, userManager);
            var result = await licenceService.GenerateLicense("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da",
                "afcc821c-70c5-448a-a938-4f320fec7689", new Random(), new DateTime(2022, 10, 1));

            Assert.That(result.DateFrom, Is.EqualTo("01.10.2022"));
        }

        [Test]
        public async Task GenerateLicenseInvalidUser()
        {
            LicenceService licenceService = new LicenceService(context, userManager);
            Assert.ThrowsAsync<Exception>(async () => await licenceService.GenerateLicense("b21c5d21-5aa2-4a91-ae20-c43f16e7b6da",
                "afcc821c-70c5-448a-a938-4f320fec7689", new Random(), new DateTime(2022, 10, 1)));
        }

        [Test]
        public async Task GenerateLicenseInvalidAdmin()
        {
            LicenceService licenceService = new LicenceService(context, userManager);
            Assert.ThrowsAsync<Exception>(async () => await licenceService.GenerateLicense("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da",
                "afcc821c-70c5-448a-a938-4f320fec7679", new Random(), new DateTime(2022, 10, 1)));
        }

        [Test]
        public async Task DeleteLicenseConfirm()
        {
            blindLicences = new List<BlindUserLicence>()
            {
                new BlindUserLicence()
                {
                    Id = Guid.Parse("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                    BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("АнгелБорисовПетров", "0252199847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 10, 15),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
            };
            await context.BlindUsersLicences.AddRangeAsync(blindLicences);
            await context.SaveChangesAsync();
            LicenceService licenceService = new LicenceService(context, userManager);

            var result = await licenceService.DeleteLicenseConfirm(
                "afcc821c-70c5-448a-a938-4f320fec7689", "3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task DeleteLicenseConfirmInvalidUser()
        {
            blindLicences = new List<BlindUserLicence>()
            {
                new BlindUserLicence()
                {
                    Id = Guid.Parse("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                    BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("АнгелБорисовПетров", "0252199847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 10, 15),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
            };
            await context.BlindUsersLicences.AddRangeAsync(blindLicences);
            await context.SaveChangesAsync();
            LicenceService licenceService = new LicenceService(context, userManager);

            Assert.ThrowsAsync<ArgumentException>(async () => await licenceService.DeleteLicenseConfirm(
                "afcc821c-70c5-458a-a938-4f320fec7689", "3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"));
        }

        [Test]
        public async Task DeleteLicense()
        {
            blindLicences = new List<BlindUserLicence>()
            {
                new BlindUserLicence()
                {
                    Id = Guid.Parse("50fe5fd4-d950-4c24-aeb6-f03ffe011876"),
                    BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("АнгелБорисовПетров", "0252199847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 10, 15),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("01b89966-4d17-458c-92f9-8dc54bb4b973"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber =
                        await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
            };
            await context.BlindUsersLicences.AddRangeAsync(blindLicences);
            await context.SaveChangesAsync();
            LicenceService licenceService = new LicenceService(context, userManager);

            var result = await licenceService.DeleteLicense(
                "afcc821c-70c5-448a-a938-4f320fec7689", "3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task DeleteLicenseInvalidUser()
        {
            LicenceService licenceService = new LicenceService(context, userManager);

            Assert.ThrowsAsync<ArgumentException>(async() => await licenceService.DeleteLicense(
                "afcc821c-70c5-448a-a938-4f320fec7689", "3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"));
        }

        [Test]
        public async Task GetLogsInvalidUser()
        {
            LogService logService = new LogService(context, userManager);
            Assert.ThrowsAsync<Exception>(async () => await logService.GetLogs("50fe5fd4-d950-4c24-aeb6-f03ffe011876", "50fe5fd4-d950-4c24-aeb6-f03ffe011876"));
        }

        [Test]
        public async Task GetLogs()
        {
            LogService logService = new LogService(context, userManager);
            var result = await logService.GetLogs("afcc821c-70c5-448a-a938-4f320fec7689",
                "b22c5d21-5aa2-4a91-ae20-c43f16e7b6da");

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task CountLogs()
        {
            LogService logService = new LogService(context, userManager);
            var result = await logService.CountUsers("afcc821c-70c5-448a-a938-4f320fec7689",
                "b22c5d21-5aa2-4a91-ae20-c43f16e7b6da");

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task CountLogsInvalidUser()
        {
            LogService logService = new LogService(context, userManager);
            Assert.ThrowsAsync<Exception>(async() => await logService.CountUsers("afcc821c-70c5-448a-a938-4f320fec7689",
                "b22c5d21-5aa1-4a91-ae20-c43f16e7b6da"));
        }

        [Test]
        public async Task GetLog()
        {
            LogService logService = new LogService(context, userManager);
            var result = await logService.GetLog("afcc821c-70c5-448a-a938-4f320fec7689",
                "1703FE60-0336-4B18-911D-AB0730F59F52");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.LogText, Is.EqualTo("Error"));
        }
    }
}

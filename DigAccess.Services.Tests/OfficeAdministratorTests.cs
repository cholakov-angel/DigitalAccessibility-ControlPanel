using System.Diagnostics.Metrics;
using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Data.Entities.Organisation.Organisation;
using DigAccess.Models.OfficeAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Services.OfficeAdministrator;
using DigAccess.Web.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DigAccess.Data.Entities.Enums;

namespace DigAccess.Services.Tests
{
    [TestFixture]
    public class OfficeAdministratorTests
    {
        private DigAccessDbContext context;
        private List<City> cities;
        private List<OrganisationCompany> organisations;
        private List<Office> offices;
        private List<ApplicationUser> users;
        private List<IdentityRole> roles;
        private List<IdentityUserRole<string>> userRoles;
        private IOfficeDetailsService service;
        private UserManager<ApplicationUser> userManager;
        [SetUp]
        public void SetUp()
        {
            organisations = new List<OrganisationCompany>()
            {
                new OrganisationCompany() { Id = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef"), Name = "Съюз на слепите", National_Phone = "028127050" },
                new OrganisationCompany() { Id = Guid.Parse("db7b29f9-8974-463b-8937-a28e3dc8d90f"), Name = "Фондация Хоризонти", National_Phone = "24448858" }
            };
            cities = new List<City>()
            { new City() { Id = Guid.Parse("cc974363-80a0-47c1-8433-039d4bf99fd0"), Name = "София" },
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
                    PasswordHash = "AQAAAAIAAYagAAAAENreOGd42QJ4SEZUywLy1srF3OCAL4bAxvaqM92L3wPvKPAR3UWiMQg5qA4Q6Vd4EA==",
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
                    Id = "36f7ec79-9a12-4317-97ae-74b3476126d8",
                    UserName = "petar.petrov1@gmail.com",
                    NormalizedUserName = "PETAR.PETROV1@GMAIL.COM",
                    Email = "petar.petrov1@gmail.com",
                    NormalizedEmail = "PETAR.PETROV1@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEETkT0AGEK0+Xt9VDugMLgHXDGjQFftwWHbEObNJAPAYGHS+7SST6NYFx9LG3t9Rww==",
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
                     PasswordHash = "AQAAAAIAAYagAAAAEJbq30iBdYk6UXkXeWcDCMC0qIuJL9xgRqA4d1FImLRtIMzPyI7T+ty9MQ8VWqebDQ==",
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
                     PasswordHash = "AQAAAAIAAYagAAAAEM3bsQmvXGw2pc7tq4zT4qX9HfRb/doo3FGUN8yOH7EoEAAEV8ZFIwoC8i64YZHB2A==",
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
                     PasswordHash = "AQAAAAIAAYagAAAAEJt44Jxyi0L3HAZY5seapTblWHInH6nrhZ3lhMKo2ZFkcfkkh/UYO7EIzhL+ojpcxQ==",
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
                     PasswordHash = "AQAAAAIAAYagAAAAELL4+GOCpW1GGuK0scEGkRoA88sy6YeKU012G88HWU/bItaxcvn1DG+ZKXL/JBua8w==",
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
            this.context.SaveChanges();
            userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        } // SetUp

        public static UserManager<ApplicationUser> CreateMockUserManager(List<ApplicationUser> users,
         List<IdentityUserRole<string>> userRoles,
         List<IdentityRole> roles)
        {
            // Mock IUserStore
            var userStore = new Mock<IUserStore<ApplicationUser>>();

            // Mock IQueryableUserStore for `Users` property
            userStore.As<IQueryableUserStore<ApplicationUser>>()
                     .Setup(x => x.Users)
                     .Returns(users.AsQueryable());

            // Mock IdentityOptions
            var identityOptions = new Mock<IOptions<IdentityOptions>>();
            identityOptions.Setup(o => o.Value).Returns(new IdentityOptions());

            // Mock dependencies
            var passwordHasher = new Mock<IPasswordHasher<ApplicationUser>>();
            var userValidators = new List<IUserValidator<ApplicationUser>> { new Mock<IUserValidator<ApplicationUser>>().Object };
            var passwordValidators = new List<IPasswordValidator<ApplicationUser>> { new Mock<IPasswordValidator<ApplicationUser>>().Object };
            var normalizer = new Mock<ILookupNormalizer>();
            var describer = new Mock<IdentityErrorDescriber>();
            var logger = new Mock<ILogger<UserManager<ApplicationUser>>>();
            var serviceProvider = new Mock<IServiceProvider>();

            // Create Mock UserManager (directly mocked)
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStore.Object,
                identityOptions.Object,
                passwordHasher.Object,
                userValidators,
                passwordValidators,
                normalizer.Object,
                describer.Object,
                serviceProvider.Object,
                logger.Object);

            // Mock FindByIdAsync (GetByIdAsync equivalent)
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                           .ReturnsAsync((string userId) =>
                           {
                               return users.FirstOrDefault(u => u.Id == userId); // Return user by Id
                           });

            // Mock IsInRoleAsync
            userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                           .ReturnsAsync((ApplicationUser user, string role) =>
                           {
                               var userId = user.Id;
                               return userRoles.Any(ur => ur.UserId == userId && roles.Any(r => r.Id == ur.RoleId && r.Name == role));
                           });

            // Mock AddToRoleAsync
            userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                           .ReturnsAsync((ApplicationUser user, string role) =>
                           {
                               var roleEntity = roles.FirstOrDefault(r => r.Name == role);
                               if (roleEntity == null)
                                   return IdentityResult.Failed(new IdentityError { Description = $"Role '{role}' does not exist." });

                               var userRole = new IdentityUserRole<string> { UserId = user.Id, RoleId = roleEntity.Id };
                               if (!userRoles.Any(ur => ur.UserId == user.Id && ur.RoleId == roleEntity.Id))
                               {
                                   userRoles.Add(userRole);
                               }
                               return IdentityResult.Success;
                           });

            return userManagerMock.Object;
        }

        [Test]
        public async Task Cities()
        {
            this.service = new OfficeDetailsService(context, userManager);
            var cities = await service.GetCities();

            Assert.That(10, Is.EqualTo(cities.Count));
        } // Cities

        [Test]
        public async Task GetOfficeFromUser()
        {
            this.service = new OfficeDetailsService(context, userManager);
            var result = await service.GetOfficeDetails("36f7ec79-9a12-4317-97ae-74b3476126d8");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Съюз на слепите София"));
        } // GetOfficeFromUser

        [Test]
        public async Task GetOfficeFromInvalidUserThrowsError()
        {
            this.service = new OfficeDetailsService(context, userManager);
            Assert.ThrowsAsync<ArgumentException>(async () => await service.GetOfficeDetails("36f7ec79-9a12-1317-97ae-74b3476126d8"));
        }

        [Test]
        public async Task UpdateOfficeValidUser()
        {
            OfficeViewModel office = new OfficeViewModel();
            office.Id = "89729AB3-FE73-49AF-9BCF-97CF00C49E2F";
            office.Name = "Променено име";
            office.Organisation = "c5e270c8-df31-40ec-938e-d1300457f3ef";
            office.Phone = "0890216478";
            office.Street = "ул. Черна гора";
            office.StreetNumber = 50;
            office.CityName = "cc974363-80a0-47c1-8433-039d4bf99fd0";

            this.service = new OfficeDetailsService(context, userManager);
            bool result = await service.UpdateOffice("36f7ec79-9a12-4317-97ae-74b3476126d8", office);

            Assert.That(result, Is.True);
        } // UpdateOfficeValidUser

        [Test]
        public async Task UpdateOfficeValidUserInvalidOffice()
        {
            OfficeViewModel office = new OfficeViewModel();
            office.Id = "89729AB3-FE13-49AF-91CF-97CF00C49E2F";
            office.Name = "Променено име";
            office.Organisation = "c5e270c8-df31-40ec-938e-d1300457f3ef";
            office.Phone = "0890216478";
            office.Street = "ул. Черна гора";
            office.StreetNumber = 50;
            office.CityName = "cc974363-80a0-47c1-8433-039d4bf99fd0";

            this.service = new OfficeDetailsService(context, userManager);
            bool result = await service.UpdateOffice("36f7ec79-9a12-4317-97ae-74b3476126d8", office);

            Assert.That(result, Is.False);
        } // UpdateOfficeValidUser

        [Test]
        public async Task UpdateOfficeInvalidUser()
        {
            OfficeViewModel office = new OfficeViewModel();
            office.Id = "89729AB3-FE13-49AF-91CF-97CF00C49E2F";
            office.Name = "Променено име";
            office.Organisation = "c5e270c8-df31-40ec-938e-d1300457f3ef";
            office.Phone = "0890216478";
            office.Street = "ул. Черна гора";
            office.StreetNumber = 50;
            office.CityName = "cc974363-80a0-47c1-8433-039d4bf99fd0";

            this.service = new OfficeDetailsService(context, userManager);
            Assert.ThrowsAsync<ArgumentException>(async () => await service.UpdateOffice("36f7ec71-9a12-4317-97ae-74b3476126d8", office));
        } // UpdateOfficeValidUser

        [Test]
        public async Task GetWorkersValid()
        {
            WorkerOfficeAdminService workersService = new WorkerOfficeAdminService(context, userManager);

            var result = await workersService.GetWorkers("36f7ec79-9a12-4317-97ae-74b3476126d8", 1);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task CountUsers()
        {
            WorkerOfficeAdminService workersService = new WorkerOfficeAdminService(context, userManager);

            var result = await workersService.CountUsers("36f7ec79-9a12-4317-97ae-74b3476126d8");
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public async Task WorkerDetails()
        {
            WorkerOfficeAdminService workersService = new WorkerOfficeAdminService(context, userManager);
            var result = await workersService.WorkerDetails("36f7ec79-9a12-4317-97ae-74b3476126d8",
                "4bae9afb-921c-4732-87be-ab5eb984e4ca");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Email, Is.EqualTo("zoia.stefanova@worker.com"));
        }

        [Test]
        public async Task InvalidWorkerDetails()
        {
            WorkerOfficeAdminService workersService = new WorkerOfficeAdminService(context, userManager);

            Assert.ThrowsAsync<ArgumentException>(async () => await workersService.WorkerDetails(
                "36f7ec79-9a12-4317-97ae-74b3476126d8",
                "4bae9afb-921c-4732-87be-ab5eb984e3ca"));
        }

        [Test]
        public async Task AddWorker()
        {
            AddWorkerViewModel officeWorker = new AddWorkerViewModel();
            officeWorker.Email = "petar.simeonov@gmail.com";
            officeWorker.Password = "Petar1.Simeonov";
            officeWorker.PhoneNumber = "0895648985";
            officeWorker.FirstName = "Петър";
            officeWorker.MiddleName = "Стефанов";
            officeWorker.LastName = "Симеонов";
            officeWorker.PersonalID = "7512189841";

            WorkerOfficeAdminService workersService = new WorkerOfficeAdminService(context, userManager);
            bool result = await workersService.AddOfficeWorker("36f7ec79-9a12-4317-97ae-74b3476126d8", officeWorker);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteUser()
        {
            WorkerOfficeAdminService workersService = new WorkerOfficeAdminService(context, userManager);
            bool result = await workersService.DeleteUser("36f7ec79-9a12-4317-97ae-74b3476126d8",
                "4bae9afb-921c-4732-87be-ab5eb984e4ca");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteUserInvalid()
        {
            WorkerOfficeAdminService workersService = new WorkerOfficeAdminService(context, userManager);
            Assert.ThrowsAsync<ArgumentException>(async () => await workersService.DeleteUser("36f7ec79-9a12-4317-97ae-74b3476126d8",
                "4bae9afb-921c-4732-27be-ab5eb984e4ca"));
        }

        [Test]
        public async Task GetWorkersByName()
        {
            WorkerOfficeAdminService workersService = new WorkerOfficeAdminService(context, userManager);

            var result = await workersService.GetWorkersByName("36f7ec79-9a12-4317-97ae-74b3476126d8", "зоя");

            Assert.That(result.Count, Is.EqualTo(1));
        }
    } // OfficeAdministratorTests
}

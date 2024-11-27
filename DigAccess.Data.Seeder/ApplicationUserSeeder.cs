using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Seeder
{
    public class ApplicationUserSeeder : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
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
                    Gender = Entities.Enums.Gender.Мъж,
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
                    Gender = Entities.Enums.Gender.Мъж,
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
                     Gender = Entities.Enums.Gender.Мъж,
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
                     Gender = Entities.Enums.Gender.Жена,
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
                     Gender = Entities.Enums.Gender.Мъж,
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
                     Gender = Entities.Enums.Gender.Мъж,
                     OrganisationId = Guid.Parse("C5E270C8-DF31-40EC-938E-D1300457F3EF")
                 }
                );
        } // Configure
    }
}

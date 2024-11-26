using DigAccess.Common;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Data.Entities.Organisation.Organisation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Seeder
{
    public class OfficeSeeder : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.HasData(
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
                );
        } // Configure
    } // OfficeSeeder
}

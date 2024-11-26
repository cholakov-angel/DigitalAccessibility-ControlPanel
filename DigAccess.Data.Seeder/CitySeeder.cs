using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Feature;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Seeder
{
    public class CitySeeder : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(

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
            );
        } // Configure
    } // CitySeeder
}

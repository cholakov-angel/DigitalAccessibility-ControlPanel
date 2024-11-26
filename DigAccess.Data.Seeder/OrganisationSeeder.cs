using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Blind;
using DigAccess.Data.Entities.Enums;
using DigAccess.Data.Entities.Feature;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Data.Entities.Organisation.Organisation;
using DigAccess.Keys;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Seeder
{
    public class OrganisationSeeder : IEntityTypeConfiguration<OrganisationCompany>
    {
        public void Configure(EntityTypeBuilder<OrganisationCompany> builder)
        {
            builder.HasData(

                new OrganisationCompany() { Id = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef"), Name = "Съюз на слепите", National_Phone = "028127050" },
                new OrganisationCompany() { Id = Guid.Parse("db7b29f9-8974-463b-8937-a28e3dc8d90f"), Name = "Фондация Хоризонти", National_Phone = "24448858" }
            );
        } // Configure
    } // OrganisationSeeder
}

using DigAccess.Data.Entities.Feature;
using DigAccess.Data.Entities.Organisation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Seeder
{
    public class FeatureSeeder : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasData(
                new Feature()
                {
                    Id = Guid.Parse("564c9939-9c1d-44a5-bc8a-f4cb16bce86a"),
                    Name = "ChatGPT",
                    IsLicenceKeyRequired = true
                },
                new Feature()
                {
                    Id = Guid.Parse("8855d892-574a-434a-b347-b88c716608b6"),
                    Name = "Wikipedia",
                    IsLicenceKeyRequired = false
                }
                );
        } // Configure
    } // FeatureSeeder
}

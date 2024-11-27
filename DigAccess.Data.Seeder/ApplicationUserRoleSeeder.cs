using DigAccess.Data.Entities;
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
    public class ApplicationUserRoleSeeder : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "6f78d6a0-481a-4343-859f-f3eaa5d873df",
                    UserId = "14423824-2618-46fb-b9fb-38666f84d6e9"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "afcc821c-70c5-448a-a938-4f320fec7689",
                    UserId = "36f7ec79-9a12-4317-97ae-74b3476126d8"
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "e556c4eb-7791-4dd2-a69a-c40bd94d002e",
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
                });

        }
    }
}

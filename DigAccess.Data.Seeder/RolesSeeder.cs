using DigAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DigAccess.Data.Seeder
{
    public class RolesSeeder : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole()
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
            );
        }
    }
}

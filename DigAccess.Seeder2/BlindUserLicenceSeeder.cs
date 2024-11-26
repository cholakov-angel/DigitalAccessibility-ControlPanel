using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Blind;
using DigAccess.Data.Entities.Feature;
using DigAccess.Keys;
using DigAccess.Web.Data;
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
    public class BlindUserLicenceSeeder
    {
        private UserManager<ApplicationUser> userManager;
        private DigAccessDbContext context;
        public BlindUserLicenceSeeder(UserManager<ApplicationUser> userManager, DigAccessDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        } // BlindUserLicenceSeeder
        public async Task Configure()
        {
            await context.BlindUsersLicences.AddRangeAsync(
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
                    LicenceNumber = await MasterKey.GenerateMasterkey("КалинЦветановПетров", "9512099847", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                },
                new BlindUserLicence()
                {
                    Id = Guid.Parse("3b3ed71b-7b8a-4faf-9cc1-1b6705c03c1f"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LicenceNumber = await MasterKey.GenerateMasterkey("ЦветелинаАнгеловаПетрова", "9902199878", new Random()),
                    IsActivated = false,
                    DateFrom = new DateTime(2024, 5, 20),
                    MacAddress = null,
                    IsDeleted = false
                }
                );
            await context.SaveChangesAsync();
        }
    }
}

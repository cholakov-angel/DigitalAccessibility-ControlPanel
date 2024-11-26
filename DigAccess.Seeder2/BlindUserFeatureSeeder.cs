using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Feature;
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
    public class BlindUserFeatureSeeder { 
        private UserManager<ApplicationUser> userManager;
        private DigAccessDbContext context;
        public BlindUserFeatureSeeder(UserManager<ApplicationUser> userManager, DigAccessDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        } // BlindUserFeatureSeeder
        public async Task Configure()
        {
            await context.BlindUsersFeatures.AddAsync
                (
                    new BlindUserFeature()
                    {
                        BlindUserId = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                        FeatureId = Guid.Parse("8855d892-574a-434a-b347-b88c716608b6"),
                        LicenceKey=""
                    }
                );
            await context.SaveChangesAsync();
        } // Configure
    } // BlindUserFeatureSeeder
}

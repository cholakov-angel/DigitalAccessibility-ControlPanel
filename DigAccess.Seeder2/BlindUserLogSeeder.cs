using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Blind;
using DigAccess.Data.Entities.Feature;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigAccess.Data.Seeder
{
    public class BlindUserLogSeeder
    {
        private UserManager<ApplicationUser> userManager;
        private DigAccessDbContext context;
        public BlindUserLogSeeder(UserManager<ApplicationUser> userManager, DigAccessDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        } // BlindUserLogSeeder
        public async Task Configure()
        {
            await context.BlindUsersLogs.AddRangeAsync(
                new BlindUserLog()
                {
                    Id = Guid.Parse("7589b531-e7a1-4d77-b44f-0c4fbe62c4bc"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LogType = "Грешка",
                    LogCode = 100,
                    LogText = "Въведеният ключ за ChatGPT е невалиден!",
                    DateTimeOfLog = new DateTime(2023, 12, 1)
                },
                new BlindUserLog()
                {
                    Id = Guid.Parse("e7d6b236-fcd3-4333-8f57-32df271bb06f"),
                    BlindUserId = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                    LogType = "Грешка",
                    LogCode = 100,
                    LogText = "Въведеният ключ за ChatGPT е невалиден!",
                    DateTimeOfLog = new DateTime(2023, 10, 18)
                },
                new BlindUserLog()
                {
                    Id = Guid.Parse("fe7eb809-4416-462b-9708-3c4d68d5efcf"),
                    BlindUserId = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                    LogType = "Грешка",
                    LogCode = 100,
                    LogText = "Въведеният ключ за ChatGPT е невалиден!",
                    DateTimeOfLog = new DateTime(2024, 9, 14)
                }
                );
            await context.SaveChangesAsync();
        } // Configure
    } // BlindUserLogSeeder
}

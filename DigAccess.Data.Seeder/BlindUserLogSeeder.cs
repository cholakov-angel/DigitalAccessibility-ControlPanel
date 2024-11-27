using DigAccess.Data.Entities.Blind;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Seeder
{
    public class BlindUserLogSeeder : IEntityTypeConfiguration<BlindUserLog>
    {
        public void Configure(EntityTypeBuilder<BlindUserLog> builder)
        {
            builder.HasData(new BlindUserLog()
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
                });
        }
    }
}

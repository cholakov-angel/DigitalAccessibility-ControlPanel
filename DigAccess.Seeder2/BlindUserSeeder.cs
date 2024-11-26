using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Blind;
using DigAccess.Data.Entities.Enums;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;

namespace DigAccess.Data.Seeder
{
    public class BlindUserSeeder
    {
        private UserManager<ApplicationUser> userManager;
        private DigAccessDbContext context;
        public BlindUserSeeder(UserManager<ApplicationUser> userManager, DigAccessDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        } // BlindUserSeeder
        public async Task Configure()
        {
            await context.BlindUsers.AddRangeAsync(new List<BlindUser>() {
                 new BlindUser()
                 {
                     Id = Guid.Parse("2b143304-b5f0-4029-ba97-449f09e66649"),
                     FirstName = "Ангел",
                     MiddleName = "Борисов",
                     LastName = "Петров",
                     PersonalId = "0252199847",
                     TELKNumber = "0598415698",
                     Birthdate = PersonalIDParser.BirthdateExtract("0252199847"),
                     AdministratorId = userManager.Users.FirstOrDefault(x => x.PersonalId == "7912159865").Id,
                     CityId = Guid.Parse("cc974363-80a0-47c1-8433-039d4bf99fd0"),
                     StreetNumber = 250,
                     Street = "бул. Ломско шосе",
                     Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("0252199847"))
                 },
                 new BlindUser()
                 {
                     Id = Guid.Parse("4376ee53-2314-4d99-87b6-c08d9aecabcc"),
                     FirstName = "Калин",
                     MiddleName = "Цветанов",
                     LastName = "Иванов",
                     PersonalId = "9512099847",
                     TELKNumber = "0195415679",
                     Birthdate = PersonalIDParser.BirthdateExtract("9512099847"),
                     AdministratorId = userManager.Users.FirstOrDefault(x => x.PersonalId == "7912159865").Id,
                     CityId = Guid.Parse("6b827b51-65a3-4b0c-a59e-9f4d56951f82"),
                     StreetNumber = 19,
                     Street = "бул. Трети март",
                     Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("9512099847"))
                 },
                 new BlindUser()
                 {
                     Id = Guid.Parse("b22c5d21-5aa2-4a91-ae20-c43f16e7b6da"),
                     FirstName = "Цветелина",
                     MiddleName = "Ангелова",
                     LastName = "Петрова",
                     PersonalId = "9902199878",
                     TELKNumber = "0195415679",
                     Birthdate = PersonalIDParser.BirthdateExtract("9902199878"),
                     AdministratorId = userManager.Users.FirstOrDefault(x => x.PersonalId == "7912159865").Id,
                     CityId = Guid.Parse("9ac5590a-6946-471f-812f-503544b3fba7"),
                     StreetNumber = 190,
                     Street = "бул. България",
                     Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("9902199878"))
                 }

            });
            await context.SaveChangesAsync();

        } // Configure
    } // BlindUserSeeder
}

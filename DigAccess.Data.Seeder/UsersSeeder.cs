using DigAccess.Common;
using DigAccess.Data.Entities.Enums;
using DigAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DigAccess.Keys;

namespace DigAccess.Data.Seeder
{
    public static class UsersSeeder
    {
        public static async Task CreateUsers(UserManager<ApplicationUser> userManager)
        {
            // UserAdministrator
            ApplicationUser userAdmin1 = new ApplicationUser();
            userAdmin1.UserName = "petar.petrov1@gmail.com";
            userAdmin1.Email = "petar.petrov1@gmail.com";
            userAdmin1.ApprovalStatus = 1;
            userAdmin1.PhoneNumber = "0895648985";
            userAdmin1.FirstName = "Петър";
            userAdmin1.MiddleName = "Стефанов";
            userAdmin1.LastName = "Петров";
            userAdmin1.MasterKey = await MasterKey.GenerateMasterkey("ПетърСтефановПетров", "7912159865", new Random());
            userAdmin1.OfficeId = GuidParser.GuidParse("89729ab3-fe73-49af-9bcf-97cf00c49e2f");
            userAdmin1.OrganisationId = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef");
            userAdmin1.PersonalId = "7912159865";
            userAdmin1.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("7912159865"));

            var result = await userManager.CreateAsync(userAdmin1, "Petar1.Petrov1");
            if (result.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }
            IdentityResult roleresult = await userManager.AddToRoleAsync(userAdmin1, "UserAdministrator");

            if (roleresult.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }

            // OfficeWorker
            ApplicationUser officeWorker1 = new ApplicationUser();
            officeWorker1.UserName = "zoia.stefanova@worker.com";
            officeWorker1.Email = "zoia.stefanova@worker.com";
            officeWorker1.ApprovalStatus = 1;
            officeWorker1.PhoneNumber = "0895447985";
            officeWorker1.FirstName = "Зоя";
            officeWorker1.MiddleName = "Николова";
            officeWorker1.LastName = "Стефанова";

            officeWorker1.OfficeId = GuidParser.GuidParse("89729ab3-fe73-49af-9bcf-97cf00c49e2f");
            officeWorker1.OrganisationId = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef");
            officeWorker1.PersonalId = "7805059878";
            officeWorker1.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("7805059878"));

            result = await userManager.CreateAsync(officeWorker1, "Zoia1.Stefanova");
            if (result.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }
            roleresult = await userManager.AddToRoleAsync(officeWorker1, "OfficeWorker");

            if (roleresult.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }

            ApplicationUser officeWorker2 = new ApplicationUser();
            officeWorker2.UserName = "lucho.stoyanov@worker.com";
            officeWorker2.Email = "lucho.stoyanov@worker.com";
            officeWorker2.ApprovalStatus = 1;
            officeWorker2.PhoneNumber = "0896947986";
            officeWorker2.FirstName = "Лъчезар";
            officeWorker2.MiddleName = "Николова";
            officeWorker2.LastName = "Стоянов";

            officeWorker2.OfficeId = GuidParser.GuidParse("eba30287-4914-4a15-bb3c-2dd3d53c918f");
            officeWorker2.OrganisationId = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef");
            officeWorker2.PersonalId = "0345125989";
            officeWorker2.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("0345125989"));

            result = await userManager.CreateAsync(officeWorker2, "Lucho1.Stoyanov");
            if (result.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }
            roleresult = await userManager.AddToRoleAsync(officeWorker2, "OfficeWorker");

            if (roleresult.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }

            // OfficeAdministrator
            ApplicationUser officeAdmin1 = new ApplicationUser();
            officeAdmin1.UserName = "nikolay.nikolov@admin.com";
            officeAdmin1.Email = "nikolay.nikolov@admin.com";
            officeAdmin1.ApprovalStatus = 1;
            officeAdmin1.PhoneNumber = "0895447985";
            officeAdmin1.FirstName = "Николай";
            officeAdmin1.MiddleName = "Карашимов";
            officeAdmin1.LastName = "Николов";

            officeAdmin1.OfficeId = GuidParser.GuidParse("89729ab3-fe73-49af-9bcf-97cf00c49e2f");
            officeAdmin1.OrganisationId = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef");
            officeAdmin1.PersonalId = "9704059669";
            officeAdmin1.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("9704059669"));

            result = await userManager.CreateAsync(officeAdmin1, "Nikolay1.Nikolov");
            if (result.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }
            roleresult = await userManager.AddToRoleAsync(officeAdmin1, "OfficeAdministrator");

            if (roleresult.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }

            ApplicationUser officeAdmin2 = new ApplicationUser();
            officeAdmin2.UserName = "Angel.Borisov@admin.com";
            officeAdmin2.Email = "Angel.Borisov@admin.com";
            officeAdmin2.ApprovalStatus = 1;
            officeAdmin2.PhoneNumber = "0896947986";
            officeAdmin2.FirstName = "Ангел";
            officeAdmin2.MiddleName = "Николова";
            officeAdmin2.LastName = "Борисов";

            officeAdmin2.OfficeId = GuidParser.GuidParse("eba30287-4914-4a15-bb3c-2dd3d53c918f");
            officeAdmin2.OrganisationId = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef");
            officeAdmin2.PersonalId = "8808169869";
            officeAdmin2.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("8808169869"));

            result = await userManager.CreateAsync(officeAdmin2, "Angel1.Borisov");
            if (result.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }
            roleresult = await userManager.AddToRoleAsync(officeAdmin2, "OfficeAdministrator");

            if (roleresult.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }

            // OrgAdministrator
            ApplicationUser orgAdministrator1 = new ApplicationUser();
            orgAdministrator1.UserName = "blindunion@org.com";
            orgAdministrator1.Email = "blindunion@org.com";
            orgAdministrator1.ApprovalStatus = 1;
            orgAdministrator1.PhoneNumber = "0895446985";
            orgAdministrator1.FirstName = "Петър";
            orgAdministrator1.MiddleName = "Петров";
            orgAdministrator1.LastName = "Петров";

            orgAdministrator1.OrganisationId = Guid.Parse("c5e270c8-df31-40ec-938e-d1300457f3ef");
            orgAdministrator1.PersonalId = "7512059861";
            orgAdministrator1.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("7512059861"));

            result = await userManager.CreateAsync(orgAdministrator1, "Petar1.Petrov");
            if (result.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }
            roleresult = await userManager.AddToRoleAsync(orgAdministrator1, "OrgAdministrator");

            if (roleresult.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }

            ApplicationUser orgAdministrator2 = new ApplicationUser();
            orgAdministrator2.UserName = "horizonti@org.com";
            orgAdministrator2.Email = "horizonti@org.com";
            orgAdministrator2.ApprovalStatus = 1;
            orgAdministrator2.PhoneNumber = "0867457815";
            orgAdministrator2.FirstName = "Стефан";
            orgAdministrator2.MiddleName = "Стефанов";
            orgAdministrator2.LastName = "Стефанов";

            orgAdministrator2.OrganisationId = Guid.Parse("db7b29f9-8974-463b-8937-a28e3dc8d90f");
            orgAdministrator2.PersonalId = "0151169845";
            orgAdministrator2.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract("0151169845"));

            result = await userManager.CreateAsync(orgAdministrator2, "Stefan1.Stefanov");
            if (result.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }
            roleresult = await userManager.AddToRoleAsync(orgAdministrator2, "OrgAdministrator");

            if (roleresult.Succeeded == false)
            {
                throw new ArgumentException("Error occurred!");
            }
        } // CreateUsers
    } // UsersSeeder
}

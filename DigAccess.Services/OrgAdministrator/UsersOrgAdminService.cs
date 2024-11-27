using DigAccess.Common;
using DigAccess.Data.Entities.Enums;
using DigAccess.Data.Entities;
using DigAccess.Models.OrgAdministrator;
using Microsoft.AspNetCore.Identity;
using System.Data;
using DigAccess.Web.Data;
using Microsoft.EntityFrameworkCore;
using DigAccess.Services.Interfaces;
using System.Globalization;

namespace DigAccess.Services.OrgAdministrator
{
    public class UsersOrgAdminService : BaseService, IUsersOrgAdminService
    {
        private string role = "OrgAdministrator";
        public UsersOrgAdminService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // UsersOrgAdminService

        public async Task<bool> AddUserAsAdmin(string userId, string officeUserId)
        {
            var userAdmin = await this.GetOfficeWorker(userId, role);
            var officeUser = await this.GetOfficeWorker(officeUserId, "OfficeWorker");

            bool doesOfficeHasAnAdmin = await this.DoesOfficeHasAnAdmin(officeUser.OfficeId.ToString());

            if (doesOfficeHasAnAdmin == true)
            {
                throw new Exception("Office has an aministrator!");
            }

            await this.userManager.RemoveFromRoleAsync(officeUser, "OfficeWorker");
            await this.userManager.AddToRoleAsync(officeUser, "OfficeAdministrator");
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveOfficeAdmin(string userId, string workerId)
        {
            var userAdmin = await this.GetOfficeWorker(userId, role);
            var officeUser = await this.GetOfficeWorker(workerId, "OfficeAdministrator");

            await this.userManager.RemoveFromRoleAsync(officeUser, "OfficeAdministrator");
            return true;
        }

        public async Task<bool> AddOfficeAdmin(string userId, AddOfficeAdminViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentException("Invalid model!");
            }
            var userAdmin = await this.GetOfficeWorker(userId, role);

            bool doesOfficeHasAnAdmin = await this.DoesOfficeHasAnAdmin(model.OfficeId);

            if (doesOfficeHasAnAdmin == true)
            {
                throw new Exception("Office has an aministrator!");
            }

            var user = await GetOfficeWorker(userId, role);

            ApplicationUser officeWorker = new ApplicationUser();
            officeWorker.UserName = model.Email;
            officeWorker.Email = model.Email;
            officeWorker.ApprovalStatus = 1;
            officeWorker.PhoneNumber = model.PhoneNumber;
            officeWorker.FirstName = model.FirstName;
            officeWorker.MiddleName = model.MiddleName;
            officeWorker.LastName = model.LastName;

            officeWorker.OfficeId = GuidParser.GuidParse(model.OfficeId);
            officeWorker.OrganisationId = this.context.Offices.FirstOrDefault(x => x.Id == GuidParser.GuidParse(model.OfficeId)).OrganisationId;
            officeWorker.PersonalId = model.PersonalID;
            officeWorker.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract(model.PersonalID));

            var result = await userManager.CreateAsync(officeWorker, model.Password);
            if (result.Succeeded == false)
            {
                return false;
            }
            IdentityResult roleresult = await userManager.AddToRoleAsync(officeWorker, "OfficeAdministrator");

            if (roleresult.Succeeded == false)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DoesOfficeHasAnAdmin(string officeId)
        {
            var allWorkers = await this.userManager.Users.Where(x => x.OfficeId == GuidParser.GuidParse(officeId)).ToListAsync();

            foreach (var worker in allWorkers)
            {
                if (await this.userManager.IsInRoleAsync(worker, "OfficeAdministrator"))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<AddOfficeAdminViewModel> AddOfficeAdmin(string userId, string officeId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            bool doesOfficeHasAnAdmin = await this.DoesOfficeHasAnAdmin(officeId);

            if (doesOfficeHasAnAdmin == true)
            {
                throw new Exception("Office has an aministrator!");
            }

            AddOfficeAdminViewModel model = new AddOfficeAdminViewModel();
            model.OfficeId = officeId;

            return model;
        }

        public async Task<bool> RemoveUserAdmin(string userId, string officeAdminId)
        {
            var user = await this.GetOfficeWorker(userId, role);
            var officeAdmin = await this.GetOfficeWorker(officeAdminId, "OfficeAdministrator");

            await this.userManager.RemoveFromRoleAsync(officeAdmin, "OfficeAdministrator");
            await this.userManager.AddToRoleAsync(officeAdmin, "OfficeWorker");
            await this.context.SaveChangesAsync();

            return true;
        }
    } // UsersOrgAdminService
}

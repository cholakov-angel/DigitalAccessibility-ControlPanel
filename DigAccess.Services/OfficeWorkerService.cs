using DigAccess.Data.Entities;
using DigAccess.Models.OfficeWorker;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services
{
    public class OfficeWorkerService : BaseService, IOfficeWorkerService
    {
        public OfficeWorkerService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // OfficeWorkerService

        public async Task<bool> ApproveUser(string workerId, string userId)
        {
            ApplicationUser? officeWorker = await this.GetOfficeWorker(workerId);
            ApplicationUser? userToBeApproved = await this.GetUserToBeApproved(userId);

            if (userToBeApproved.OfficeId != officeWorker.OfficeId)
            {
                return false;
            }

            userToBeApproved.ApprovalStatus = 1;
            await userManager.AddToRoleAsync(userToBeApproved, "UserAdministrator");
            await userManager.RemoveFromRoleAsync(userToBeApproved, "WaitingApproval");

            return true;
        } // ApproveUser

        public async Task<UserDetailsViewModel> GetUserDetails(string workerId, string userId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId);
            var userToBeApproved = await this.GetUserToBeApproved(userId);


            if (userToBeApproved.OfficeId != officeWorker.OfficeId)
            {
                throw new ArgumentException("Invalid user!");
            }

            UserDetailsViewModel model = new UserDetailsViewModel();
            model.Id = userToBeApproved.Id;
            model.FirstName = userToBeApproved.FirstName;
            model.MiddleName = userToBeApproved.MiddleName;
            model.LastName = userToBeApproved.LastName;
            model.PersonalId = userToBeApproved.PersonalId;
            model.Gender = userToBeApproved.Gender.ToString();

            return model;
        } // GetUserDetails

        public async Task<WaitingUsersViewModel> GetWaitingUsers(string id)
        {
            var officeWorker = await this.GetOfficeWorker(id);

            var result = await userManager.Users.Where(x => x.OfficeId == officeWorker.OfficeId && x.ApprovalStatus == 0)
                .Select(x => new UserWaiting
                {
                    Id = x.Id,
                    OfficeId = x.OfficeId.ToString(),
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName
                }).ToListAsync();

            
            WaitingUsersViewModel model = new WaitingUsersViewModel();
            model.OfficeId = officeWorker.OfficeId.ToString();
            model.UserId = officeWorker.Id;
            model.UsersWaitings = result;

            return model;
        } // GetWaitingUsers

        public async Task<bool> RejectUser(string workerId, string userId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId);
            var userToBeApproved = await this.GetUserToBeApproved(userId);

            if (userToBeApproved.OfficeId != officeWorker.OfficeId)
            {
                return false;
            }

            userToBeApproved.ApprovalStatus = 2;
            await this.context.SaveChangesAsync();
            return true;
        } // RejectUser

        private async Task<ApplicationUser?> GetOfficeWorker(string workerId)
        {
            var officeWorker = await userManager.FindByIdAsync(workerId);

            if (officeWorker == null)
            {
                throw new ArgumentException("Invalid user!");
            }

            if (await userManager.IsInRoleAsync(officeWorker, "OfficeWorker") == false)
            {
                throw new ArgumentException("Invalid user!");
            }
            return officeWorker;
        } // GetOfficeWorker

        private async Task<ApplicationUser?> GetUserToBeApproved(string userId)
        {
            var userToBeApproved = await userManager.FindByIdAsync(userId);

            if (userToBeApproved == null)
            {
                throw new ArgumentException("Invalid user!");
            }

            return userToBeApproved;
        } // GetUserToBeApproved
    } // OfficeWorkerService
}

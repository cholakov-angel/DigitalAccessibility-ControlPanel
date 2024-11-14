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
            ApplicationUser? userToBeApproved = await this.GetUser(userId);

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
            var user = await this.GetUser(userId);


            if (user.OfficeId != officeWorker.OfficeId)
            {
                throw new ArgumentException("Invalid user!");
            }

            UserDetailsViewModel model = new UserDetailsViewModel();
            model.Id = user.Id;
            model.FirstName = user.FirstName;
            model.MiddleName = user.MiddleName;
            model.LastName = user.LastName;
            model.PersonalId = user.PersonalId;
            model.Gender = user.Gender.ToString();

            return model;
        } // GetUserDetails

        public async Task<bool> DeleteUser(string workerId, string userId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId);
            var user = await this.GetUser(userId);

            if (user.OfficeId != officeWorker.OfficeId)
            {
                return false;
            }

            user.ApprovalStatus = 3;
            await userManager.SetLockoutEnabledAsync(user, true);
            await userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(100));

            await context.SaveChangesAsync();

            return true;
        } // DeleteUser

        public async Task<List<UserDetailsViewModel>> GetUsers(string userId, int page)
        {
            var officeWorker = await this.GetOfficeWorker(userId);

            return await this.userManager.Users.Where(x => x.OfficeId == officeWorker.OfficeId && x.ApprovalStatus == 1)
                .Select(x => new UserDetailsViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    PersonalId = x.PersonalId,
                    Gender = x.Gender.ToString()
                })
                .Skip((page - 1) * 8)
                .Take(8)
                .ToListAsync();
        } // GetUsers

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
            var userToBeApproved = await this.GetUser(userId);

            if (userToBeApproved.OfficeId != officeWorker.OfficeId)
            {
                return false;
            }

            userToBeApproved.ApprovalStatus = 2;
            await this.context.SaveChangesAsync();
            return true;
        } // RejectUser

        private async Task<ApplicationUser?> GetUser(string userId)
        {
            var userToBeApproved = await userManager.FindByIdAsync(userId);

            if (userToBeApproved == null)
            {
                throw new ArgumentException("Invalid user!");
            }

            return userToBeApproved;
        } // GetUserToBeApproved

        public async Task<UserDeleteViewModel> ApproveUserForDelete(string workerId, string userId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId);
            var userToBeApproved = await this.GetUser(userId);

            if (userToBeApproved.OfficeId != officeWorker.OfficeId)
            {
                throw new ArgumentException("Invalid user!");
            }

            return new UserDeleteViewModel { Id = userId };
        } // ApproveUserForDelete

        public async Task<List<UserDetailsViewModel>> GetUsersByName(string userId, string name, int page)
        {
            if (name == null)
            {
                return await this.GetUsers(userId, 1);
            }
            var officeWorker = await this.GetOfficeWorker(userId);

            return await this.userManager.Users.Where(x => x.OfficeId == officeWorker.OfficeId && x.ApprovalStatus == 1
            && (x.FirstName + " " + x.MiddleName + " " + x.LastName).ToLower().StartsWith(name.ToLower()))
                .Select(x => new UserDetailsViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    PersonalId = x.PersonalId,
                    Gender = x.Gender.ToString()
                })
                .Skip((page - 1) * 8)
                .Take(8)
                .ToListAsync();
        } // GetUsersByName

        public async Task<int> CountUsers(string workerId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId);

            return await this.userManager.Users.Where(x => x.OfficeId == officeWorker.OfficeId && x.ApprovalStatus == 1)
                .CountAsync();

        } // CountUsers
        private async Task<ApplicationUser?> GetOfficeWorker(string workerId)
        {
            if (workerId == null)
            {
                throw new ArgumentException("Invalid user!");
            }

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
    } // OfficeWorkerService
}

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
        private const string role = "OfficeWorker";
        public OfficeWorkerService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // OfficeWorkerService

        public async Task<bool> ApproveUser(string workerId, string userId)
        {
            ApplicationUser? officeWorker = await this.GetOfficeWorker(workerId, role);
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
            var officeWorker = await this.GetOfficeWorker(workerId, role);
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
            model.BlindUsers = await context.BlindUsers.Where(x => x.AdministratorId == user.Id)
                .Select(x => new BlindUserViewModel()
                {
                    Id = x.Id.ToString(),
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName
                }).ToListAsync();
            return model;
        } // GetUserDetails

        public async Task<bool> DeleteUser(string workerId, string userId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId, role);
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
            var officeWorker = await this.GetOfficeWorker(userId, role);

            var users = await this.userManager.Users.Where(x => x.OfficeId == officeWorker.OfficeId && x.ApprovalStatus == 1)
                .ToListAsync();


            List<UserDetailsViewModel> model = new List<UserDetailsViewModel>();
            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, "UserAdministrator"))
                {
                    model.Add(new UserDetailsViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        PersonalId = user.PersonalId,
                        Gender = user.Gender.ToString()
                    });
                }
            }

            return model.Skip((page - 1) * 8).Take(8).ToList();
        } // GetUsers

        public async Task<WaitingUsersViewModel> GetWaitingUsersByName(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return await this.GetWaitingUsers(id);
            }

            var officeWorker = await this.GetOfficeWorker(id, role);

            var result = await userManager.Users.Where(x => x.OfficeId == officeWorker.OfficeId && x.ApprovalStatus == 0
            && (x.FirstName + " " + x.MiddleName + " " + x.LastName).ToLower().StartsWith(name.ToLower()))
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
        } // GetWaitingUsersByName

        public async Task<WaitingUsersViewModel> GetWaitingUsers(string id)
        {
            var officeWorker = await this.GetOfficeWorker(id, role);

            var users = await userManager.Users.Where(x => x.OfficeId == officeWorker.OfficeId && x.ApprovalStatus == 0)
                .ToListAsync();

            List<UserWaiting> waititngUsers = new List<UserWaiting>();
            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, "WaitingApproval"))
                {
                    waititngUsers.Add(new UserWaiting()
                    {

                        Id = user.Id,
                        OfficeId = user.OfficeId.ToString(),
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName
                    });
                }
            }

            WaitingUsersViewModel model = new WaitingUsersViewModel();
            model.OfficeId = officeWorker.OfficeId.ToString();
            model.UserId = officeWorker.Id;
            model.UsersWaitings = waititngUsers;

            return model;
        } // GetWaitingUsers

        public async Task<bool> RejectUser(string workerId, string userId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId, role);
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

            if (userToBeApproved == null || await userManager.IsInRoleAsync(userToBeApproved, "OrgAdministrator")
                || await userManager.IsInRoleAsync(userToBeApproved, "OfficeAdministrator"))
            {
                throw new ArgumentException("Invalid user!");
            }

            return userToBeApproved;
        } // GetUserToBeApproved

        public async Task<UserDeleteViewModel> ApproveUserForDelete(string workerId, string userId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId, role);
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
            var officeWorker = await this.GetOfficeWorker(userId, role);

            var users = await this.userManager.Users.Where(x => x.OfficeId == officeWorker.OfficeId && x.ApprovalStatus == 1
            && (x.FirstName + " " + x.MiddleName + " " + x.LastName).ToLower().StartsWith(name.ToLower()))
                .ToListAsync();

            List<UserDetailsViewModel> model = new List<UserDetailsViewModel>();
            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, "UserAdministrator"))
                {
                    model.Add(new UserDetailsViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        PersonalId = user.PersonalId,
                        Gender = user.Gender.ToString()
                    });
                }
            }

            return model;
        } // GetUsersByName

        public async Task<int> CountUsers(string workerId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId, role);

            var users = await this.userManager.Users.Where(x => x.OfficeId == officeWorker.OfficeId && x.ApprovalStatus == 1)
                .ToListAsync();

            int count = 0;
            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, "UserAdministrator"))
                {
                    count++;
                }
            }

            return count;
        } // CountUsers
    } // OfficeWorkerService
}

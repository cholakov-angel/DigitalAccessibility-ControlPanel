using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Enums;
using DigAccess.Models.OfficeAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services.OfficeAdministrator
{
    public class WorkerOfficeAdminService : BaseService, IWorkerOfficeAdminService
    {
        private const string role = "OfficeAdministrator";
        public WorkerOfficeAdminService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {

        } // WorkerOfficeAdminService

        public async Task<List<WorkerViewModel>> GetWorkers(string id, int page)
        {
            var user = await this.GetOfficeWorker(id, role);

            var users = await userManager.Users.Where(x => x.OfficeId == user.OfficeId && x.Id != user.Id)
                .ToListAsync();

            List<WorkerViewModel> workers = new List<WorkerViewModel>();
            foreach (var u in users)
            {
                if (await userManager.IsInRoleAsync(u, "OfficeWorker") == true)
                {
                    workers.Add(new WorkerViewModel
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName
                    });
                }
            }

            return workers.Skip((page - 1) * 8).Take(8).ToList();
        } // GetWorkers

        public async Task<int> CountUsers(string workerId)
        {
            var user = await GetOfficeWorker(workerId, role);
            var users = await userManager.Users.Where(x => x.OfficeId == user.OfficeId && x.Id != user.Id).ToListAsync();

            List<WorkerViewModel> workers = new List<WorkerViewModel>();
            foreach (var u in users)
            {
                if (await userManager.IsInRoleAsync(u, "OfficeWorker") == true)
                {
                    workers.Add(new WorkerViewModel
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName
                    });
                }
            }
            return workers.Count;
        } // CountUsers

        public async Task<WorkerDetailsViewModel> WorkerDetails(string userId, string workerId)
        {
            var user = await GetOfficeWorker(userId, role);
            var worker = await GetOfficeWorker(workerId, "OfficeWorker");

            if (worker.OfficeId != user.OfficeId)
            {
                throw new ArgumentException("Invalid user!");
            }

            WorkerDetailsViewModel model = new WorkerDetailsViewModel();
            model.Id = worker.Id;
            model.FirstName = worker.FirstName;
            model.MiddleName = worker.MiddleName;
            model.LastName = worker.LastName;
            model.PersonalID = worker.PersonalId;
            model.Email = worker.Email;
            model.Phone = worker.PhoneNumber;

            return model;
        } // WorkerDetails

        public async Task<bool> AddOfficeWorker(string userId, AddWorkerViewModel model)
        {
            var user = await GetOfficeWorker(userId, role);

            ApplicationUser officeWorker = new ApplicationUser();
            officeWorker.UserName = model.Email;
            officeWorker.Email = model.Email;
            officeWorker.ApprovalStatus = 1;
            officeWorker.PhoneNumber = model.PhoneNumber;
            officeWorker.FirstName = model.FirstName;
            officeWorker.MiddleName = model.MiddleName;
            officeWorker.LastName = model.LastName;

            officeWorker.OfficeId = user.OfficeId;
            officeWorker.OrganisationId = this.context.Offices.FirstOrDefault(x => x.Id == user.OfficeId).OrganisationId;
            officeWorker.PersonalId = model.PersonalID;
            officeWorker.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract(model.PersonalID));

            var result = await userManager.CreateAsync(officeWorker, model.Password);
            if (result.Succeeded == false)
            {
                return false;
            }
            IdentityResult roleresult = await userManager.AddToRoleAsync(officeWorker, "OfficeWorker");

            if (roleresult.Succeeded == false)
            {
                return false;
            }
            return true;
        } // AddOfficeWorker

        public async Task<bool> DeleteUser(string workerId, string userId)
        {
            var officeWorker = await this.GetOfficeWorker(workerId, role);
            var user = await this.GetOfficeWorker(userId, "OfficeWorker");

            if (user.OfficeId != officeWorker.OfficeId)
            {
                return false;
            }

            await userManager.DeleteAsync(user);

            await context.SaveChangesAsync();

            return true;
        } // DeleteUser

        public async Task<List<WorkerViewModel>> GetWorkersByName(string id, string name)
        {
            var user = await this.GetOfficeWorker(id, role);

            var users = await userManager.Users.Where(x => x.OfficeId == user.OfficeId && x.Id != user.Id &&
                            (x.FirstName + " " + x.MiddleName + " " + x.LastName).ToLower().StartsWith(name.ToLower()))
                            .ToListAsync();

            List<WorkerViewModel> workers = new List<WorkerViewModel>();
            foreach (var u in users)
            {
                if (await userManager.IsInRoleAsync(u, "OfficeWorker") == true)
                {
                    workers.Add(new WorkerViewModel
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName
                    });
                }
            }
            return workers;
        } // GetWorkersByName
    } // WorkerOfficeAdminService
}

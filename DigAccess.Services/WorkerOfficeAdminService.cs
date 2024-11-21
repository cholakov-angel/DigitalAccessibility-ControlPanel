using Azure;
using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Enums;
using DigAccess.Models.OfficeAdministrator;
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
    public class WorkerOfficeAdminService : BaseService, IWorkerOfficeAdminService
    {
        private const string role = "OfficeAdministrator";
        public WorkerOfficeAdminService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {

        } // WorkerOfficeAdminService

        public async Task<List<WorkerViewModel>> GetWorkers(string id, int page)
        {
            var user = await this.GetOfficeWorker(id, role);

            var users = await this.userManager.Users.Where(x => x.OfficeId == user.OfficeId && x.Id != user.Id)
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
            var user = await this.GetOfficeWorker(workerId, role);
            var users = await this.userManager.Users.Where(x => x.OfficeId == user.OfficeId && x.Id != user.Id).ToListAsync();

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

        public async Task<bool> AddOfficeWorker(string userId, AddWorkerViewModel model)
        {
            var user = await this.GetOfficeWorker(userId, role);

            ApplicationUser officeWorker = new ApplicationUser();
            officeWorker.UserName = model.Email;
            officeWorker.Email = model.Email;
            officeWorker.ApprovalStatus = 1;
            officeWorker.PhoneNumber = model.PhoneNumber;
            officeWorker.FirstName = model.FirstName;
            officeWorker.MiddleName = model.MiddleName;
            officeWorker.LastName = model.LastName;

            officeWorker.OfficeId = user.OfficeId;
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
        }
    } // WorkerOfficeAdminService
}

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
    public class QuestionOfficeWorkerService : BaseService, IQuestionOfficeWorkerService
    {
        public QuestionOfficeWorkerService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // QuestionOfficeWorkerService

        public async Task<List<QuestionViewModel>> GetQuestions(string userId)
        {
            var user = await this.GetOfficeWorker(userId);

            var model = await this.context.Questions.Include(x => x.User)
                .Where(x => x.User.OfficeId == user.OfficeId)
                .Select(x => new QuestionViewModel 
                {
                    Id = x.Id.ToString(),
                    UserName = x.User.FirstName + " " + x.User.MiddleName + " " + x.User.LastName,
                    Title = x.Title,
                    UserId = x.UserId
                }).ToListAsync();

            return model;
        } // GetQuestions

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
    } // QuestionOfficeWorkerService
}

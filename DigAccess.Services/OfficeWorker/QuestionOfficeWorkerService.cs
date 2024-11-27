using DigAccess.Common;
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
        } // QuestionAnserOfficeWorkerService

        public async Task<List<QuestionViewModel>> GetQuestionsByName(string userId, string name)
        {
            var user = await this.GetOfficeWorker(userId);

            var model = await this.context.Questions.Include(x => x.User)
                .Where(x => x.User.OfficeId == user.OfficeId && x.IsAnswered == false && x.Title.ToLower().StartsWith(name))
                .Select(x => new QuestionViewModel
                {
                    Id = x.Id.ToString(),
                    UserName = x.User.FirstName + " " + x.User.MiddleName + " " + x.User.LastName,
                    Title = x.Title,
                    UserId = x.UserId
                }).ToListAsync();

            return model;
        } // GetQuestionsByName

        public async Task<List<QuestionViewModel>> GetQuestions(string userId, int page)
        {
            var user = await this.GetOfficeWorker(userId);

            var model = await this.context.Questions.Include(x => x.User)
                .Where(x => x.User.OfficeId == user.OfficeId && x.IsAnswered == false)
                .Select(x => new QuestionViewModel
                {
                    Id = x.Id.ToString(),
                    UserName = x.User.FirstName + " " + x.User.MiddleName + " " + x.User.LastName,
                    Title = x.Title,
                    UserId = x.UserId
                }).Skip((page - 1) * 8).Take(8).ToListAsync();

            return model;
        } // GetQuestions

        public async Task<QuestionDetailsViewModel> GetQuestion(string userId, string questionId)
        {
            var questionIdGuid = GuidParser.GuidParse(questionId);
            var user = await this.GetOfficeWorker(userId);

            var model = await this.context.Questions.Include(x => x.User)
                .Where(x => x.User.OfficeId == user.OfficeId && x.Id == questionIdGuid)
                .Select(x => new QuestionDetailsViewModel
                {
                    Id = x.Id.ToString(),
                    UserName = x.User.FirstName + " " + x.User.MiddleName + " " + x.User.LastName,
                    Title = x.Title,
                    UserId = x.UserId,
                    Description = x.Description,
                    Date = x.Date.ToString(Constants.DateTimeFormat)
                }).FirstOrDefaultAsync();

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

        public async Task<int> CountQuestions(string userId)
        {
            var user = await this.GetOfficeWorker(userId);

            return await this.context.Questions.Include(x => x.User)
                .Where(x => x.User.OfficeId == user.OfficeId && x.IsAnswered == false).CountAsync();
        }
    } // QuestionAnserOfficeWorkerService
}

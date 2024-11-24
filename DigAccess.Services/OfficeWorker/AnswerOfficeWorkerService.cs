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
    public class AnswerOfficeWorkerService : BaseService, IAnswerOfficeWorkerService
    {
        public AnswerOfficeWorkerService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // AnswerOfficeWorkerService

        public async Task<bool> AnswerQuestion(AnswerViewModel model, string userId)
        {
            var user = await this.GetOfficeWorker(userId);
            var questionIdGuid = GuidParser.GuidParse(model.QuestionId);

            var question = await this.context.Questions.FirstOrDefaultAsync(x => x.Id == questionIdGuid && x.IsAnswered == false);
            
            if (question == null)
            {
                return false;
            }

            Answer answer = new Answer();
            answer.QuestionId = questionIdGuid;
            answer.Title = model.Title;
            answer.Description = model.Description;
            answer.Date = model.Date;

            question.IsAnswered = true;

            await context.Answers.AddAsync(answer);
            await context.SaveChangesAsync();

            return true;
        } // AnswerQuestion

        public async Task<AnswerViewModel> CreateAnswer(string userId, string questionId, DateTime date)
        {
            var questionIdGuid = GuidParser.GuidParse(questionId);
            var user = await this.GetOfficeWorker(userId);

            bool isQuestionValid = await this.context.Questions.AnyAsync(x => x.Id == questionIdGuid && x.IsAnswered == false);
        
            if (isQuestionValid == false)
            {
                throw new ArgumentException("Invalid question id!");
            }

            AnswerViewModel answerViewModel = new AnswerViewModel
            {
                QuestionId = questionId,
                Date = date
            };

            return answerViewModel;
        } // CreateAnswer

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
    } // AnswerOfficeWorkerService
}

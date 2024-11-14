using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Models.UserAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services
{
    public class QuestionService : BaseService, IQuestionService
    {
        public QuestionService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // QuestionService

        public async Task<bool> AskQuestion(QuestionViewModel questionViewModel, string userId, DateTime date)
        {
            if (userId == null)
            {
                return false;
            }
            Question question = new Question();
            question.Title = questionViewModel.Title;
            question.Description = questionViewModel.Description;
            question.Date = date;
            question.UserId = userId;

            context.Questions.Add(question);
            await context.SaveChangesAsync();
            return true;
        } // AskQuestion
    } // QuestionService
}

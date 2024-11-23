using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Models.UserAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AnswerViewModel = DigAccess.Models.UserAdministrator.AnswerViewModel;

namespace DigAccess.Services
{
    public class AnswerUserAdministratorService : BaseService, IAnswerUserAdministratorService
    {
        public AnswerUserAdministratorService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // IAnswerUserAdministratorService

        public async Task<List<AnswerViewModel>> GetAnswers(string userId, int page)
        {
            return await this.context.Answers.Include(x=> x.Question)
                .Where(x=> x.Question.UserId == userId && x.IsReviewed == false)
                .Select(x=> new AnswerViewModel()
                {
                    Id = x.Id.ToString(),
                    DateTime = x.Date.ToString(Constants.DateTimeFormat),
                    Title = x.Title
                })
                .Skip((page - 1) * 8).Take(8)
                .ToListAsync();
        } // GetAnswers

        public async Task<bool> DeleteAnswer(string userId, string answerId)
        {
            Guid answerIdGuid = GuidParser.GuidParse(answerId);

            var answer = await this.context.Answers.Include(x => x.Question)
                .FirstOrDefaultAsync(x => x.Question.UserId == userId && x.Id == answerIdGuid);

            if (answer == null)
            {
                return false;
            }

            answer.IsReviewed = true;
            await this.context.SaveChangesAsync();
            return true;
        }
        public async Task<AnswerDetailsViewModel> GetAnswer(string userId, string answerId)
        {
            Guid answerIdGuid = GuidParser.GuidParse(answerId);
            var answer = await this.context.Answers.Include(x => x.Question)
                .Where(x => x.Question.UserId == userId && x.Id == answerIdGuid)
                .Select(x => new AnswerDetailsViewModel()
                {
                    Id = x.Id.ToString(),
                    QuestionDate = x.Question.Date.ToString(Constants.DateTimeFormat),
                    QuestionTitle = x.Question.Title,
                    QuestionDescription = x.Question.Description,
                    Title = x.Title,
                    Description = x.Description,
                    Date = x.Date.ToString(Constants.DateTimeFormat)
                }).FirstOrDefaultAsync();

            return answer;
        } // GetAnswer

        public async Task<int> CountAnswers(string userId)
        {
            return await this.context.Answers.Include(x => x.Question)
                .Where(x => x.Question.UserId == userId && x.IsReviewed == false)
                .CountAsync();
        } // CountAnswers
    } // IAnswerUserAdministratorService
}

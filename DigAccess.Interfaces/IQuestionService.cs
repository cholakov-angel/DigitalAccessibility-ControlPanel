using DigAccess.Interfaces;
using DigAccess.Models.UserAdministrator;

namespace DigAccess.Services.Interfaces
{
    public interface IQuestionService : IService
    {
        public Task<bool> AskQuestion(QuestionViewModel questionViewModel, string userId, DateTime date);
    }
}

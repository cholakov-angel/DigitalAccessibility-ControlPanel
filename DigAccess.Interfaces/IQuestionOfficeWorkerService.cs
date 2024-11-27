using DigAccess.Interfaces;
using DigAccess.Models.OfficeWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IQuestionOfficeWorkerService : IService
    {
        public Task<List<QuestionViewModel>> GetQuestions(string userId, int page);
        public Task<QuestionDetailsViewModel> GetQuestion(string userId, string questionId);
        public Task<List<QuestionViewModel>> GetQuestionsByName(string userId, string name);
        public Task<int> CountQuestions(string userId);

    }
}

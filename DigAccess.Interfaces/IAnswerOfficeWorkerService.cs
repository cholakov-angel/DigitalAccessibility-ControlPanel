using DigAccess.Interfaces;
using DigAccess.Models.OfficeWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IAnswerOfficeWorkerService : IService
    {
        public Task<AnswerViewModel> CreateAnswer(string userId, string questionId, DateTime date);

        public Task<bool> AnswerQuestion(AnswerViewModel model, string userId);
    }
}

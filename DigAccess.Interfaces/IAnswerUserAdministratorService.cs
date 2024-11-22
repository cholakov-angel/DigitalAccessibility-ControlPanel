using DigAccess.Interfaces;
using DigAccess.Models.UserAdministrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IAnswerUserAdministratorService : IService
    {
        public Task<List<AnswerViewModel>> GetAnswers(string userId, int page);
        public Task<AnswerDetailsViewModel> GetAnswer(string userId, string answerId);
        public Task<int> CountAnswers(string userId);

    } // IAnswerUserAdministratorService
}

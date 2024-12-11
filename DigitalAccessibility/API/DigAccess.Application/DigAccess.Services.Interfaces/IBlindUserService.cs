using DigAccess.ViewModels;

namespace DigAccess.Services.Interfaces
{
    public interface IBlindUserService : IService
    {
        public Task<EmailViewModel> GetEmail(BlindUserViewModel model);

        public Task<bool> AddLog(LogViewModel model);
    } // IBlindUserService
}

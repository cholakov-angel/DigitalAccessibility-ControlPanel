using DigAccess.ViewModels;

namespace DigAccess.Services.Interfaces
{
    public interface IBlindUserService : IService
    {
        public Task<EmailViewModel> GetEmail(string blindUserId, string adminId);

        public Task<bool> AddLog(LogViewModel model);
    } // IBlindUserService
}

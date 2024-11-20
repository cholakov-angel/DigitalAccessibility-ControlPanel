using DigAccess.ViewModels;

namespace DigAccess.Services.Interfaces
{
    public interface ILicenceService : IService
    {
        public Task<bool> IsLicenseActive(string licence, string masterKey);
        public Task<BlindUserViewModel> GetUserFromLincense(string licence, string masterKey);
    } // ILicenceService
}

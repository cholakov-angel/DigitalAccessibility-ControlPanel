using DigAccess.Models.UserAdministrator.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DigAccess.Interfaces
{
    public interface ILicenseService : IService
    {
        public Task<LicenseDetailsViewModel> GetLicense(string userId, string licenceId);
        public Task<UserLicenseViewModel> GetLicenses(string blindUserId, string userId);
        public Task<LicenseDeleteViewModel> DeleteLicenseConfirm(string userId, string licenceId);

        public Task<LicenseKeyViewModel> GenerateLicense(string blindUserId, string userId, Random random, DateTime dateFrom);
        public Task<string> DeleteLicense(string blindUserId, string userId);
    } // ILicenceService
}

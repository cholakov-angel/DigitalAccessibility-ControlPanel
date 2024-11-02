using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Models.Licence;

namespace DigAccess.Interfaces
{
    public interface ILicenceService : IService
    {
        public Task<LicenceDetailsViewModel> GetLicence(string userId, string licenceId);
        public Task<UserLicenceViewModel> GetLicences(string blindUserId, string userId);
        public Task<LicenceDeleteViewModel> DeleteLicenceConfirm(string userId, string licenceId);

        public Task<LicenceKeyViewModel> GenerateLicence(string blindUserId, string userId, Random random, DateTime dateFrom);
        public Task<string> DeleteLicence(string blindUserId, string userId);
    } // ILicenceService
}

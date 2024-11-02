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
        public Task<List<LicenceAllViewModel>> GetAll(string userId);
        public Task<UserLicenceViewModel> GetLicences(string blindUserId, string userId);
        public Task GenerateLicence(string blindUserId, string userId, Random random, DateTime dateFrom);
    } // ILicenceService
}

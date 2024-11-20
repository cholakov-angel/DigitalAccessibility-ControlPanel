using DigAccess.Application;
using DigAccess.Services.Interfaces;
using DigAccess.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services
{
    public class LicenceService : BaseService, ILicenceService
    {
        public LicenceService(DigAccessAdminPanelContext context) : base(context)
        {
        } // LicenceService

        public async Task<bool> IsLicenseActive(string licence, string masterKey)
        {
            var licenceObj = await this.context.BlindUsersLicences.Include(x => x.BlindUser)
                .ThenInclude(x => x.Administrator)
                .FirstOrDefaultAsync(x => x.LicenceNumber == licence && x.BlindUser.Administrator.MasterKey == masterKey);

            if (licenceObj == null)
            {
                return false;
            }

            return true;
        } // IsLicenseActive

        public async Task<BlindUserViewModel> GetUserFromLincense(string licence, string masterKey)
        {
            var licenceObj = await this.context.BlindUsersLicences.Include(x => x.BlindUser)
                .ThenInclude(x => x.Administrator)
                .FirstOrDefaultAsync(x => x.LicenceNumber == licence && x.BlindUser.Administrator.MasterKey == masterKey);

            if (licenceObj == null)
            {
                throw new ArgumentException("Invalid id");
            }

            var user = await this.context.BlindUsers.FirstOrDefaultAsync(x => x.Id == licenceObj.BlindUserId);

            if (user == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            BlindUserViewModel model = new BlindUserViewModel();
            model.Id = user.Id.ToString();
            model.AdministratorId = user.AdministratorId.ToString();

            return model;
        } // GetUserFromLincense
    } // LicenceService
}

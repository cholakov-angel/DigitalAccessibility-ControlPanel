using DigAccess.Application;
using DigAccess.Services.Interfaces;
using DigAccess.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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

        public async Task<BlindUserViewModel> ActivateLicense(LicenseActivateViewModel model)
        {
            var licenceObj = await this.context.BlindUsersLicences.Include(x => x.BlindUser)
                .ThenInclude(x => x.Administrator)
                .FirstOrDefaultAsync(x => x.LicenceNumber == model.LicenseKey && x.BlindUser.Administrator.MasterKey == model.MasterKey && x.IsDeleted == false);

            if (licenceObj == null)
            {
                throw new ArgumentException("Invalid id");
            }

            var user = await this.context.BlindUsers.FirstOrDefaultAsync(x => x.Id == licenceObj.BlindUserId);

            if (user == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            licenceObj.DateOfActivation = DateTime.Parse(model.DateTime);
            licenceObj.IsActivated = true;
            licenceObj.MacAddress = model.MACAddress;

            await this.context.SaveChangesAsync();

            BlindUserViewModel model1 = new BlindUserViewModel();
            model1.Id = user.Id.ToString();
            model1.AdministratorId = user.AdministratorId.ToString();

            return model1;
        }
        public async Task<BlindUserViewModel> GetUserFromLincense(string licence, string masterKey)
        {
            var licenceObj = await this.context.BlindUsersLicences.Include(x => x.BlindUser)
                .ThenInclude(x => x.Administrator)
                .FirstOrDefaultAsync(x => x.LicenceNumber == licence && x.BlindUser.Administrator.MasterKey == masterKey && x.IsDeleted == false);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Blind;
using DigAccess.Interfaces;
using DigAccess.Keys;
using DigAccess.Models.UserAdministrator.License;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services
{
    public class LicenceService : BaseService, ILicenseService
    {
        public LicenceService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // LicenceService

        public async Task<LicenseDetailsViewModel> GetLicense(string userId, string licenceId)
        {
            // Преобразуване на лицензния идентификатор в Guid
            var licenceIdGuid = GuidParser.GuidParse(licenceId);

            // Проверка дали съществува незрящ потребител с въведения идентикатор, който да е с администратор потребителя,
            // влязъл в системата
            bool isBlindUserValid = await context.BlindUsers.AnyAsync(x =>
                                    x.BlindUserLicences.Any(x => x.Id == licenceIdGuid) && x.AdministratorId == userId);

            if (!isBlindUserValid)
            {
                throw new ArgumentException("Invalid user or administrator!");
            }
            // Взимане на лиценза от базата данни
            var licence = await context.BlindUsersLicences.FirstOrDefaultAsync(x => x.Id == licenceIdGuid);

            if (licence == null)
            {
                throw new ArgumentException("Invalid licence id!");
            }

            var blindUser = await context.BlindUsers.FirstOrDefaultAsync(x => x.Id == licence.BlindUserId);
            LicenseDetailsViewModel licenceModel = new LicenseDetailsViewModel();

            licenceModel.Id = licenceId;
            licenceModel.DateFrom = licence.DateFrom.ToString(Constants.DateTimeFormat);
            licenceModel.MACAddress = licence.MacAddress;
            if (licence.IsActivated)
            {
                licenceModel.IsActive = "Активиран";
            }
            else
            {
                licenceModel.IsActive = "Неактивиран";

            }
            licenceModel.DateOfActivation = licence.DateOfActivation.ToString(Constants.DateTimeFormat);
            licenceModel.BlindUser = new LicenseDetailsBlindUserViewModel()
            {
                Id = blindUser.Id.ToString(),
                FirstName = blindUser.FirstName,
                MiddleName = blindUser.MiddleName,
                LastName = blindUser.LastName
            };
            return licenceModel;
        } // GetLicence

        public async Task<UserLicenseViewModel> GetLicenses(string blindUserId, string userId)
        {
            // Преобразуване на идентификатора в Guid
            var id = GuidParser.GuidParse(blindUserId);

            var blindUser = await context.BlindUsers.FindAsync(id);

            if (blindUser == null)
            {
                throw new Exception("Invalid user!");
            }
            // Проверка дали администратора на незрящото лице е потребителя, влезнал в системата
            if (blindUser.AdministratorId != userId)
            {
                throw new Exception("Invalid administrator!");
            }

            var licences = context.BlindUsers.Where(x => x.Id == id)
                .Select(x => new UserLicenseViewModel()
                {
                    UserId = x.Id.ToString(),
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Licenses = x.BlindUserLicences.Where(x => x.IsDeleted == false)
                        .Select(y => new LicenseViewModel()
                        {
                            DateFrom = y.DateFrom.ToString(Constants.DateTimeFormat),
                            Id = y.Id.ToString(),
                            IsActivated = y.IsActivated == true ? "Активиран" : "Неактивиран",
                        }).ToList()
                }).FirstOrDefault();

            return licences;
        } // GetLicences

        public async Task<LicenseKeyViewModel> GenerateLicense(string blindUserId, string userId, Random random, DateTime dateFrom)
        {
            // Преобразуване на идентификатора в Guid
            var id = GuidParser.GuidParse(blindUserId);

            var user = await context.BlindUsers.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("Invalid user!");
            }

            // Проверка дали администратора на незрящото лице е влезналият в системата потребител
            if (user.AdministratorId != userId)
            {
                throw new Exception("Invalid user tries to add a licence key!");
            }

            string userName = user.FirstName + user.MiddleName + user.LastName;
            string licence = await BlindUserKey.GenerateKey(userName, user.PersonalId, random);

            BlindUserLicence blindUserLicence = new BlindUserLicence();
            blindUserLicence.LicenceNumber = licence;
            blindUserLicence.BlindUserId = id;
            blindUserLicence.DateFrom = dateFrom;
            blindUserLicence.IsActivated = false;

            await context.BlindUsersLicences.AddAsync(blindUserLicence);
            await context.SaveChangesAsync();

            LicenseKeyViewModel model = new LicenseKeyViewModel();
            model.DateFrom = blindUserLicence.DateFrom.ToString(Constants.DateTimeFormat);
            model.Id = blindUserLicence.Id.ToString();
            model.BlindUserId = blindUserLicence.BlindUserId.ToString();
            model.Key = blindUserLicence.LicenceNumber;
            return model;
        } // GenerateLicence

        public async Task<LicenseDeleteViewModel> DeleteLicenseConfirm(string userId, string licenceId)
        {
            // Преобразуване на лицензния идентификатор в Guid
            var licenceIdGuid = GuidParser.GuidParse(licenceId);

            // Проверка дали съществува незрящ потребител с въведения идентикатор, който да е с администратор потребителя,
            // влязъл в системата
            bool isBlindUserValid = await context.BlindUsers.AnyAsync(x =>
                                    x.BlindUserLicences.Any(x => x.Id == licenceIdGuid) && x.AdministratorId == userId);

            if (!isBlindUserValid)
            {
                throw new ArgumentException("Invalid user or administrator!");
            }

            // Взимане на лиценза от базата данни
            var licence = await context.BlindUsersLicences.FirstOrDefaultAsync(x => x.Id == licenceIdGuid);

            LicenseDeleteViewModel licenceViewModel = new LicenseDeleteViewModel();
            licenceViewModel.Id = licence.Id.ToString();
            licenceViewModel.MACAddress = licence.MacAddress;
            licenceViewModel.DateFrom = licence.DateFrom.Date.ToString(Constants.DateTimeFormat);
            licenceViewModel.BlindUserId = licence.BlindUserId.ToString();
            return licenceViewModel;
        }
        public async Task<string> DeleteLicense(string userId, string licenceId)
        {
            // Преобразуване на лицензния идентификатор в Guid
            var licenceIdGuid = GuidParser.GuidParse(licenceId);

            // Проверка дали съществува незрящ потребител с въведения идентикатор, който да е с администратор потребителя,
            // влязъл в системата
            bool isBlindUserValid = await context.BlindUsers.AnyAsync(x =>
                                    x.BlindUserLicences.Any(x => x.Id == licenceIdGuid) && x.AdministratorId == userId);

            if (!isBlindUserValid)
            {
                throw new ArgumentException("Invalid user or administrator!");
            }
            // Взимане на лиценза от базата данни
            var licence = await context.BlindUsersLicences.FirstOrDefaultAsync(x => x.Id == licenceIdGuid);

            if (licence == null)
            {
                throw new ArgumentException("Invalid licence id!");
            }

            licence.IsDeleted = true;

            await context.SaveChangesAsync();


            return licence.BlindUserId.ToString();
        }

    }
}

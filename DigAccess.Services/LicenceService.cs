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
using DigAccess.Models.Licence;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services
{
    public class LicenceService : BaseService, ILicenceService
    {
        public LicenceService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // LicenceService

        public async Task<List<LicenceAllViewModel>> GetAll(string userId)
        {
            var users = await context.BlindUsers.Where(x => x.AdministratorId == userId)
                .Select(x=> new LicenceAllViewModel()
                {
                    Id = x.Id,
                    LicenceNumber = context.BlindUsersLicences.Count(y=> y.BlindUserId == x.Id && y.IsActivated),
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToListAsync();

            return users;
        } // GetAll

        public async Task<UserLicenceViewModel> GetLicences(string blindUserId, string userId)
        {
            // Проверка дали въведения идентификатор за незрящо лице е във валиден формат
            bool isIdValid = Guid.TryParse(blindUserId, out Guid id);

            if (!isIdValid)
            {
                throw new Exception("Invalid id!");
            }

            var blindUser = await context.BlindUsers.FindAsync(id);

            // Проверка дали администратора на незрящото лице е потребителя, влезнал в системата
            if (blindUser.AdministratorId != userId)
            {
                throw new Exception("Invalid administrator!");
            }

            var licences = context.BlindUsers.Where(x => x.Id == id)
                .Select(x => new UserLicenceViewModel()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Licences = x.BlindUserLicences.Select(y => new LicenceViewModel()
                    {
                        DateFrom = y.DateFrom.ToString(Constants.DateTimeFormat),
                        DateTo = y.DateTo.ToString(Constants.DateTimeFormat),
                        Id = y.Id,
                        IsActivated = y.IsActivated,
                    }).ToList()
                }).FirstOrDefault();

            return licences;
        } // GetLicences

        public async Task GenerateLicence(string blindUserId, string userId, Random random, DateTime dateFrom)
        {
            // Проверка дали въведения идентификатор за незрящо лице е Във валиден формат
            bool isIdValid = Guid.TryParse(blindUserId, out Guid id);

            if (!isIdValid)
            {
                throw new Exception("Invalid id!");
            }

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
            blindUserLicence.DateTo = dateFrom.AddMonths(6);
            blindUserLicence.IsActivated = true;
            
            await context.BlindUsersLicences.AddAsync(blindUserLicence);
            await context.SaveChangesAsync();
        } // GenerateLicence
    }
}

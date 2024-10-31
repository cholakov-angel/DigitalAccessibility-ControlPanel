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

        public async Task<UserLicenceViewModel> GetLicences(string userId)
        {
            bool isIdValid = Guid.TryParse(userId, out Guid id);

            if (!isIdValid)
            {
                throw new Exception("Invalid id!");
            }
            var blindUser = await context.BlindUsers.FindAsync(id);
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
            bool isIdValid = Guid.TryParse(blindUserId, out Guid id);

            if (!isIdValid)
            {
                throw new Exception("Invalid id!");
            }

            var user = await context.BlindUsers.FirstOrDefaultAsync(x => x.Id == id);

            if (user.AdministratorId != userId)
            {
                throw new Exception("Invalid user tries to add a licence key!");
            }
            string licence = await BlindUserKey.GenerateKey(user.FirstName + user.MiddleName + user.LastName, user.PersonalId, random);
        
            BlindUserLicence blindUserLicence = new BlindUserLicence();
            blindUserLicence.LicenceNumber = licence;
            blindUserLicence.BlindUserId = id;
            blindUserLicence.DateFrom = dateFrom;
            blindUserLicence.DateTo = dateFrom.AddMonths(6);
            blindUserLicence.IsActivated = true;
            
            await context.BlindUsersLicences.AddAsync(blindUserLicence);
            await context.SaveChangesAsync();
        }
    }
}

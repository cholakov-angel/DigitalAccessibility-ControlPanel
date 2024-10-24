using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Interfaces;
using DigAccess.Models.Licence;
using DigAccess.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services
{
    public class LicenceService : BaseService, ILicenceService
    {
        public LicenceService(DigAccessDbContext context) : base(context)
        {

        }

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
        }

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
                        DateFrom = y.DateFrom.ToString("dd.MM.yyyy"),
                        DateTo = y.DateTo.ToString("dd.MM.yyyy"),
                        Id = y.Id,
                        IsActivated = y.IsActivated,
                    }).ToList()
                }).FirstOrDefault();

            return licences;
        }
    }
}

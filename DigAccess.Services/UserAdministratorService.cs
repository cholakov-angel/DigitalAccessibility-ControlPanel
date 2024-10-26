using DigAccess.Interfaces;
using DigAccess.Models.UserAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services
{
    public class UserAdministratorService : BaseService, IUserAdministratorService
    {
        public UserAdministratorService(DigAccessDbContext context) : base(context)
        {
        }

        public async Task<HomePageViewModel> GetAllUsers(string userId)
        {
            HomePageViewModel model = new HomePageViewModel();
            model.BlindUsers = await context.BlindUsers
                .Where(x => x.AdministratorId == userId)
                .OrderBy(x => x.FirstName)
                .Take(5)
                .Select(x => new BlindUserHomePageViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToListAsync();
            return model;
        }
    }
}

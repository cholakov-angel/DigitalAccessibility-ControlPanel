using DigAccess.Data.Entities;
using DigAccess.Interfaces;
using DigAccess.Models.UserAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services
{
    public class UserAdministratorService : BaseService, IUserAdministratorService
    {
        public UserAdministratorService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // UserAdministratorService

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
        } // GetAllUsers
    } // UserAdministratorService
}

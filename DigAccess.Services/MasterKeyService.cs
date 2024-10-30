using DigAccess.Data.Entities;
using DigAccess.Models.MasterKey;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services
{
    public class MasterKeyService : BaseService, IMasterKeyService
    {
        public MasterKeyService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // MasterKeyService

        public async Task<MasterKeyIndexViewModel> GetUserMasterKey(string id)
        {
            var user = await userManager.Users.Select(x=> new MasterKeyIndexViewModel
            {
                UserId = x.Id,
                MasterKey = x.MasterKey
            }).FirstOrDefaultAsync(x=> x.UserId == id);

            return user;
        }
    }
}

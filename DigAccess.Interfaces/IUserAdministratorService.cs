using DigAccess.Models.UserAdministrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IUserAdministratorService
    {
        public Task<HomePageViewModel> GetAllUsers(string userId);
    }
}

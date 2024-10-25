using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Models.BlindUser;
using DigAccess.Web.Data;

namespace DigAccess.Interfaces
{
    public interface IBlindUserService : IService
    {
        public Task<List<BlindUserViewModel>> GetAllModels(string userId);
        public Task<bool> Add(BlindUserViewModel model, string userId);
        public Task<List<CityViewModel>> GetCities();
        public Task<BlindUserDetailsViewModel> GetUserDetails(string id);
    } // IBlindUserService
}

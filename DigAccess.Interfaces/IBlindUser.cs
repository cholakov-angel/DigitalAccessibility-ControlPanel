using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Models.BlindUser;
using DigAccess.Web.Data;

namespace DigAccess.Interfaces
{
    public interface IBlindUser : IService
    {
        public Task<List<BlindUserViewModel>> GetAllModels(string userId);
        public Task Add(BlindUserViewModel model, string userId);
        public BlindUserViewModel FindById(string id);
        public void SetContext(DigAccessDbContext context);

        public Task<List<CityViewModel>> GetCities();
    }
}

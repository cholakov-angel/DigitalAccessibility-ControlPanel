using DigAccess.Interfaces;
using DigAccess.Models.OfficeAdministrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IOfficeDetailsService : IService
    {
        public Task<List<CityViewModel>> GetCities();
        public Task<OfficeViewModel> GetOfficeDetails(string userId);
        public Task<bool> UpdateOffice(string userId, OfficeViewModel office);
    } // IOfficeDetailsService
}

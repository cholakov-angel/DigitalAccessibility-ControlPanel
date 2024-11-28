using Azure;
using DigAccess.Interfaces;
using DigAccess.Models.OrgAdministrator;

namespace DigAccess.Services.Interfaces
{
    public interface IOfficeOrgAdminService : IService
    {
        public Task<List<CityViewModel>> GetCities();
        public Task<bool> AddOffice(string userId, EditOfficeViewModel model);
        public Task<bool> EditOffice(string userId, EditOfficeViewModel model);
        public Task<List<OfficeInfoViewModel>> GetOffices(string userId, int page);
        public Task<EditOfficeViewModel> GetOfficeDetails(string userId, string officeId);
        public Task<OfficeViewModel> GetFullOfficeDetails(string userId, string officeId, int page);
        public Task<OfficeAdminViewModel> GetOfficeAdmin(string userId, string officeAdminId);
        public Task<List<OfficeInfoViewModel>> GetOfficesByName(string userId, string name);
        public Task<bool> DeleteOffice(string userId, string officeId);
        public Task<int> CountOffices(string userId);
        public Task<int> CountWorkers(string userId, string officeId);

    }
}

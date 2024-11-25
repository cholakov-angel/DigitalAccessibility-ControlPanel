using DigAccess.Interfaces;
using DigAccess.Models.OrgAdministrator;

namespace DigAccess.Services.Interfaces
{
    public interface IOfficeOrgAdminService : IService
    {
        public Task<List<CityViewModel>> GetCities();
        public Task<bool> AddOffice(string userId, EditOfficeViewModel model);
        public Task<bool> EditOffice(string userId, EditOfficeViewModel model);
        public Task<List<OfficeInfoViewModel>> GetOffices(string userId);
        public Task<EditOfficeViewModel> GetOfficeDetails(string userId, string officeId);
        public Task<OfficeViewModel> GetFullOfficeDetails(string userId, string officeId);
        public Task<OfficeAdminViewModel> GetOfficeAdmin(string userId, string officeAdminId);
        public Task<AddOfficeAdminViewModel> AddOfficeAdmin(string userId, string officeId);
        public Task<bool> AddOfficeAdmin(string userId, AddOfficeAdminViewModel model);
        public Task<bool> AddUserAsAdmin(string userId, string officeUserId);
        public Task<bool> RemoveUserAdmin(string userId, string officeAdminId);

    }
}

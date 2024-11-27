using DigAccess.Interfaces;
using DigAccess.Models.OrgAdministrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IUsersOrgAdminService : IService
    {
        public Task<bool> AddUserAsAdmin(string userId, string officeUserId);
        public Task<bool> AddOfficeAdmin(string userId, AddOfficeAdminViewModel model);
        protected Task<bool> DoesOfficeHasAnAdmin(string officeId);
        public Task<AddOfficeAdminViewModel> AddOfficeAdmin(string userId, string officeId);
        public Task<bool> RemoveUserAdmin(string userId, string officeAdminId);
    } // IUsersOrgAdminService
}

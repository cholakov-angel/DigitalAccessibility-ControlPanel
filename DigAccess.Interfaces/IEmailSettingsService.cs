using DigAccess.Interfaces;
using DigAccess.Models.UserAdministrator.EmailSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IEmailSettingsService : IService
    {
        public Task<EmailViewModel> GetEmail(string userId, string blindUserId);
        public Task<EmailAddViewModel> Add(string userId, string blindUserId);
        public Task<bool> AddEdit(EmailAddViewModel model, string userId);
        public Task<List<EmailProviderViewModel>> GetProvider();
        public Task<EmailAddViewModel> Edit(string id, string userId);
        public Task<bool> HasEmail(string userId, string blindUserId);
    } // IEmailSettingsService
}

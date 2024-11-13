using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Blind;
using DigAccess.Models.EmailSettings;
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
    public class EmailSettingsService : BaseService, IEmailSettingsService
    {
        public EmailSettingsService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // EmailSettingsService

        public async Task<EmailViewModel> GetEmail(string userId, string blindUserId)
        {
            var blindUserIdGuid = GuidParser.GuidParse(blindUserId);

            var user = await context.BlindUsers.FirstOrDefaultAsync(x => x.Id == blindUserIdGuid && x.AdministratorId == userId);
            if (user == null)
            {
                throw new Exception("Invalid user!");
            }

            var licence = context.BlindUsersEmails.FirstOrDefault(x => x.BlindUserId == user.Id);

            EmailViewModel model = new EmailViewModel();
            model.BlindUserId = blindUserId.ToString();

            if (licence != null)
            {
                var emailSettings = await context.EmailsSettings.FirstOrDefaultAsync(x => x.Id == licence.EmailSettingsId);
                if (emailSettings == null)
                {
                    throw new ArgumentException("Invalid email provider!");
                }
                
                model.Id = licence.Id.ToString();
                model.BlindUserId = licence.BlindUserId.ToString();
                model.Email = licence.Email;
                model.EmailProvider = emailSettings.Name;
            }
            return model;
        } // GetEmail

        public async Task<EmailAddViewModel> Add(string userId, string blindUserId)
        {
            var blindUserIdGuid = GuidParser.GuidParse(blindUserId);

            var user = await context.BlindUsers.FirstOrDefaultAsync(x => x.Id == blindUserIdGuid && x.AdministratorId == userId);
            if (user == null)
            {
                throw new Exception("Invalid user!");
            }

            EmailAddViewModel model = new EmailAddViewModel();
            model.BlindUserId = blindUserId;
            model.AdministratorId = userId;
            model.EmailProviders = await context.EmailsSettings.Select(x => new EmailProviderViewModel
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToListAsync();

            return model;
        } // Add

        public async Task<List<EmailProviderViewModel>> GetProvider()
        {
            return await context.EmailsSettings.Select(x => new EmailProviderViewModel
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToListAsync();
        } // GetProvider

        public async Task<bool> AddEdit(EmailAddViewModel model, string userId)
        {
            if (model == null)
            {
                throw new Exception("Invalid model!");
            }

            if (model.AdministratorId != userId)
            {
                throw new ArgumentException("Invalid user!");
            }

            var blindUserIdGuid = GuidParser.GuidParse(model.BlindUserId);

            var user = await context.BlindUsers.FirstOrDefaultAsync(x => x.Id == blindUserIdGuid && x.AdministratorId == model.AdministratorId);
            if (user == null)
            {
                throw new ArgumentException("Invalid user");
            }

            var email = context.BlindUsersEmails.FirstOrDefault(x => x.BlindUserId == user.Id);

            if (email == null)
            {
                email = new BlindUserEmail();
                await context.BlindUsersEmails.AddAsync(email);
            }

            var emailProviderId = GuidParser.GuidParse(model.EmailProvider);

            email.BlindUserId = blindUserIdGuid;
            email.Email = model.Email;
            email.EmailPassword = model.Password;
            email.EmailSettingsId = emailProviderId;

            await context.SaveChangesAsync();
            return true;
        } // AddEdit

        public async Task<EmailAddViewModel> Edit(string id, string userId)
        {
            var emailId = GuidParser.GuidParse(id);

            var licence = context.BlindUsersEmails.FirstOrDefault(x => x.Id == emailId);

            if (licence == null)
            {
                throw new ArgumentException("Invalid email!");
            }

            bool isUserValid = await context.BlindUsers.AnyAsync(x => x.Id == licence.BlindUserId && x.AdministratorId == userId);
            if (isUserValid == false)
            {
                throw new ArgumentException("Invalid user!");
            }

            EmailAddViewModel model = new EmailAddViewModel();
            model.Id = licence.Id.ToString();
            model.EmailProviders = await this.GetProvider();
            model.Email = licence.Email;
            model.Password = licence.EmailPassword;
            model.AdministratorId = userId;
            model.BlindUserId = licence.BlindUserId.ToString();

            return model;
        } // Edit

        public async Task<bool> HasEmail(string userId, string blindUserId)
        {
            var blindUserIdGuid = GuidParser.GuidParse(blindUserId);

            var user = await context.BlindUsers.FirstOrDefaultAsync(x => x.Id == blindUserIdGuid && x.AdministratorId == userId);
            if (user == null)
            {
                throw new Exception("Invalid user!");
            }

            var email = context.BlindUsersEmails.FirstOrDefault(x => x.BlindUserId == user.Id);

            if (email == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        } // HasEmail
    } // EmailSettingsService
}

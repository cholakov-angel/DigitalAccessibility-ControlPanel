using DigAccess.Application;
using DigAccess.Services.Interfaces;
using DigAccess.ViewModels;
namespace DigAccess.Services
{
    public class BlindUserService : BaseService, IBlindUserService
    {
        public BlindUserService(DigAccessAdminPanelContext context) : base(context)
        {
        } // BlindUserService

        public async Task<bool> AddLog(LogViewModel model)
        {
            bool result = Guid.TryParse(model.BlindUserId, out Guid blindUserIdGuid);
            if (result == false)
            {
                throw new ArgumentException("Invalid user id format!");
            }
            var user = this.context.BlindUsers.FirstOrDefault(x => x.Id == blindUserIdGuid && x.AdministratorId == model.AdminsitratorId);
            if (user == null)
            {
                return false;
            }

            bool isDateValid = DateTime.TryParse(model.DateTimeOfLog, out DateTime dateOfLog);

            if (isDateValid == false)
            {
                throw new ArgumentException("Invalid date format!");
            }
            BlindUsersLog log = new BlindUsersLog();
            log.BlindUserId = blindUserIdGuid;
            log.LogText = model.LogText;
            log.LogType = model.LogType;
            log.LogCode = model.LogCode;
            log.DateTimeOfLog = dateOfLog;

            await this.context.BlindUsersLogs.AddAsync(log);
            await this.context.SaveChangesAsync();

            return true;
        } // AddLog

        public async Task<EmailViewModel> GetEmail(BlindUserViewModel parameterModel)
        {
            bool result = Guid.TryParse(parameterModel.Id, out Guid blindUserIdGuid);
            if (result == false)
            {
                throw new ArgumentException("Invalid user id format!");
            }
            var user = this.context.BlindUsers.FirstOrDefault(x => x.Id == blindUserIdGuid && x.AdministratorId == parameterModel.AdministratorId);
            if (user == null)
            {
                return null;
            }
            var email = this.context.BlindUsersEmails.FirstOrDefault(x => x.BlindUserId == blindUserIdGuid);
            if (email == null)
            {
                throw new ArgumentException("Invalid email");
            }
            var emailSettings = this.context.EmailsSettings.FirstOrDefault(x => x.Id == email.EmailSettingsId);
            
            if (emailSettings == null)
            {
                throw new ArgumentException("Invalid email settings!");
            }
            EmailViewModel model = new EmailViewModel();
            model.Email = email.Email;
            model.Password = email.EmailPassword;
            model.IncommingServer = emailSettings.IncomingServer;
            model.OutgoingServer = emailSettings.OutgoingServer;
            model.Port = emailSettings.Port;

            return model;
        } // GetEmail
    } // BlindUserService
}

using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Blind;
using DigAccess.Models.UserAdministrator.Log;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services.UserAdministrator
{
    public class LogService : BaseService, ILogService
    {
        public LogService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // LogService

        public async Task<List<LogViewModel>> GetLogs(string userId, string blindUserId)
        {
            var blindUserIdGuid = GuidParser.GuidParse(blindUserId);

            if (await context.BlindUsers.AnyAsync(x => x.Id == blindUserIdGuid && x.AdministratorId == userId) == false)
            {
                throw new Exception("Invalid user!");
            }

            return await this.context.BlindUsersLogs.Where(x => x.BlindUserId == blindUserIdGuid)
                .Select(x => new LogViewModel()
                {
                    Id = x.Id.ToString(),
                    BlindUserId = x.BlindUserId.ToString(),
                    LogType = x.LogType,
                    DateTimeOfLog = x.DateTimeOfLog.ToString(Constants.DateTimeFormat)
                }
            ).ToListAsync();
        } // GetLogs

        public async Task<int> CountUsers(string userId, string blindUserId)
        {
            var blindUserIdGuid = GuidParser.GuidParse(blindUserId);

            if (await context.BlindUsers.AnyAsync(x => x.Id == blindUserIdGuid && x.AdministratorId == userId) == false)
            {
                throw new Exception("Invalid user!");
            }

            return await this.context.BlindUsersLogs.Where(x => x.BlindUserId == blindUserIdGuid).CountAsync();
        } // CountUsers

        public async Task<LogDetailsViewModel> GetLog(string userId, string id)
        {
            var userIdGuid = GuidParser.GuidParse(userId);

            return await this.context.BlindUsersLogs.Include(x => x.BlindUser)
                .Where(x => x.Id == GuidParser.GuidParse(id) && x.BlindUser.AdministratorId == userId)
                .Select(x => new LogDetailsViewModel() 
                { 
                    Id = x.Id.ToString(),
                    BlindUserId = x.BlindUserId.ToString(),
                    LogType = x.LogType,
                    LogText = x.LogText,
                    LogCode = x.LogCode,
                    DateTimeOfLog = x.DateTimeOfLog.ToString(Constants.DateTimeFormat)
                }).FirstOrDefaultAsync();
        } // GetLog
    } // LogService
}

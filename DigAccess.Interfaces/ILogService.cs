using DigAccess.Interfaces;
using DigAccess.Models.UserAdministrator.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface ILogService : IService
    {
        public Task<List<LogViewModel>> GetLogs(string userId, string blindUserId);
        public Task<int> CountUsers(string userId, string blindUserId);
        public Task<LogDetailsViewModel> GetLog(string userId, string id);
    } // ILogService
}

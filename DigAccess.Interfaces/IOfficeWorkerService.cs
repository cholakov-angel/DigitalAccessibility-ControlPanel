using DigAccess.Interfaces;
using DigAccess.Models.OfficeWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IOfficeWorkerService : IService
    {
        public Task<WaitingUsersViewModel> GetWaitingUsers(string id);

        public Task<bool> ApproveUser(string workerId, string userId);

        public Task<bool> RejectUser(string workerId, string userId);

        public Task<UserDetailsViewModel> GetUserDetails(string workerId, string userId);
    }
}

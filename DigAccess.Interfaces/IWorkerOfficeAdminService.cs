using DigAccess.Models.OfficeAdministrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IWorkerOfficeAdminService
    {
        public Task<List<WorkerViewModel>> GetWorkers(string id, int page);
        public Task<int> CountUsers(string workerId);
        public Task<bool> AddOfficeWorker(string userId, AddWorkerViewModel model);
    } // IWorkerOfficeAdminService
}

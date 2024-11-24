using DigAccess.Models.OfficeAdministrator;

namespace DigAccess.Services.Interfaces
{
    public interface IWorkerOfficeAdminService
    {
        public Task<List<WorkerViewModel>> GetWorkers(string id, int page);
        public Task<int> CountUsers(string workerId);
        public Task<bool> AddOfficeWorker(string userId, AddWorkerViewModel model);
        public Task<WorkerDetailsViewModel> WorkerDetails(string userId, string workerId);
        public Task<bool> DeleteUser(string workerId, string userId);
    } // IWorkerOfficeAdminService
}

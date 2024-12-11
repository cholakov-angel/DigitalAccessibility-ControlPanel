using DigAccess.Interfaces;
using DigAccess.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IAdminOrgService : IService
    {
        public Task<OrganisationViewModel> GetOrganisation(string userId, string orgId);
        public Task<List<OrganisationListViewModel>> GetOrganisations(string userId, int page);
        public Task<List<OrganisationListViewModel>> GetOrganisationsByName(string userId, string name);
        public Task<bool> DeleteOrganisation(string userId, string orgId);
        public Task<int> CountOrganisations(string userId);
        public Task<OrganisationEditViewModel> EditOrganisation(string userId, string orgId);
        public Task<bool> EditOrganisation(string userId, OrganisationEditViewModel org);
        public Task<List<OrgWorkerViewModel>> GetOrgWorkers(string userId, string orgId);
        public Task<bool> ChangeAdministrator(string userId, string workerId);
        public Task<bool> AddOrganisation(string userId, AddOrgViewModel model);
    } // IAdminOrgService
}

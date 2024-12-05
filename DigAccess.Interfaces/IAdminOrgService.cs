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
        public Task<int> CountOrganisations(string userId);


    } // IAdminOrgService
}

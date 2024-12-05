using DigAccess.Interfaces;
using DigAccess.Models.OrgAdministrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IOrganisationOrgAdminService : IService
    {
        public Task<OrganisationViewModel> GetOrganisationDetails(string userId);
        public Task<bool> EditOrganisation(string userId, OrganisationViewModel model);
    } // IOrganisationOrgAdminService
}

using DigAccess.Interfaces;
using DigAccess.Models.WaitingApproval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IWaitingApprovalService : IService
    {
        public Task<List<OrganisationViewModel>> GetOrganisations();
        public Task<WaitingApprovalViewModel> OfficeSelect(string organisationId);
        public Task<List<OfficeViewModel>> GetOffices(string organisationId);
        public Task<bool> SetOrganisationForUser(string userId, WaitingApprovalViewModel model);
    }
}

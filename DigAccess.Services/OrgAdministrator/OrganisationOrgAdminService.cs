using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Models.OrgAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.OrgAdministrator
{
    public class OrganisationOrgAdminService : BaseService, IOrganisationOrgAdminService
    {
        private string role = "OrgAdministrator";
        public OrganisationOrgAdminService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // OrganisationOrgAdminService

        public async Task<OrganisationViewModel> GetOrganisationDetails(string userId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var model = await this.context.Organisations.Where(x => x.Id == user.OrganisationId)
                                .Select(x => new OrganisationViewModel()
                                {
                                    Id = x.Id.ToString(),
                                    Name = x.Name,
                                    National_Phone = x.National_Phone
                                }).FirstOrDefaultAsync();

            if (model == null)
            {
                throw new Exception("Error!");
            }
            return model;
        } // GetOrganisationDetails

        public async Task<bool> EditOrganisation(string userId, OrganisationViewModel model)
        {
            var user = await this.GetOfficeWorker(userId, role);

            if (GuidParser.GuidParse(model.Id) != user.OrganisationId)
            {
                throw new ArgumentException("Invalid user!");
            }
            var orgObj = await this.context.Organisations.Where(x => x.Id == user.OrganisationId).FirstOrDefaultAsync();

            if (model == null)
            {
                return false;
            }

            orgObj.National_Phone = model.National_Phone;
            orgObj.Name = model.Name;

            await this.context.SaveChangesAsync();
            return true;
        }
    } // OrganisationOrgAdminService
}

using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Models.Admin;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Admin
{
    public class AdminOrgService : BaseService, IAdminOrgService
    {
        private const string role = "Admin";
        public AdminOrgService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // AdminOrgService

        public async Task<List<OrganisationListViewModel>> GetOrganisations(string userId, int page)
        {
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Organisations.Where(x => x.IsDeleted == false)
                .Select(x => new OrganisationListViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Phone = x.National_Phone
                }).Skip((page - 1) * 8).Take(8).ToListAsync();
        } // GetOrganisations

        public async Task<OrganisationViewModel> GetOrganisation(string userId, string orgId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Organisations.Where(x => x.IsDeleted == false)
                .Select(x => new OrganisationViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Phone = x.National_Phone,
                    OrgAdmin = this.userManager.Users.Where(x => x.OrganisationId == GuidParser.GuidParse(orgId))
                    .Select(x => new OrgAdminViewModel()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        MiddleName = x.MiddleName,
                        LastName = x.LastName,
                        Email = x.Email,
                        Phone = x.PhoneNumber,
                        Birthdate = PersonalIDParser.BirthdateExtract(x.PersonalId).ToString(Constants.DateTimeFormat)
                    }).FirstOrDefault()
                }).FirstOrDefaultAsync();
        } // GetOrganisation

        public async Task<int> CountOrganisations(string userId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Organisations.Where(x => x.IsDeleted == false).CountAsync();
        }
    } // AdminOrgService
}

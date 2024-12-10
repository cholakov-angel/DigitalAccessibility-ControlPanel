using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Models.WaitingApproval;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services
{
    public class WaitingApprovalService : BaseService, IWaitingApprovalService
    {
        public WaitingApprovalService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // WaitingApprovalService

        public async Task<List<OfficeViewModel>> GetOffices(string organisationId)
        {
            var organisationIdGuid = GuidParser.GuidParse(organisationId);

            return await context.Offices.Where(x => x.OrganisationId == organisationIdGuid && x.IsDeleted == false)
               .Select(x => new OfficeViewModel
               {
                   Id = x.Id.ToString(),
                   Name = x.Name
               }).ToListAsync();
        } // GetOffices

        public async Task<List<OrganisationViewModel>> GetOrganisations()
        {
            return await context.Organisations.Where(x=> x.IsDeleted == false).Select(x => new OrganisationViewModel
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToListAsync();
        } // GetOrganisations

        public async Task<WaitingApprovalViewModel> OfficeSelect(string organisationId)
        {
            var organisationIdGuid = GuidParser.GuidParse(organisationId);

            var organisation = await context.Organisations.FirstOrDefaultAsync(x => x.Id == organisationIdGuid && x.IsDeleted == false);
            if (organisation == null)
            {
                throw new ArgumentException("Invalid organisation!");
            }

            WaitingApprovalViewModel model = new WaitingApprovalViewModel();
            model.OrganisationId = organisationId;
            model.OrganisationName = organisation.Name;
            model.Offices = await context.Offices.Where(x => x.OrganisationId == organisationIdGuid && x.IsDeleted == false)
                .Select(x => new OfficeViewModel
                {
                    Id = x.Id.ToString(),
                    Name = x.Name
                }).ToListAsync();

            return model;
        } // OfficeSelect

        public async Task<bool> SetOrganisationForUser(string userId, WaitingApprovalViewModel model)
        {
            if (userId == null || model == null)
            {
                throw new ArgumentException("Invalid parameters!");
            }

            var user = await userManager.Users.FirstOrDefaultAsync(x=> x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid parameters!");
            }

            var organisationIdGuid = GuidParser.GuidParse(model.OrganisationId);

            // Проверка дали идентификатора е във валиден формат
            Guid officeIdGuid = GuidParser.GuidParse(model.OfficeId);

            if (await context.Offices.AnyAsync(x=> x.Id ==  officeIdGuid && x.OrganisationId == organisationIdGuid && x.IsDeleted == false) == false)
            {
                throw new ArgumentException("Invalid model!");
            }

            user.OfficeId = officeIdGuid;
            await context.SaveChangesAsync();
            return true;
        } // SetOrganisationForUser
    } // WaitingApprovalService
}

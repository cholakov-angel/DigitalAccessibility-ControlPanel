using Azure;
using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Enums;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Models.Admin;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            return await this.context.Organisations.Where(x => x.IsDeleted == false && x.Id == GuidParser.GuidParse(orgId))
                .Select(x => new OrganisationViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Phone = x.National_Phone,
                    OrgAdmin = this.userManager.Users.Where(x => x.OrganisationId == GuidParser.GuidParse(orgId) && x.OfficeId == null)
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

        public async Task<bool> DeleteOrganisation(string userId, string orgId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var organisation = await this.context.Organisations.FirstOrDefaultAsync(x => x.Id == GuidParser.GuidParse(orgId));

            organisation.IsDeleted = true;
            await this.context.SaveChangesAsync();

            return true;
        } // DeleteOrganisation

        public async Task<int> CountOrganisations(string userId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Organisations.Where(x => x.IsDeleted == false).CountAsync();
        }

        public async Task<OrganisationEditViewModel> EditOrganisation(string userId, string orgId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Organisations.Where(x => x.Id == GuidParser.GuidParse(orgId) && x.IsDeleted == false)
                .Select(x => new OrganisationEditViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Phone = x.National_Phone
                }).FirstOrDefaultAsync();
        } // EditOrganisation

        public async Task<bool> EditOrganisation(string userId, OrganisationEditViewModel org)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var organisation = await this.context.Organisations.FirstOrDefaultAsync(x => x.Id == GuidParser.GuidParse(org.Id)
                                                                                && x.IsDeleted == false);

            if (organisation == null)
            {
                return false;
            }
            organisation.Name = org.Name;
            organisation.National_Phone = org.Phone;

            await this.context.SaveChangesAsync();
            return true;
        } // EditOrganisation

        public async Task<bool> AddOrganisation(string userId, AddOrgViewModel model)
        {
            var user = await this.GetOfficeWorker(userId, role);

            OrganisationCompany organisation = new OrganisationCompany();
            organisation.Name = model.Name;
            organisation.National_Phone = model.Phone;

            await this.context.Organisations.AddAsync(organisation);
            await this.context.SaveChangesAsync();

            ApplicationUser officeWorker = new ApplicationUser();
            officeWorker.UserName = model.OrgAdministrator.Email;
            officeWorker.Email = model.OrgAdministrator.Email;
            officeWorker.ApprovalStatus = 1;
            officeWorker.PhoneNumber = model.OrgAdministrator.PhoneNumber;
            officeWorker.FirstName = model.OrgAdministrator.FirstName;
            officeWorker.MiddleName = model.OrgAdministrator.MiddleName;
            officeWorker.LastName = model.OrgAdministrator.LastName;

            officeWorker.OrganisationId = this.context.Organisations.FirstOrDefault(x => x.Name == organisation.Name).Id;
            officeWorker.PersonalId = model.OrgAdministrator.PersonalID;
            officeWorker.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract(model.OrgAdministrator.PersonalID));

            var result = await userManager.CreateAsync(officeWorker, model.OrgAdministrator.Password);
            if (result.Succeeded == false)
            {
                return false;
            }
            IdentityResult roleresult = await userManager.AddToRoleAsync(officeWorker, "OrgAdministrator");

            if (roleresult.Succeeded == false)
            {
                return false;
            }

            return true;
        } // AddOrganisation

        public async Task<List<OrgWorkerViewModel>> GetOrgWorkers(string userId, string orgId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            bool isOrgValid = await this.context.Organisations.AnyAsync(x => x.Id == GuidParser.GuidParse(orgId)
                                                                                && x.IsDeleted == false);
            if (isOrgValid == false)
            {
                throw new ArgumentException("Invalid organisation id!");
            }

            return await this.userManager.Users.Where(x => x.OrganisationId == GuidParser.GuidParse(orgId) && x.OfficeId != null)
                .Select(x => new OrgWorkerViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName
                }).ToListAsync();
        } // GetOrgWorkers

        public async Task<bool> ChangeAdministrator(string userId, string workerId)
        {
            var user = await this.GetOfficeWorker(userId, role);
            
            var worker = await this.userManager.FindByIdAsync(workerId);

            if (worker == null)
            {
                return false;
            }

            var workers = await this.userManager.Users
                                .Where(x => x.OrganisationId == worker.OrganisationId)
                                .ToListAsync();

            foreach (var orgWorker in workers)
            {
                if (await userManager.IsInRoleAsync(orgWorker, "OrgAdministrator"))
                {
                    await userManager.DeleteAsync(orgWorker);
                }
            }
            worker.OfficeId = null;
            await userManager.AddToRoleAsync(worker, "OrgAdministrator");
            await userManager.RemoveFromRoleAsync(worker, "OfficeWorker");
            await userManager.RemoveFromRoleAsync(worker, "OfficeAdministrator");

            await this.context.SaveChangesAsync();

            return true;
        } // ChangeAdministrator

        public async Task<List<OrganisationListViewModel>> GetOrganisationsByName(string userId, string name)
        {
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Organisations.Where(x => x.IsDeleted == false && x.Name.ToLower().StartsWith(name.ToLower()))
                .Select(x => new OrganisationListViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Phone = x.National_Phone
                }).ToListAsync();
        } // GetOrganisationsByName
    } // AdminOrgService
}

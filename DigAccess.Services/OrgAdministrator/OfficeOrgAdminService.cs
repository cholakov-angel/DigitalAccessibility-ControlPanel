using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Enums;
using DigAccess.Data.Entities.Organisation.Organisation;
using DigAccess.Models.OrgAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services.OrgAdministrator
{
    public class OfficeOrgAdminService : BaseService, IOfficeOrgAdminService
    {
        private string role = "OrgAdministrator";
        public OfficeOrgAdminService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // OfficeOrgAdminService

        public async Task<List<CityViewModel>> GetCities()
        {
            return await this.context.Cities.Select(x => new CityViewModel
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToListAsync();
        } // GetCities

        public async Task<bool> AddUserAsAdmin(string userId, string officeUserId)
        {
            var userAdmin = await this.GetOfficeWorker(userId, role);
            var officeUser = await this.GetOfficeWorker(officeUserId, "OfficeWorker");

            bool doesOfficeHasAnAdmin = await this.DoesOfficeHasAnAdmin(officeUser.OfficeId.ToString());

            if (doesOfficeHasAnAdmin == true)
            {
                throw new Exception("Office has an aministrator!");
            }

            await this.userManager.RemoveFromRoleAsync(officeUser, "OfficeWorker");
            await this.userManager.AddToRoleAsync(officeUser, "OfficeAdministrator");
            await this.context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> AddOfficeAdmin(string userId, AddOfficeAdminViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentException("Invalid model!");
            }
            var userAdmin = await this.GetOfficeWorker(userId, role);

            bool doesOfficeHasAnAdmin = await this.DoesOfficeHasAnAdmin(model.OfficeId);

            if (doesOfficeHasAnAdmin == true)
            {
                throw new Exception("Office has an aministrator!");
            }

            var user = await GetOfficeWorker(userId, role);

            ApplicationUser officeWorker = new ApplicationUser();
            officeWorker.UserName = model.Email;
            officeWorker.Email = model.Email;
            officeWorker.ApprovalStatus = 1;
            officeWorker.PhoneNumber = model.PhoneNumber;
            officeWorker.FirstName = model.FirstName;
            officeWorker.MiddleName = model.MiddleName;
            officeWorker.LastName = model.LastName;

            officeWorker.OfficeId = GuidParser.GuidParse(model.OfficeId);
            officeWorker.OrganisationId = this.context.Offices.FirstOrDefault(x => x.Id == GuidParser.GuidParse(model.OfficeId)).OrganisationId;
            officeWorker.PersonalId = model.PersonalID;
            officeWorker.Gender = Enum.Parse<Gender>(PersonalIDParser.GenderExtract(model.PersonalID));

            var result = await userManager.CreateAsync(officeWorker, model.Password);
            if (result.Succeeded == false)
            {
                return false;
            }
            IdentityResult roleresult = await userManager.AddToRoleAsync(officeWorker, "OfficeAdministrator");

            if (roleresult.Succeeded == false)
            {
                return false;
            }
            return true;
        } // AddOfficeAdmin

        private async Task<bool> DoesOfficeHasAnAdmin(string officeId)
        {
            var allWorkers = await this.userManager.Users.Where(x => x.OfficeId == GuidParser.GuidParse(officeId)).ToListAsync();

            foreach (var worker in allWorkers)
            {
                if (await this.userManager.IsInRoleAsync(worker, "OfficeAdministrator"))
                {
                    return true;
                }
            }

            return false;
        } // DoesOfficeHasAnAdmin
        public async Task<AddOfficeAdminViewModel> AddOfficeAdmin(string userId, string officeId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            bool doesOfficeHasAnAdmin = await this.DoesOfficeHasAnAdmin(officeId);

            if (doesOfficeHasAnAdmin == true)
            {
                throw new Exception("Office has an aministrator!");
            }

            AddOfficeAdminViewModel model = new AddOfficeAdminViewModel();
            model.OfficeId = officeId;

            return model;
        } // AddOfficeAdmin

        public async Task<bool> RemoveUserAdmin(string userId, string officeAdminId)
        {
            var user = await this.GetOfficeWorker(userId, role);
            var officeAdmin = await this.GetOfficeWorker(officeAdminId, "OfficeAdministrator");

            await this.userManager.RemoveFromRoleAsync(officeAdmin, "OfficeAdministrator");
            await this.userManager.AddToRoleAsync(officeAdmin, "OfficeWorker");
            await this.context.SaveChangesAsync();

            return true;
        } // RemoveUserAdmin
        public async Task<OfficeAdminViewModel> GetOfficeAdmin(string userId, string officeAdminId)
        {
            var user = await this.GetOfficeWorker(userId, role);
            var officeAdmin = await this.GetOfficeWorker(officeAdminId, "OfficeAdministrator");

            OfficeAdminViewModel model = new OfficeAdminViewModel();
            model.Id = officeAdmin.Id;
            model.FirstName = officeAdmin.FirstName;
            model.MiddleName = officeAdmin.MiddleName;
            model.LastName = officeAdmin.LastName;
            model.PersonalID = officeAdmin.PersonalId;
            model.Phone = officeAdmin.PhoneNumber;
            model.Email = officeAdmin.Email;

            return model;
        } // GetOfficeAdmin

        public async Task<List<OfficeInfoViewModel>> GetOffices(string userId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Offices.Where(x => x.OrganisationId == user.OrganisationId)
                                             .Include(x => x.City)
                                             .Select(x => new OfficeInfoViewModel()
                                             {
                                                 Id = x.Id.ToString(),
                                                 Name = x.Name,
                                                 Phone = x.LocalPhone
                                             })
                                             .ToListAsync();
        } // GetOffices
        public async Task<bool> AddOffice(string userId, EditOfficeViewModel model)
        {
            var user = await this.GetOfficeWorker(userId, role);
            bool isCityValid = await this.context.Cities.AnyAsync(x => x.Id == GuidParser.GuidParse(model.CityName));

            if (isCityValid == false)
            {
                return false;
            }

            Office office = new Office();
            office.Name = model.Name;
            office.OrganisationId = (Guid)user.OrganisationId;
            office.LocalPhone = model.Phone;
            office.StreetNumber = model.StreetNumber;
            office.CityId = GuidParser.GuidParse(model.CityName);
            office.Street = model.Street;

            await this.context.Offices.AddAsync(office);
            await this.context.SaveChangesAsync();

            return true;
        } // AddOffice

        public async Task<bool> EditOffice(string userId, EditOfficeViewModel model)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var office = await this.context.Offices.FirstOrDefaultAsync(x => x.Id == GuidParser.GuidParse(model.Id));
            if (office == null)
            {
                return false;
            }

            bool isCityValid = await this.context.Cities.AnyAsync(x => x.Id == GuidParser.GuidParse(model.CityName));
            if (isCityValid == false)
            {
                return false;
            }

            office.Name = model.Name;
            office.OrganisationId = (Guid)user.OrganisationId;
            office.LocalPhone = model.Phone;
            office.StreetNumber = model.StreetNumber;
            office.CityId = GuidParser.GuidParse(model.CityName);
            office.Street = model.Street;

            await this.context.SaveChangesAsync();

            return true;
        } // EditOffice

        public async Task<OfficeViewModel> GetFullOfficeDetails(string userId, string officeId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var office = await this.context.Offices.Include(x => x.Organisation)
                                .Where(x => x.Id == GuidParser.GuidParse(officeId) && user.OrganisationId == x.OrganisationId)
                                .Select(x => new OfficeViewModel
                                {
                                    Id = x.Id.ToString(),
                                    Name = x.Name,
                                    Phone = x.LocalPhone,
                                    StreetNumber = x.StreetNumber,
                                    Street = x.Street,
                                    CityName = x.City.Name
                                }).FirstOrDefaultAsync();

            var allWorkers = await this.userManager.Users.Where(x => x.OfficeId == GuidParser.GuidParse(office.Id)).ToListAsync();

            List<WorkerViewModel> workers = new List<WorkerViewModel>();
            foreach (var worker in allWorkers)
            {
                if (await this.userManager.IsInRoleAsync(worker, "OfficeAdministrator"))
                {
                    office.OfficeAdministrator = new WorkerViewModel()
                    {
                        Id = worker.Id,
                        Name = worker.FirstName,
                        MiddleName = worker.MiddleName,
                        LastName = worker.LastName
                    };
                }
                else if (await this.userManager.IsInRoleAsync(worker, "OfficeWorker"))
                {
                    workers.Add(new WorkerViewModel()
                    {
                        Id = worker.Id,
                        Name = worker.FirstName,
                        MiddleName = worker.MiddleName,
                        LastName = worker.LastName
                    });
                }
            }

            office.Workers = workers;
            return office;
        } // GetFullOfficeDetails

        public async Task<EditOfficeViewModel> GetOfficeDetails(string userId, string officeId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var office = await this.context.Offices.Include(x => x.Organisation)
                                .Where(x => x.Id == GuidParser.GuidParse(officeId) && user.OrganisationId == x.OrganisationId)
                                .Select(x => new EditOfficeViewModel
                                {
                                    Id = x.Id.ToString(),
                                    Name = x.Name,
                                    Phone = x.LocalPhone,
                                    StreetNumber = x.StreetNumber,
                                    Street = x.Street,
                                    CityName = x.City.Name
                                }).FirstOrDefaultAsync();

            office.Cities = await this.GetCities();

            return office;
        } // GetOfficeDetails
    } // OfficeOrgAdminService
}

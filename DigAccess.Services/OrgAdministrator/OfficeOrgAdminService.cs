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
            model.OfficeId = officeAdmin.OfficeId.ToString();
            return model;
        } // GetOfficeAdmin

        public async Task<bool> DeleteOffice(string userId, string officeId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var office = await this.context.Offices.Include(x => x.Organisation)
                                .FirstOrDefaultAsync(x => x.Id == GuidParser.GuidParse(officeId) && user.OrganisationId == x.OrganisationId && x.IsDeleted == false);

            if (office == null)
            {
                return false;
            }

            office.IsDeleted = true;
            await this.context.SaveChangesAsync();
            return true;
        } // DeleteOffice

        public async Task<List<OfficeInfoViewModel>> GetOfficesByName(string userId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Offices.Where(x => x.OrganisationId == user.OrganisationId && x.Name.ToLower().StartsWith(name.ToLower()) && x.IsDeleted == false)
                                             .Include(x => x.City)
                                             .Select(x => new OfficeInfoViewModel()
                                             {
                                                 Id = x.Id.ToString(),
                                                 Name = x.Name,
                                                 Phone = x.LocalPhone
                                             })
                                             .ToListAsync();
        }

        public async Task<List<OfficeInfoViewModel>> GetOffices(string userId, int page)
        {
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Offices.Where(x => x.OrganisationId == user.OrganisationId && x.IsDeleted == false)
                                             .Include(x => x.City)
                                             .Select(x => new OfficeInfoViewModel()
                                             {
                                                 Id = x.Id.ToString(),
                                                 Name = x.Name,
                                                 Phone = x.LocalPhone
                                             })
                                             .Skip((page - 1) * 8).Take(8)
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

            var office = await this.context.Offices.FirstOrDefaultAsync(x => x.Id == GuidParser.GuidParse(model.Id) && x.IsDeleted == false);
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


        public async Task<OfficeViewModel> GetFullOfficeDetails(string userId, string officeId, int page = 1)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var office = await this.context.Offices.Include(x => x.Organisation)
                                .Where(x => x.Id == GuidParser.GuidParse(officeId) && user.OrganisationId == x.OrganisationId && x.IsDeleted == false)
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

            office.Workers = workers.Skip((page - 1) * 3).Take(3).ToList();
            return office;
        } // GetFullOfficeDetails

        public async Task<int> CountWorkers(string userId, string officeId)
        {
            int count = 0;
            var user = await this.GetOfficeWorker(userId, role);

            var office = await this.context.Offices.Include(x => x.Organisation)
                                .Where(x => x.Id == GuidParser.GuidParse(officeId) && user.OrganisationId == x.OrganisationId && x.IsDeleted == false)
                                .FirstOrDefaultAsync();
            var allWorkers = await this.userManager.Users.Where(x => x.OfficeId == GuidParser.GuidParse(officeId)).ToListAsync();

            List<WorkerViewModel> workers = new List<WorkerViewModel>();
            foreach (var worker in allWorkers)
            {
                if (await this.userManager.IsInRoleAsync(worker, "OfficeWorker"))
                {
                    count++;
                }
            }

            return count;
        } // CountWorkers
        public async Task<EditOfficeViewModel> GetOfficeDetails(string userId, string officeId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var office = await this.context.Offices.Include(x => x.Organisation)
                                .Where(x => x.Id == GuidParser.GuidParse(officeId) && user.OrganisationId == x.OrganisationId && x.IsDeleted == false)
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

        public async Task<int> CountOffices(string userId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            return await this.context.Offices.Where(x => x.OrganisationId == user.OrganisationId && x.IsDeleted == false).CountAsync();
        }
    } // OfficeOrgAdminService
}

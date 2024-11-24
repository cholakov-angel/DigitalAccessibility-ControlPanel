using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Models.OfficeAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services.OfficeAdministrator
{
    public class OfficeDetailsService : BaseService, IOfficeDetailsService
    {
        private string role = "OfficeAdministrator";
        public OfficeDetailsService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // OfficeDetailsService

        public async Task<List<CityViewModel>> GetCities()
        {
            return await this.context.Cities.Select(x => new CityViewModel
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToListAsync();
        } // GetCities

        public async Task<OfficeViewModel> GetOfficeDetails(string userId)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var office = await this.context.Offices.Include(x => x.Organisation)
                                .Where(x => x.Id == user.OfficeId)
                                .Select(x => new OfficeViewModel 
                                { 
                                    Id = x.Id.ToString(),
                                    Name = x.Name,
                                    Organisation = x.Organisation.Name,
                                    Phone = x.LocalPhone,
                                    StreetNumber = x.StreetNumber,
                                    Street = x.Street,
                                    CityName = x.City.Name
                                }).FirstOrDefaultAsync();

            office.Cities = await this.GetCities();

            return office;
        } // GetOfficeDetails

        public async Task<bool> UpdateOffice(string userId, OfficeViewModel office)
        {
            var user = await this.GetOfficeWorker(userId, role);

            var officeObj = await this.context.Offices.Include(x => x.Organisation)
                                .Where(x => x.Id == user.OfficeId)
                                .FirstOrDefaultAsync();

            if (officeObj == null)
            {
                return false;
            }

            if (officeObj.Id != GuidParser.GuidParse(office.Id))
            {
                return false;
            }

            officeObj.Name = office.Name;
            officeObj.LocalPhone = office.Phone;
            officeObj.CityId = GuidParser.GuidParse(office.CityName);
            officeObj.Street = office.Street;
            officeObj.StreetNumber = office.StreetNumber;

            await this.context.SaveChangesAsync();
            return true;
        } // UpdateOffice
    } // OfficeDetailsService
}

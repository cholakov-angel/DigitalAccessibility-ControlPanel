using DigAccess.Common;
using DigAccess.Data.Entities.Blind;
using DigAccess.Interfaces;
using DigAccess.Models.BlindUser;
using DigAccess.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services
{
    public class BlindUserService : BaseService, IBlindUserService
    {
        public BlindUserService(DigAccessDbContext context) : base(context)
        {
        } // BlindUserService

        public void SetContext(DigAccessDbContext context)
        {
            this.context = context;
        } // SetContext

        public async Task<List<CityViewModel>> GetCities()
        {
            var cities = await context.Cities.Select(x => new CityViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return cities;
        } // GetCities

        public async Task<BlindUserDetailsViewModel> GetUserDetails(string id)
        {
            var result = Guid.TryParse(id, out Guid resultGuid);

            if (result == false)
            {
                throw new Exception("Invalid id model!");
            }
            var user = await context.BlindUsers.FindAsync(resultGuid);

            if (user == null)
            {
                throw new Exception("Invalid id!");
            }

            var city = await context.Cities.FindAsync(user.CityId);

            BlindUserDetailsViewModel model = new BlindUserDetailsViewModel();
            model.FirstName = user.FirstName;
            model.MiddleName = user.MiddleName;
            model.LastName = user.LastName;
            model.PersonalId = user.PersonalId;
            model.BirthDate = user.Birthdate.Value.ToString(Constants.DateTimeFormat);
            model.City = city.Name;

            return model;
        } // GetUserDetails

        public async Task<List<BlindUserViewModel>> GetAllModels(string userId)
        {
            return await context.BlindUsers
                .Where(x => x.AdministratorId == userId)
                .Select(x => new BlindUserViewModel()
                {
                    Id = x.Id,
                    City = x.City.Name,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Street = x.Street,
                    StreetNumber = x.StreetNumber,
                    TELKID = x.TELKNumber,
                    PersonalId = x.PersonalId,
                    BirthDate = x.Birthdate.Value.ToString(Constants.DateTimeFormat)
                }).ToListAsync();
        } // GetAllModels

        public async Task<bool> Add(BlindUserViewModel model, string userId)
        {
            BlindUser user = new BlindUser();
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.AdministratorId = userId;

            bool result = Guid.TryParse(model.City, out Guid cityId);
            if (result == false)
            {
                throw new Exception("Invalid city id!");
            }

            if (context.Cities.Any(x=> x.Id == cityId) == false)
            {
                throw new Exception("Invalid city id!");
            }

            DateTime date = PersonalIDParser.BirthdateExtract(model.PersonalId);

            if (date == default)
            {
                return false!;
            }

            user.Birthdate = date;
            user.CityId = cityId;
            user.Street = model.Street;
            user.StreetNumber = model.StreetNumber;

            await context.BlindUsers.AddAsync(user);
            await context.SaveChangesAsync();

            return true;
        } // Add
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigAccess.Common;
using DigAccess.Data.Entities.Blind;
using DigAccess.Interfaces;
using DigAccess.Models.BlindUser;
using DigAccess.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DigAccess.Services
{
    public class BlindUserService : BaseService, IBlindUserService
    {
        public BlindUserService(DigAccessDbContext context) : base(context)
        {
        }

        public void SetContext(DigAccessDbContext context)
        {
            this.context = context;
        }

        public async Task<List<CityViewModel>> GetCities()
        {
            return await context.Cities.Select(x => new CityViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<BlindUserDetailsViewModel> GetUserDetails(string id)
        {
            var result = Guid.TryParse(id, out Guid resultGuid);

            if (result == null)
            {
                throw new Exception("Invalid id model!");
                return null;
            }
            var user = await context.BlindUsers.FindAsync(resultGuid);

            if (user == null)
            {
                throw new Exception("Invalid id!");
                return null;
            }

            var city = await context.Cities.FindAsync(user.CityId);

            BlindUserDetailsViewModel model = new BlindUserDetailsViewModel();
            model.FirstName = user.FirstName;
            model.MiddleName = user.MiddleName;
            model.LastName = user.LastName;
            model.PersonalId = user.PersonalId;
            model.BirthDate = user.Birthdate.Value.ToString("dd.MM.yyyy");
            model.City = city.Name;

            return model;
        }

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
                    BirthDate = x.Birthdate.Value.ToString("dd.MM.yyyy")
                }).ToListAsync();
        }

        public async Task Add(BlindUserViewModel model, string userId)
        {
            BlindUser user = new BlindUser();
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.AdministratorId = userId;

            bool result = Guid.TryParse(model.City, out Guid cityId);
            if (!result)
            {
                throw new Exception("Invalid data!");
            }

            user.Birthdate = PersonalIDParser.BirthdateExtract(model.PersonalId);
            user.City = context.Cities.Find(cityId);
            user.Street = model.Street;
            user.StreetNumber = model.StreetNumber;
            await context.BlindUsers.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}

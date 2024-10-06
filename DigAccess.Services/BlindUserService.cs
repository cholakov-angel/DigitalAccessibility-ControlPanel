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

namespace DigAccess.Services
{
    public class BlindUserService : IBlindUser
    {
        private DigAccessDbContext context;
        public BlindUserService(DigAccessDbContext context)
        {
            this.context = context;
        }

        public void SetContext(DigAccessDbContext context)
        {
            this.context = context;
        }

        public Task<List<CityViewModel>> GetCities()
        {
            return context.Cities.Select(x => new CityViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public Task<List<BlindUserViewModel>> GetAllModels(string userId)
        {
            return context.BlindUsers
                .Where(x => x.AdministratorId == userId)
                .Select(x => new BlindUserViewModel()
                {
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
            context.BlindUsers.Add(user);
            context.SaveChangesAsync();
        }

        public BlindUserViewModel FindById(string id)
        {
            throw new NotImplementedException();
        }
    }
}

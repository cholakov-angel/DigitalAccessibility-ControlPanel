﻿using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Blind;
using DigAccess.Interfaces;
using DigAccess.Models.UserAdministrator.BlindUser;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services
{
    public class BlindUserService : BaseService, IBlindUserService
    {
        public BlindUserService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // BlindUserService

        public async Task<BlindUserViewModel> EditUserGet(string id, string userId)
        {
            var resultGuid = GuidParser.GuidParse(id);
            var user = await context.BlindUsers.FirstOrDefaultAsync(x => x.Id == resultGuid && x.IsDeleted == false);

            if (user == null)
            {
                throw new Exception("Invalid id!");
            }

            if (user.AdministratorId != userId)
            {
                throw new Exception("Invalid administator!");
            }

            var city = await context.Cities.FindAsync(user.CityId);

            BlindUserViewModel model = new BlindUserViewModel();
            model.Id = user.Id.ToString();
            model.FirstName = user.FirstName;
            model.MiddleName = user.MiddleName;
            model.LastName = user.LastName;
            model.PersonalId = user.PersonalId;
            model.BirthDate = user.Birthdate.Value.ToString(Constants.DateTimeFormat);
            model.City = city.Name;
            model.TELKID = user.TELKNumber;
            model.CityNames = await this.GetCities();
            model.StreetNumber = user.StreetNumber;
            model.Street = user.Street;
            return model;
        } // EditUserGet

        public async Task<bool> EditBlindUser(string userId, BlindUserViewModel model)
        {
            BlindUser user =
                await this.context.BlindUsers.FirstOrDefaultAsync(x => x.Id == GuidParser.GuidParse(model.Id) && x.AdministratorId == userId);

            if (user == null)
            {
                return false;
            }

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.AdministratorId = userId;

            var cityId = GuidParser.GuidParse(model.City);

            if (context.Cities.Any(x => x.Id == cityId) == false)
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
            user.PersonalId = model.PersonalId;
            user.TELKNumber = model.TELKID;

            await context.SaveChangesAsync();

            return true;
        } // EditBlindUser

        public async Task<List<CityViewModel>> GetCities()
        {
            var cities = await context.Cities.Select(x => new CityViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return cities;
        } // GetCities

        public async Task<BlindUserDetailsViewModel> GetUserDetails(string id, string userId)
        {
            var resultGuid = GuidParser.GuidParse(id);
            var user = await context.BlindUsers.FirstOrDefaultAsync(x=> x.Id == resultGuid && x.IsDeleted == false);

            if (user == null)
            {
                throw new Exception("Invalid id!");
            }

            if (user.AdministratorId != userId)
            {
                throw new Exception("Invalid administator!");
            }

            var city = await context.Cities.FindAsync(user.CityId);

            BlindUserDetailsViewModel model = new BlindUserDetailsViewModel();
            model.FirstName = user.FirstName;
            model.MiddleName = user.MiddleName;
            model.LastName = user.LastName;
            model.PersonalId = user.PersonalId;
            model.TELKID = user.TELKNumber;
            model.StreetNumber = user.StreetNumber;
            model.Street = user.Street;
            model.BirthDate = user.Birthdate.Value.ToString(Constants.DateTimeFormat);
            model.City = city.Name;
            model.Gender = user.Gender.ToString();
            return model;
        } // GetUserDetails

        public async Task<List<BlindUserViewModel>> GetAllModels(string userId)
        {
            return await context.BlindUsers
                .Where(x => x.AdministratorId == userId && x.IsDeleted == false)
                .Select(x => new BlindUserViewModel() 
                {
                    Id = x.Id.ToString(),
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

        public async Task<List<BlindUserViewModel>> GetModel(string userId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return await this.GetAllModels(userId);
            }
           
            return await context.BlindUsers
                .Where(x => x.AdministratorId == userId && (x.FirstName + " " + x.LastName).ToLower().StartsWith(name.ToLower()) && x.IsDeleted == false)
                .Select(x => new BlindUserViewModel()
                {
                    Id = x.Id.ToString(),
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
        } // GetModel
        public async Task<bool> Add(BlindUserViewModel model, string userId)
        {
            BlindUser user = new BlindUser();
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.AdministratorId = userId;

            var cityId = GuidParser.GuidParse(model.City);

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
            user.PersonalId = model.PersonalId;
            user.TELKNumber = model.TELKID;

            await context.BlindUsers.AddAsync(user);
            await context.SaveChangesAsync();

            return true;
        } // Add

        public async Task<BlindUserViewPageModel> GetUserInformation(DateTime currentDate,string id, string userId)
        {
            var resultId = GuidParser.GuidParse(id);

            if (await context.BlindUsers.AnyAsync(x=> x.Id == resultId && x.AdministratorId == userId) == false)
            {
                throw new Exception("Invalid user!");
            }

            var user = await context.BlindUsers
                .Where(x => x.Id == resultId && x.IsDeleted == false)
                .Select(x => new BlindUserViewPageModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName!,
                    MiddleName = x.MiddleName!,
                    LastName = x.LastName!,
                    LicenseNumber = x.BlindUserLicences.Where(y=> y.IsDeleted == false).Count(y=> y.BlindUserId == x.Id),
                    Age = currentDate.Year - x.Birthdate!.Value.Year,
                    Gender = x.Gender.ToString()
                })
                .FirstOrDefaultAsync();
            
            return user;
        } // GetUserInformation
    }
}

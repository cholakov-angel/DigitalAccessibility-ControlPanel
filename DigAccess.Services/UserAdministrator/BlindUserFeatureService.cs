using DigAccess.Common;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Feature;
using DigAccess.Models.UserAdministrator.BlindUser;
using DigAccess.Services.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Services.UserAdministrator
{
    public class BlindUserFeatureService : BaseService, IBlindUserFeatureService
    {
        public BlindUserFeatureService(DigAccessDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        } // BlindUserFeatureService

        public async Task<List<FeatureViewModel>> GetFeatures(string userId, string blindUserId)
        {
            bool isUserValid = await this.context.BlindUsers.AnyAsync(x => x.Id == GuidParser.GuidParse(blindUserId)
                                                                           && x.AdministratorId == userId);

            if (!isUserValid)
            {
                throw new Exception("Invalid User!");
            }
            return await this.context.BlindUsersFeatures
                .Where(x => x.BlindUserId == GuidParser.GuidParse(blindUserId))
                .Include(x=> x.Feature)
                .Select(x => new FeatureViewModel()
                {
                    FeatureId = x.FeatureId.ToString(),
                    FeatureName = x.Feature.Name
                }).ToListAsync();
        } // GetFeatures

        public async Task<bool> Delete(string userId, string featureId)
        {
            var feature = await this.context.BlindUsersFeatures.Include(x => x.BlindUser)
                .FirstOrDefaultAsync(x => x.FeatureId == GuidParser.GuidParse(featureId) &&
                                          x.BlindUser.AdministratorId == userId);

            if (feature == null)
            {
                return false;
            }

            this.context.BlindUsersFeatures.Remove(feature);
            await this.context.SaveChangesAsync();
            return true;
        } // Delete

        public async Task<List<FeatureViewModel>> GetAvailableFeatures(string userId, string blindUserId)
        {
            bool isUserValid = await this.context.BlindUsers.AnyAsync(x => x.Id == GuidParser.GuidParse(blindUserId) 
                                                                           && x.AdministratorId == userId);

            if (!isUserValid)
            {
                throw new Exception("Invalid User!");
            }
            return await this.context.Features
                .Where(x => context.BlindUsersFeatures.Any(y => y.FeatureId == x.Id) == false)
                .Select(x => new FeatureViewModel()
                {
                    FeatureId = x.Id.ToString(),
                    FeatureName = x.Name
                }).ToListAsync();
        } // GetAvailableFeatures

        public async Task<BlindUserFeaturesViewModel> AddFeature(string blindUserId, string userId, string featureId)
        {
            bool isUserValid = await this.context.BlindUsers.AnyAsync(x => x.Id == GuidParser.GuidParse(blindUserId)
                                                                           && x.AdministratorId == userId);

            if (!isUserValid)
            {
                throw new Exception("Invalid User!");
            }

            var feature = await this.context.Features.FirstOrDefaultAsync(x => x.Id == GuidParser.GuidParse(featureId));
            if (feature == null)
            {
                throw new Exception("Invalid feature!");
            }
            if (await this.context.BlindUsersFeatures.AnyAsync(x =>
                    x.BlindUserId == GuidParser.GuidParse(blindUserId) &&
                    x.FeatureId == GuidParser.GuidParse(featureId)))
            {
                throw new Exception("Feature already added!");
            }

            BlindUserFeaturesViewModel model = new BlindUserFeaturesViewModel();
            model.FeatureId = featureId;
            model.FeatureName = feature.Name;
            model.BlindUserId = blindUserId;

            return model;
        }

        public async Task<bool> AddFeatureConfirm(BlindUserFeaturesViewModel model, string userId)
        {
            bool isUserValid = await this.context.BlindUsers.AnyAsync(x => x.Id == GuidParser.GuidParse(model.BlindUserId)
                                                                           && x.AdministratorId == userId);

            if (!isUserValid)
            {
                throw new Exception("Invalid User!");
            }

            if (await this.context.BlindUsersFeatures.AnyAsync(x =>
                    x.BlindUserId == GuidParser.GuidParse(model.BlindUserId) &&
                    x.FeatureId == GuidParser.GuidParse(model.FeatureId)))
            {
                return false;
            }

            BlindUserFeature blindUserFeature = new BlindUserFeature();
            blindUserFeature.BlindUserId = GuidParser.GuidParse(model.BlindUserId);
            blindUserFeature.FeatureId = GuidParser.GuidParse(model.FeatureId);
            blindUserFeature.LicenceKey = model.LicenceKey;

            await this.context.BlindUsersFeatures.AddAsync(blindUserFeature);
            await this.context.SaveChangesAsync();

            return true;
        }
    }
}

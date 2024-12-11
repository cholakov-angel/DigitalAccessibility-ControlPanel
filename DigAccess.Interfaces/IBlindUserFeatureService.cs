using DigAccess.Models.UserAdministrator.BlindUser;
using DigAccess.Interfaces;

namespace DigAccess.Services.Interfaces
{
    public interface IBlindUserFeatureService : IService
    {
        public Task<List<FeatureViewModel>> GetAvailableFeatures(string userId, string blindUserId);
        public Task<BlindUserFeaturesViewModel> AddFeature(string blindUserId, string userId, string featureId);
        public Task<bool> AddFeatureConfirm(BlindUserFeaturesViewModel model, string userId);
        public Task<List<FeatureViewModel>> GetFeatures(string userId, string blindUserId);
        public Task<bool> Delete(string userId, string featureId);
    } // IBlindUserFeatureService
}

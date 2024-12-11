using DigAccess.Data.Entities;
using DigAccess.Interfaces;
using DigAccess.Models.UserAdministrator.BlindUser;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]
    public class FeatureController : BaseController
    {
        private readonly IBlindUserFeatureService service;
        private readonly UserManager<ApplicationUser> userManager;

        public FeatureController(UserManager<ApplicationUser> userManager, IBlindUserFeatureService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // FeatureController

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetFeatures(userId, id);
            ViewData["BlindUserId"] = id;
            return View(model);
        } // Index

        [HttpGet]
        public async Task<IActionResult> AddFeature(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetAvailableFeatures(userId, id);
            ViewBag.BlindUserId = id;
            return View(model);
        } // AddFeature

        [HttpGet]
        public async Task<IActionResult> Delete(string id, string blindUserId)
        {
            var userId = this.GetUserId();
            bool result = await this.service.Delete(userId, id);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index", new { Id = blindUserId });
        } // Delete

        [HttpGet]
        public async Task<IActionResult> Add(string id, string blindUserId)
        {
            var userId = this.GetUserId();
            var result = await this.service.AddFeature(blindUserId, userId, id);

            return View(result);
        } // Add

        [HttpPost]
        public async Task<IActionResult> Add(BlindUserFeaturesViewModel model)
        {
            var userId = this.GetUserId();
            bool result = await this.service.AddFeatureConfirm(model, userId);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("UserPage", "BlindUser", new { Id = model.BlindUserId });
        } // Add
    } // FeatureController
}

using DigAccess.Data.Entities;
using DigAccess.Interfaces;
using DigAccess.Models.License;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]
    public class LicenseController : Controller
    {
        private readonly ILicenseService service;
        private readonly UserManager<ApplicationUser> userManager;

        public LicenseController(UserManager<ApplicationUser> userManager, ILicenseService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // LicenceController

        public async Task<IActionResult> UserLicense(string id)
        {
            string? userId = this.GetUserId();

            var licences = await service.GetLicenses(id, userId);

            return View(licences);
        } // UserLicences

        public async Task<IActionResult> AddLicense(string id)
        {
            string? userId = this.GetUserId();

            var model = await service.GenerateLicense(id, userId, new Random(), DateTime.Now);

            return View(model);
        } // AddLicence

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            string? userId = this.GetUserId();

            var model = await service.GetLicense(userId, id);
            return View(model);
        } // Details

        public async Task<IActionResult> Delete(string id)
        {
            string? userId = this.GetUserId();

            var model = await service.DeleteLicenseConfirm(userId, id);

            if (model == null)
            {
                throw new ArgumentException("Invalid licence!");
            }

            return View(model);
        } // Delete

        public async Task<IActionResult> DeleteConfirm(string id)
        {
            string? userId = this.GetUserId();

            string blindUserId = await service.DeleteLicense(userId, id);

            return RedirectToAction("UserLicense", new { Id = blindUserId });
        } // DeleteConfirm

        private string? GetUserId()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            return userId;
        } // GetUserId
    } // LicenseController
}

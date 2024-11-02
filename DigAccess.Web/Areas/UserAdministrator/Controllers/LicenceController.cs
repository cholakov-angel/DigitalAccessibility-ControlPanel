using DigAccess.Data.Entities;
using DigAccess.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]
    public class LicenceController : Controller
    {
        private readonly ILicenceService service;
        private readonly UserManager<ApplicationUser> userManager;

        public LicenceController(UserManager<ApplicationUser> userManager, ILicenceService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // LicenceController

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            var users = await service.GetAll(userId);
            return View(users);
        } // Index

        public async Task<IActionResult> UserLicence(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var licences = await service.GetLicences(id, userId);

            return View(licences);
        } // UserLicences

        public async Task<IActionResult> AddLicence(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            await service.GenerateLicence(id, userId, new Random(), DateTime.Now);

            return RedirectToAction("Index");
        } // AddLicence

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            var model = await service.GetLicence(userId, id);
            return View(model);
        } // Details
    }
}

using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Blind;
using DigAccess.Interfaces;
using DigAccess.Models.EmailSettings;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]
    public class EmailSettingsController : Controller
    {
        private readonly IEmailSettingsService service;
        private readonly UserManager<ApplicationUser> userManager;

        public EmailSettingsController(IEmailSettingsService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            var model = await service.GetEmail(userId, id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            bool hasEmail = await service.HasEmail(userId, id);
            if (hasEmail == true)
            {
                throw new ArgumentException("The user has already email!");
            }
            var model = await service.Add(userId, id);

            return View("AddEdit",model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmailAddViewModel model)
        {
            model.EmailProviders = await service.GetProvider();

            if (!ModelState.IsValid)
            {
                return View("AddEdit", model);
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            bool result = await service.AddEdit(model, userId);

            return RedirectToAction("Index", new {Id = model.BlindUserId });
        }

        public async Task<IActionResult> Edit(string id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            var model = await service.Edit(id, userId);

            return View("AddEdit", model);
        }
    }
}

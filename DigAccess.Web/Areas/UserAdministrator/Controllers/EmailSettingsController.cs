using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Blind;
using DigAccess.Interfaces;
using DigAccess.Models.UserAdministrator.EmailSettings;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]
    public class EmailSettingsController : BaseController
    {
        private readonly IEmailSettingsService service;
        private readonly UserManager<ApplicationUser> userManager;

        public EmailSettingsController(IEmailSettingsService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        } // EmailSettingsController

        public async Task<IActionResult> Index(string id)
        {
            string? userId = this.GetUserId();

            var model = await service.GetEmail(userId, id);

            return View(model);
        } // Index

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            string? userId = this.GetUserId();

            bool hasEmail = await service.HasEmail(userId, id);
            if (hasEmail == true)
            {
                throw new ArgumentException("The user has already email!");
            }
            var model = await service.Add(userId, id);

            return View("AddEdit",model);
        } // Add

        [HttpPost]
        public async Task<IActionResult> Add(EmailAddViewModel model)
        {
            model.EmailProviders = await service.GetProvider();

            if (!ModelState.IsValid)
            {
                return View("AddEdit", model);
            }

            string? userId = this.GetUserId();

            bool result = await service.AddEdit(model, userId);

            return RedirectToAction("Index", new {Id = model.BlindUserId });
        } // Add

        public async Task<IActionResult> Edit(string id)
        {
            string? userId = this.GetUserId();

            var model = await service.Edit(id, userId);

            return View("AddEdit", model);
        } // Edit
    } // EmailSettingsController
}

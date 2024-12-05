using DigAccess.Data.Entities;
using DigAccess.Models.OfficeAdministrator;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.OfficeAdministrator.Controllers
{
    [Area("OfficeAdministrator")]
    [Authorize(Roles = "OfficeAdministrator")]
    public class OfficeDetailsController : BaseController
    {
        private readonly IOfficeDetailsService service;
        private readonly UserManager<ApplicationUser> userManager;

        public OfficeDetailsController(UserManager<ApplicationUser> userManager, IOfficeDetailsService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // OfficeDetailsController

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOfficeDetails(userId);

            return View(model);
        } // Index

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOfficeDetails(userId);
            return View(model);
        } // Edit

        [HttpPost]
        public async Task<IActionResult> Edit(OfficeViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                model.Cities = await this.service.GetCities();
                return View(model);
            }

            var userId = this.GetUserId();

            bool result = await this.service.UpdateOffice(userId, model);

            if (result == false)
            {
                throw new ArgumentException("Error occurred!");
            }

            return RedirectToAction("Index");
        } // Edit
    } // OfficeDetailsController
}

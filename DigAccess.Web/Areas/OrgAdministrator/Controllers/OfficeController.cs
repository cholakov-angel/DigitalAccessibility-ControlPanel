using DigAccess.Data.Entities;
using DigAccess.Models.OrgAdministrator;
using DigAccess.Services.Interfaces;
using DigAccess.Services.OrgAdministrator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.OrgAdministrator.Controllers
{
    [Area("OrgAdministrator")]
    [Authorize(Roles = "OrgAdministrator")]
    public class OfficeController : BaseController
    {
        private readonly IOfficeOrgAdminService service;
        private readonly UserManager<ApplicationUser> userManager;

        public OfficeController(UserManager<ApplicationUser> userManager, IOfficeOrgAdminService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // OfficeController

        [HttpGet]
        public async Task<IActionResult> SearchByName(string name)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOfficesByName(userId, name);

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View("Index", model);
        } // SearchByName

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            EditOfficeViewModel model = new EditOfficeViewModel();
            model.Cities = await this.service.GetCities();

            return View(model);
        } // Add

        [HttpPost]
        public async Task<IActionResult> Add(EditOfficeViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                model.Cities = await this.service.GetCities();
                return View(model);
            }
            var userId = this.GetUserId();

            bool result = await this.service.AddOffice(userId, model);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index");
        } // Add

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOffices(userId, page);

            int totalItems = await this.service.CountOffices(userId);
            int totalPages = (int)Math.Ceiling(totalItems / (double)8);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(model);
        } // Index

        [HttpGet]
        public async Task<IActionResult> Details(string id, int page = 1)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetFullOfficeDetails(userId, id, page);

            int totalItems = await this.service.CountWorkers(userId, id);
            int totalPages = (int)Math.Ceiling(totalItems / (double)3);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(model);
        } // Details

        [HttpGet]
        public async Task<IActionResult> OfficeAdminDetails(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOfficeAdmin(userId, id);

            return View(model);
        } // OfficeAdminDetails

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOfficeDetails(userId, id);

            return View(model);
        } // Edit

        [HttpPost]
        public async Task<IActionResult> Edit(EditOfficeViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                model.Cities = await this.service.GetCities();
                return View(model);
            }
            var userId = this.GetUserId();

            bool result = await this.service.EditOffice(userId, model);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Details", "Office", new { Id = model.Id });
        } // Edit

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.GetUserId();
            
            bool result = await this.service.DeleteOffice(userId, id);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index");
        } // Delete
    } // OfficeController
}

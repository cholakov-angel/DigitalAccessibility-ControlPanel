using DigAccess.Data.Entities;
using DigAccess.Models.OrgAdministrator;
using DigAccess.Services.Interfaces;
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

        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOffices(userId);
            return View(model);
        } // Index

        public async Task<IActionResult> Details(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetFullOfficeDetails(userId, id);

            return View(model);
        } // Details

        public async Task<IActionResult> OfficeAdminDetails(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOfficeAdmin(userId, id);

            return View(model);
        } // OfficeAdminDetails

        public async Task<IActionResult> RemoveUserAdmin(string id)
        {
            var userId = this.GetUserId();

            bool result = await this.service.RemoveUserAdmin(userId, id);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index");
        } // RemoveUserAdmin

        public async Task<IActionResult> AddUserAsAdmin(string id)
        {
            var userId = this.GetUserId();

            bool result = await this.service.AddUserAsAdmin(userId, id);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index");
        } // AddUserAsAdmin

        [HttpPost]
        public async Task<IActionResult> Add(AddOfficeAdminViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View("OfficeAdminAdd", model);
            }

            var userId = this.GetUserId();

            bool result = await this.service.AddOfficeAdmin(userId, model);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index");
        } // Add

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.AddOfficeAdmin(userId, id);

            return View("OfficeAdminAdd", model);
        } // Add

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
    } // OfficeController
}

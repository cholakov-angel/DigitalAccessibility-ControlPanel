using DigAccess.Data.Entities;
using DigAccess.Models.Admin;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrganisationsController : BaseController
    {
        private readonly IAdminOrgService service;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganisationsController(UserManager<ApplicationUser> userManager, IAdminOrgService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // OrganisationsController

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOrganisations(userId, page);

            int totalItems = await this.service.CountOrganisations(userId);
            int totalPages = (int)Math.Ceiling(totalItems / (double)8);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(model);
        } // Index

        [HttpPost]
        public async Task<IActionResult> GetOrganisationsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return RedirectToAction("Index");
            }
            var userId = this.GetUserId();

            var model = await this.service.GetOrganisationsByName(userId, name);

            return View("Index", model);
        } // GetOrganisationsByName

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOrganisation(userId, id);

            return View(model);
        } // Details

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.EditOrganisation(userId, id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrganisationEditViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model); 
            }

            var userId = this.GetUserId();
            bool result = await this.service.EditOrganisation(userId, model);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Details", new { Id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> ChangeAdmin(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOrgWorkers(userId, id);

            return View(model);
        } // ChangeAdmin

        [HttpGet]
        public async Task<IActionResult> ChangeAdminSelectedUser(string officeId, string id)
        {
            var userId = this.GetUserId();

            bool result = await this.service.ChangeAdministrator(userId, id);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index");
        } // ChangeAdminSelectedUser

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        } // Add

        [HttpPost]
        public async Task<IActionResult> Add(AddOrgViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var userId = this.GetUserId();

            var result = await this.service.AddOrganisation(userId, model);
            
            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index");
        } // Add
    } // OrganisationsController
}

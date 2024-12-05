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
    public class OrganisationController : BaseController
    {
        private readonly IOrganisationOrgAdminService service;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganisationController(UserManager<ApplicationUser> userManager, IOrganisationOrgAdminService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // OrgController

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOrganisationDetails(userId);
            return View(model);
        } // Index

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = this.GetUserId();

            var model = await this.service.GetOrganisationDetails(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrganisationViewModel model)
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

            return RedirectToAction("Index");
        } // Edit
    } // OrgController
}

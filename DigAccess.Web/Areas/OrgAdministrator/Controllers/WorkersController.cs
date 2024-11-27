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
    public class WorkersController : BaseController
    {
        private readonly IUsersOrgAdminService service;
        private readonly UserManager<ApplicationUser> userManager;

        public WorkersController(UserManager<ApplicationUser> userManager, IUsersOrgAdminService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // WorkersController

        public async Task<IActionResult> RemoveUserAdmin(string id)
        {
            var userId = this.GetUserId();

            bool result = await this.service.RemoveUserAdmin(userId, id);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index", "Office");
        } // RemoveUserAdmin

        public async Task<IActionResult> AddUserAsAdmin(string id)
        {
            var userId = this.GetUserId();

            bool result = await this.service.AddUserAsAdmin(userId, id);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index", "Office");
        } // AddUserAsAdmin

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AddOfficeAdminViewModel model)
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

            return RedirectToAction("Index", "Office");
        } // Add

        [HttpGet]
        public async Task<IActionResult> AddAdmin(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.AddOfficeAdmin(userId, id);

            return View("OfficeAdminAdd", model);
        } // Add

    } // WorkersController
}

using DigAccess.Data.Entities;
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
    } // OrganisationsController
}

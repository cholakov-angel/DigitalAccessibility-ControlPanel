using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Models.WaitingApproval;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.WaitingApproval.Controllers
{
    [Area("WaitingApproval")]
    [Authorize(Roles = "WaitingApproval")]
    public class HomeController : Controller
    {
        private readonly IWaitingApprovalService service;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IWaitingApprovalService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        } // HomeController
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            if (user.OfficeId != null)
            {
                return RedirectToAction("Waiting");
            }
            return View();
        } // Index

        public async Task<IActionResult> OrganisationSelection()
        {
            OrganisationSelectViewModel model = new OrganisationSelectViewModel();
            model.Organisations = await  this.service.GetOrganisations();
            return View(model);
        } // OrganisationSelection

        public async Task<IActionResult> OfficeSelection(OrganisationSelectViewModel organisationModel)
        {
            if (ModelState.IsValid == false)
            {
                throw new ArgumentException("Invlid model!");
            }
            var model = await this.service.OfficeSelect(organisationModel.OrganisationId);

            if (model == null)
            {
                throw new ArgumentException("Invalid operation!");
            }
            return View(model);
        } // OfficeSelection

        public async Task<IActionResult> Waiting()
        {
            return View();
        } // Waiting
        public async Task<IActionResult> SetOfficeForUser(WaitingApprovalViewModel model)
        {
            model.Offices = await this.service.GetOffices(model.OrganisationId);
            if (ModelState.IsValid == false)
            {
                return View("OfficeSelection",model);
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            bool result = await this.service.SetOrganisationForUser(userId, model);
            if (result == false)
            {
                throw new ArgumentException("Invalid operation!");
            }
            return RedirectToAction("Waiting");
        }
    } // HomeController
}

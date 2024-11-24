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
    public class WorkersController : BaseController
    {
        private readonly IWorkerOfficeAdminService service;
        private readonly UserManager<ApplicationUser> userManager;

        public WorkersController(UserManager<ApplicationUser> userManager, IWorkerOfficeAdminService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // WorkersController

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = this.GetUserId();
            var model = await this.service.GetWorkers(userId, page);


            int totalItems = await this.service.CountUsers(userId);
            int totalPages = (int)Math.Ceiling(totalItems / (double)8);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(model);
        } // Index

        public async Task<IActionResult> Details(string id)
        {
            var userId = this.GetUserId();
            
            var model = await this.service.WorkerDetails(userId, id);

            return View(model);
        } // Details

        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.GetUserId();

            bool result = await this.service.DeleteUser(userId, id);
            
            if (result == false)
            {
                throw new ArgumentException("Error occurred!");
            }

            return RedirectToAction("Index");
        } // Delete

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        } // Add

        [HttpPost]
        public async Task<IActionResult> Add(AddWorkerViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var userId = this.GetUserId();

            bool result = await this.service.AddOfficeWorker(userId, model);

            if (result == false)
            {
                throw new Exception("Error occurred!");
            }

            return RedirectToAction("Index");
        }
    } // WorkersController
}

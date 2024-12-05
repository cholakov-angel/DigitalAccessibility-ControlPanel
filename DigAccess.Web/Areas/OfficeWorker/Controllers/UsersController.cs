using DigAccess.Data.Entities;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.OfficeWorker.Controllers
{
    [Area("OfficeWorker")]
    [Authorize(Roles = "OfficeWorker")]
    public class UsersController : BaseController
    {
        private readonly IOfficeWorkerService service;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager, IOfficeWorkerService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // UsersController

        [HttpGet]
        public async Task<IActionResult> GetUsersByName(string name, int page = 1)
        {
            string? userId = this.GetUserId();

            var model = await this.service.GetUsersByName(userId, name, page);

            int totalItems = await this.service.CountUsers(userId);
            int totalPages = (int)Math.Ceiling(totalItems / (double)8);

            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
            }
            return View("Index", model);
        } // GetUsersByName

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            string? userId = this.GetUserId();

            var model = await this.service.GetUsers(userId, page);

            int totalItems = await this.service.CountUsers(userId);
            int totalPages = (int)Math.Ceiling(totalItems / (double)8);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(model);
        } // Index

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            string? userId = this.GetUserId();

            var model = await this.service.ApproveUserForDelete(userId, id);

            return View(model);
        } // Delete

        [HttpGet]
        public async Task<IActionResult> Details(string id, int page = 1)
        {
            string? userId = this.GetUserId();

            var model = await this.service.GetUserDetails(userId, id, page);
            int totalItems = await this.service.CountUserBlindUsers(userId, id);
            int totalPages = (int)Math.Ceiling(totalItems / (double)3);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(model);
        } // Details

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            string? userId = this.GetUserId();
            
            bool result = await this.service.DeleteUser(userId, id);

            if (result == false)
            {
                throw new ArgumentException("Invalid user!");
            }

            return RedirectToAction("Index");
        } // ConfirmDelete
    } // UsersController
}

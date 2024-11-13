using DigAccess.Data.Entities;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.OfficeWorker.Controllers
{
    [Area("OfficeWorker")]
    [Authorize(Roles = "OfficeWorker")]
    public class ApproveUsersController : Controller
    {
        private readonly IOfficeWorkerService service;
        private readonly UserManager<ApplicationUser> userManager;

        public ApproveUsersController(UserManager<ApplicationUser> userManager, IOfficeWorkerService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // ApproveUsersController

        public async Task<IActionResult> Index()
        {
            string? userId = this.GetUserId();

            var model = await this.service.GetWaitingUsers(userId);
            return View(model);
        } // Index

        public async Task<IActionResult> ApproveUser(string id)
        {
            string? userId = this.GetUserId();

            await this.service.ApproveUser(userId, id);
            return RedirectToAction("Index");
        } // ApproveUser

        public async Task<IActionResult> RejectUser(string id)
        {
            string? userId = this.GetUserId();

            await this.service.RejectUser(userId, id);
            return RedirectToAction("Index");
        } // ApproveUser


        public async Task<IActionResult> GetUserDetails(string id)
        {
            string? userId = this.GetUserId();

            var model = await this.service.GetUserDetails(userId, id);

            return View("Details", model);
        } // GetUserDetails

        private string? GetUserId()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            return userId;
        } // GetUserId
    } // ApproveUsersController
}

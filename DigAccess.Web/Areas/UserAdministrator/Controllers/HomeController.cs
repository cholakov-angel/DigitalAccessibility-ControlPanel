using DigAccess.Data.Entities;
using DigAccess.Interfaces;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]

    public class HomeController : BaseController
    {
        private readonly IUserAdministratorService service;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IUserAdministratorService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        } // HomeController

        public async Task<IActionResult> IndexAsync()
        {
            string? userId = this.GetUserId();

            var users = await service.GetAllUsers(userId);
            return View(users);
        } // IndexAsync

        public async Task<IActionResult> ViewAllUsers()
        {
            return RedirectToAction("Index", "BlindUser");
        } // ViewAllUsers
    } // HomeController
}

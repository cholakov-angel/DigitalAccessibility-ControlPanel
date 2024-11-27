using DigAccess.Data.Entities;
using DigAccess.Interfaces;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]
    public class LogController : BaseController
    {
        private readonly ILogService service;
        private readonly UserManager<ApplicationUser> userManager;

        public LogController(UserManager<ApplicationUser> userManager, ILogService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // LicenceController
        public async Task<IActionResult> Index(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetLogs(userId, id);
            return View(model);
        } // Index

        public async Task<IActionResult> Details(string id)
        {
            var userId = this.GetUserId();
            
            var model = await this.service.GetLog(userId, id);

            return View(model);
        } // Details
    } // LicenceController
}

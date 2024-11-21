using DigAccess.Data.Entities;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.OfficeAdministrator.Controllers
{
    [Area("OfficeAdministrator")]
    [Authorize(Roles = "OfficeAdministrator")]
    public class HomeController : BaseController
    {
        private readonly IWorkerOfficeAdminService service;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager, IWorkerOfficeAdminService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // HomeController

        public IActionResult Index()
        {
            return View();
        } // Index
    } // HomeController
}

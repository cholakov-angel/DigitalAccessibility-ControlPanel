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
    public class HomeController : BaseController
    {
        private readonly IOfficeWorkerService service;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager, IOfficeWorkerService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // HomeController

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        } // Index
    } // HomeController
}

using System.Diagnostics;
using DigAccess.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        } // HomeController

        public IActionResult Index()
        {
            if (User.IsInRole("UserAdministrator"))
            {
                return RedirectToAction("Index", "Home", new { area = "UserAdministrator" });
            }
            else if (User.IsInRole("OfficeWorker"))
            {
                return RedirectToAction("Index", "Home", new { area = "OfficeWorker" });
            }
            else if (User.IsInRole("OfficeAdministrator"))
            {
                return RedirectToAction("Index", "Home", new { area = "OfficeAdministrator" });
            }
            else if (User.IsInRole("OrgAdministrator"))
            {
                return RedirectToAction("Index", "Home", new { area = "OrgAdministrator" });
            }
            else if (User.IsInRole("WaitingApproval"))
            {
                return RedirectToAction("Index", "Home", new { area = "WaitingApproval" });
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        } // Index

        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            if (code == 404)
            {
                return View("~/Views/Shared/Error/NotFound.cshtml");
            }
            else if (code == 500)
            {
                return View("~/Views/Shared/Error/BadRequest.cshtml");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        } // Error
    } // HomeController
}

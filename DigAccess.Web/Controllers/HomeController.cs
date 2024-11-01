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
            return View();
        } // Index


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        } // Error
    }
}

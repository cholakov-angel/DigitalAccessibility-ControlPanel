using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.OrgAdministrator.Controllers
{
    [Area("OrgAdministrator")]
    [Authorize(Roles = "OrgAdministrator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } // Index
    } // HomeController
}

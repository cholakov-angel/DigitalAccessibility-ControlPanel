using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.OfficeAdministrator.Controllers
{
    [Area("OfficeAdministrator")]
    [Authorize(Roles = "OfficeAdministrator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

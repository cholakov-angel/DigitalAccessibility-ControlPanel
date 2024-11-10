using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Web.Areas.OfficeWorker.Controllers
{
    public class HomeController : Controller
    {
        [Area("OfficeWorker")]
        [Authorize(Roles = "OfficeWorker")]

        public IActionResult Index()
        {
            return View();
        }
    }
}

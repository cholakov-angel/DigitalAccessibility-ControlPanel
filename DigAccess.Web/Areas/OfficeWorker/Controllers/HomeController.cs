using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.OfficeWorker.Controllers
{
    [Area("OfficeWorker")]
    [Authorize(Roles = "OfficeWorker")]
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        } // Index

        private string? GetUserId()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            return userId;
        } // GetUserId
    } // HomeController
}

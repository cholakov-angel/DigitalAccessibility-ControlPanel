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
    public class MasterKeyController : Controller
    {
        private readonly IMasterKeyService service;
        private readonly UserManager<ApplicationUser> userManager;

        public MasterKeyController(IMasterKeyService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        } // MasterKeyController

        public async Task<IActionResult> Index()
        {
            string userId = userManager.GetUserId(User);

            if (userId == null)
            {
                throw new ArgumentException("Invalid user id!");
            }
            var model = await service.GetUserMasterKey(userId);
            return View(model);
        }
    }
}

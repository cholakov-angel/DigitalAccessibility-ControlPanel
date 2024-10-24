using System.Security.Claims;
using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Blind;
using DigAccess.Interfaces;
using DigAccess.Models.BlindUser;
using DigAccess.Services;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace DigAccess.Web.Controllers
{
    [Authorize(Roles = "UserAdministrator")]
    public class BlindUserController : Controller
    {
        private readonly IBlindUserService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DigAccessDbContext context;

        public BlindUserController(UserManager<ApplicationUser> userManager, IBlindUserService service, DigAccessDbContext context)
        {
            this.service = service;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = await service.GetAllModels(userId);
            return View(list);
        }

        public async Task<IActionResult> Add()
        {
            BlindUserViewModel model = new BlindUserViewModel();
            model.CityNames = await service.GetCities();
            return View(model);

        }

        public async Task<IActionResult> AddUser(BlindUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CityNames = await service.GetCities();
                return View("Add",model);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            service.Add(model, userId);
            return RedirectToAction("Index");
        }
    }
}

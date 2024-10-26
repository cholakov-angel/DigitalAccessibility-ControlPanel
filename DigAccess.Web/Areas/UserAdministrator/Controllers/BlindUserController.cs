using System.Security.Claims;
using DigAccess.Common;
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

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]
    public class BlindUserController : Controller
    {
        private readonly IBlindUserService service;
        private readonly UserManager<ApplicationUser> userManager;

        public BlindUserController(UserManager<ApplicationUser> userManager, IBlindUserService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // BlindUserController
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = await service.GetAllModels(userId);

            return View(list);
        } // Index

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            BlindUserViewModel model = new BlindUserViewModel();
            model.CityNames = await service.GetCities();

            return View(model);
        } // Add

        [HttpPost]
        public async Task<IActionResult> Add(BlindUserViewModel model)
        {
            model.CityNames = await service.GetCities();

            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool result = await service.Add(model, userId);

            if (result == false)
            {
                ModelState.AddModelError("PersonalId", BlindUserConstants.PersonalIDError);
                return View("Add", model);
            }

            return RedirectToAction("Index");
        } // AddUser

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = await service.GetUserDetails(id);

            if (model == null)
            {
                throw new Exception("Invalid model!");
            }

            return View(model);
        } // Details
    }
}

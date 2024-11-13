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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            string? userId = this.GetUserId();

            var list = await service.GetAllModels(userId);

            return View(list);
        } // Index

        public async Task<IActionResult> GetUser(string name)
        {
            string? userId = this.GetUserId();

            var list = await service.GetModel(userId, name);

            return View("Index", list);
        }
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

            string? userId = this.GetUserId();

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
            string? userId = this.GetUserId();

            var model = await service.GetUserDetails(id, userId);

            if (model == null)
            {
                throw new Exception("Invalid model!");
            }

            return View(model);
        } // Details

        public async Task<IActionResult> UserPage(string id)
        {
            string? userId = this.GetUserId();

            var user = await service.GetUserInformation(DateTime.Now, id, userId);

            if (user == null)
            {
                throw new Exception("Invalid model!");
            }
            return View(user);
        } // UserPage

        private string? GetUserId()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            return userId;
        } // GetUserId
    } // BlindUserController
}

﻿using DigAccess.Data.Entities;
using DigAccess.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]
    public class LicenceController : Controller
    {
        private readonly ILicenceService service;
        private readonly UserManager<ApplicationUser> userManager;

        public LicenceController(UserManager<ApplicationUser> userManager, ILicenceService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // LicenceController

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            var users = await service.GetAll(userId);
            return View(users);
        } // Index

        public async Task<IActionResult> UserLicence(string id)
        {
            var licences = await service.GetLicences(id);

            return View(licences);
        } // UserLicences
    }
}
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
    public class AnswersController : BaseController
    {
        private readonly IAnswerUserAdministratorService service;
        private readonly UserManager<ApplicationUser> userManager;

        public AnswersController(UserManager<ApplicationUser> userManager, IAnswerUserAdministratorService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // AnswersController

        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetAnswers(userId, page);

            int totalItems = await this.service.CountAnswers(userId);
            int totalPages = (int)Math.Ceiling(totalItems / (double)8);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(model);
        } // Index

        public async Task<IActionResult> Details(string id)
        {
            var userId = this.GetUserId();

            var model = await this.service.GetAnswer(userId, id);

            return View(model);
        } // Details

        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.GetUserId();

            bool result = await this.service.DeleteAnswer(userId, id);

            if (result == false)
            {
                throw new Exception("Error occured!");
            }

            return RedirectToAction("Index");
        } // Delete
    } // AnswersController
}

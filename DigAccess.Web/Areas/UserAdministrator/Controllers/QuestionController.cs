using DigAccess.Data.Entities;
using DigAccess.Models.UserAdministrator;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.UserAdministrator.Controllers
{
    [Area("UserAdministrator")]
    [Authorize(Roles = "UserAdministrator")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService service;
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionController(IQuestionService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        } // MasterKeyController

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        } // Add

        [HttpPost]
        public async Task<IActionResult> Add(QuestionViewModel model)
        {
            string? userId = this.GetUserId();
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            bool result = await this.service.AskQuestion(model, userId, DateTime.Now);

            if (result == false)
            {
                throw new Exception("Invalid operation!");
            }
            return RedirectToAction("Success");
        } // Add

        public async Task<IActionResult> Success()
        {
            return View();
        } // Success
        private string? GetUserId()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            return userId;
        } // GetUserId
    }
}

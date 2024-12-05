using DigAccess.Data.Entities;
using DigAccess.Models.OfficeWorker;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.OfficeWorker.Controllers
{
    [Area("OfficeWorker")]
    [Authorize(Roles = "OfficeWorker")]
    public class AnswersController : BaseController
    {
        private readonly IAnswerOfficeWorkerService service;
        private readonly UserManager<ApplicationUser> userManager;

        public AnswersController(UserManager<ApplicationUser> userManager, IAnswerOfficeWorkerService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // AnswerController

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            string? userId = this.GetUserId();
            var model = await this.service.CreateAnswer(userId, id, DateTime.Now);

            return View(model);
        } // Index

        [HttpPost]
        public async Task<IActionResult> Answer(AnswerViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View("Index", model);
            }
            string? userId = this.GetUserId();
            bool result = await this.service.AnswerQuestion(model, userId);

            if (result == false)
            {
                throw new Exception("Invalid operation!");
            }

            return RedirectToAction("Index", "Questions");
        } // Answer
    }
}

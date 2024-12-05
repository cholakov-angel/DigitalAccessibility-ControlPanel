using DigAccess.Data.Entities;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigAccess.Web.Areas.OfficeWorker.Controllers
{
    [Area("OfficeWorker")]
    [Authorize(Roles = "OfficeWorker")]
    public class QuestionsController : BaseController
    {
        private readonly IQuestionOfficeWorkerService service;
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionsController(UserManager<ApplicationUser> userManager, IQuestionOfficeWorkerService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // QuestionsController

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = this.GetUserId();
            var model = await this.service.GetQuestions(userId, page);

            int totalItems = await this.service.CountQuestions(userId);
            int totalPages = (int)Math.Ceiling(totalItems / (double)8);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(model);
        } // Index

        [HttpGet]
        public async Task<IActionResult> GetQuestionsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return RedirectToAction("Index");
            }

            var userId = this.GetUserId();
            var model = await this.service.GetQuestionsByName(userId, name);

            return View("Index", model);
        } // GetQuestionsByName

        [HttpGet]
        public async Task<IActionResult> QuestionDetails(string id)
        {
            var userId = this.GetUserId();
            var model = await this.service.GetQuestion(userId, id);

            return View(model);
        }
    }
}

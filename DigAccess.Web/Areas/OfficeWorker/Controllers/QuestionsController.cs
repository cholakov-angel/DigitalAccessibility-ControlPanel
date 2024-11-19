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
    public class QuestionsController : Controller
    {
        private readonly IQuestionOfficeWorkerService service;
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionsController(UserManager<ApplicationUser> userManager, IQuestionOfficeWorkerService service)
        {
            this.service = service;
            this.userManager = userManager;
        } // QuestionsController
        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId();
            var model = await this.service.GetQuestions(userId);

            return View(model);
        } // Index

        public async Task<IActionResult> QuestionDetails(string id)
        {
            var userId = this.GetUserId();
            var model = await this.service.GetQuestion(userId, id);

            return View(model);
        }
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

using DigAccess.Services.Interfaces;
using DigAccess.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Application.Controllers
{
    public class BlindUserController : BaseController
    {
        private readonly IBlindUserService service;
        public BlindUserController(IBlindUserService service)
        {
            this.service = service;
        } // BlindUserController

        [HttpPost]
        public async Task<IActionResult> GetEmail([FromQuery] string blindUserId, string administratorId)
        {
            var email = this.service.GetEmail(blindUserId, administratorId);

            if (email == null)
            {
                return NotFound();
            }

            return Ok(email);
        } // GetEmail

        [HttpPost]
        public async Task<IActionResult> AddLog([FromBody] LogViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            bool result = await this.service.AddLog(model);

            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        } // AddLog
    } // BlindUserController
}

using DigAccess.Services;
using DigAccess.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigAccess.Application.Controllers
{
    public class LicenceController : BaseController
    {
        private readonly ILicenceService service;
        public LicenceController(ILicenceService service)
        {
            this.service = service;
        } // LicenceController

        [HttpPost]
        public async Task<IActionResult> ValidateLicense([FromQuery] string license, string masterKey)
        {
            var result = await this.service.IsLicenseActive(license, masterKey);

            if (result)
            {
                return Ok();
            }
            return NotFound();
        } // GetLicense

        [HttpPost]
        public async Task<IActionResult> GetUserFromLicense([FromQuery] string license, string masterKey)
        {
            var user = await this.service.GetUserFromLincense(license, masterKey);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }  // GetUserFromLicense
    } // LicenceController
}

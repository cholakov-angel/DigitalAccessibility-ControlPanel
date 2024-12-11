using DigAccess.Services;
using DigAccess.Services.Interfaces;
using DigAccess.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
        public async Task<IActionResult> ActivateLicense([FromBody] LicenseActivateViewModel model)
        {
            var result = await this.service.ActivateLicense(model);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        } // ActivateLicense
    } // LicenceController
}

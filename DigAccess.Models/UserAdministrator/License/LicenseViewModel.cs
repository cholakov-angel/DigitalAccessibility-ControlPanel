using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.UserAdministrator.License
{
    public class LicenseViewModel
    {
        public string Id { get; set; }

        public string LicenceNumber { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public bool IsActivated { get; set; }
    }
}

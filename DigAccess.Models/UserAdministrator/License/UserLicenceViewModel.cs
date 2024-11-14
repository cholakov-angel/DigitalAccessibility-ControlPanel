using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.UserAdministrator.License
{
    public class UserLicenseViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public ICollection<LicenseViewModel> Licenses { get; set; }
    }
}

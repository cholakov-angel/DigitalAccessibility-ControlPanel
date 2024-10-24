using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.Licence
{
    public class UserLicenceViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public ICollection<LicenceViewModel> Licences { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.Licence
{
    public class LicenceDetailsViewModel
    {
        public string Id { get; set; }

        public LicenceDetailsBlindUserViewModel BlindUser { get; set; }

        public string DateFrom { get; set; }

        public string MACAddress { get; set; }

        public bool IsActive { get; set; }

        public string DateOfActivation { get; set; }

    }
}

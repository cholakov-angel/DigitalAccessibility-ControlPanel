using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.ViewModels
{
    public class LicenseActivateViewModel
    {
        public string MasterKey { get; set; }

        public string LicenseKey { get; set; }

        public string MACAddress { get; set; }

        public string DateTime { get; set; }
    }
}

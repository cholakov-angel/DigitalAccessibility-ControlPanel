using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.ViewModels
{
    public class EmailViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string IncommingServer { get; set; }

        public string OutgoingServer { get; set; }

        public int Port { get; set; }
    }
}

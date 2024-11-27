using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.UserAdministrator.Log
{
    public class LogViewModel
    {
        public string Id { get; set; }
        public string BlindUserId { get; set; }

        public string LogType { get; set; }

        public string DateTimeOfLog { get; set; }

    }
}

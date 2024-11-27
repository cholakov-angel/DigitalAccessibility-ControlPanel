using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.UserAdministrator.Log
{
    public class LogDetailsViewModel
    {
        public string Id { get; set; }
        public string BlindUserId { get; set; }

        public string LogType { get; set; }

        public int LogCode { get; set; }

        public string LogText { get; set; } 

        public string DateTimeOfLog { get; set; }
    }
}

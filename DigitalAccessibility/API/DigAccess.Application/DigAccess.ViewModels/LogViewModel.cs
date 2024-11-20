using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.ViewModels
{
    public class LogViewModel
    {
        [Required]
        public string BlindUserId { get; set; }

        [Required]
        public string AdminsitratorId { get; set; }

        [Required]
        public string LogType { get; set; } = null!;

        [Required]
        public int LogCode { get; set; }

        [Required]
        public string LogText { get; set; } = null!;

        [Required]
        public string DateTimeOfLog { get; set; } = null!;
    }
}

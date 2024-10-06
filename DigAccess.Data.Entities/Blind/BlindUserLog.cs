using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Entities.Blind
{
    public class BlindUserLog
    {
        public BlindUserLog()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        public Guid BlindUserId { get; set; }

        [ForeignKey(nameof(BlindUserId))]
        public BlindUser BlindUser { get; set; }

        [Required]
        public string LogType { get; set; } = null!;

        [Required]
        public int LogCode { get; set; }

        [Required] 
        public string LogText { get; set; } = null!;

        public DateTime DateTimeOfLog { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Entities.Blind
{
    public class EmailSettings
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string IncomingServer { get; set; } = null!;

        [Required]
        public string OutgoingServer { get; set; } = null!;

        [Required]
        public int Port { get; set; }

        public ICollection<BlindUserEmail> BlindUserEmails { get; set; } = new List<BlindUserEmail>();
    }
}

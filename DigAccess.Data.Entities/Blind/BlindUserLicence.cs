using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Entities.Blind
{
    public class BlindUserLicence
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BlindUserId { get; set; }

        [ForeignKey(nameof(BlindUserId))]
        public BlindUser BlindUser { get; set; }

        [Required] 
        public string LicenceNumber { get; set; } = null!;

        public bool IsActivated { get; set; }

        public DateTime DateFrom { get; set; }

        [MaxLength(24)]
        public string? MacAddress { get; set; }

        public DateTime DateOfActivation { get; set; }
    }
}

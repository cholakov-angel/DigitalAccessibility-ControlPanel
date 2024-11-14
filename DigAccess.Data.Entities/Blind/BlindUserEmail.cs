using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Data.Entities.Blind
{
    public class BlindUserEmail
    {
        public BlindUserEmail()
        {
            this.Id = new Guid();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid BlindUserId { get; set; }

        [ForeignKey(nameof(BlindUserId))]
        public BlindUser BlindUser { get; set; }

        public string Email { get; set; }

        public string EmailPassword { get; set; }

        public Guid EmailSettingsId { get; set; }

        [ForeignKey(nameof(EmailSettingsId))]
        public EmailSettings EmailSettings { get; set; }

    }
}

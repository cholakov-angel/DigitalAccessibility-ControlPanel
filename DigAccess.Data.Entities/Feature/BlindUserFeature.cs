using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Data.Entities.Blind;

namespace DigAccess.Data.Entities.Feature
{
    public class BlindUserFeature
    {
        public Guid BlindUserId { get; set; }

        [ForeignKey(nameof(BlindUserId))]
        public BlindUser BlindUser { get; set; }

        public Guid FeatureId { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public Feature Feature { get; set; }

        [Required]
        public string LicenceKey { get; set; } = null!;
    }
}

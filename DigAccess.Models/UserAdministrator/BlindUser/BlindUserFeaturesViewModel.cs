using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.UserAdministrator.BlindUser
{
    public class BlindUserFeaturesViewModel
    {
        public string BlindUserId { get; set; }

        public string FeatureId { get; set; }
        public string FeatureName { get; set; }

        public string? LicenceKey { get; set; }
    }
}

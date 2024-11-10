using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.WaitingApproval
{
    public class WaitingApprovalViewModel
    {
        [Required]
        public string OrganisationId { get; set; } = null!;

        [Required]
        public string OrganisationName { get; set; } = null!;

        [Required]
        public string OfficeId { get; set; } = null!; 
        public List<OfficeViewModel>? Offices { get; set; }
    }
}
